using UnityEngine;

/// <summary>
/// 控制角色的移动，包含了WASD移动和寻路移动
/// </summary>
[RequireComponent(typeof(WASDController))]
[RequireComponent(typeof(SyncPosRotController))]
[RequireComponent(typeof(AreaController))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMoveController : Moveable, IPersonController
{
    public void updated()
    {
        stopMove();

        Start();
    }

    private Person person;

    private CharacterController characterController;

    private WASDController wasdController; 

    private MoveAnimationController moveAnimationController;

    private SyncPosRotController syncPosRotController;

    private AreaController areaController;

    // 重力加速度
    private float gravity = 20;
    // 是否在地面上
    private bool onGround = false;
    // 移动朝向
    private Vector3 moveDirection = Vector3.zero;

    private CollisionFlags flags;

    // 场景资源动态加载
    private Vector3 oldPos = Vector3.zero;

    // 上次同步给服务器的坐标
    private Vector3 serverPos = Vector3.zero;

    private Vector3 serverRot = Vector3.zero;

    // 是否处于飞行状态
    private bool isFly = false;

    void Start()
    {
        this.person = GetComponent<Person>();

        this.characterController = GetComponent<CharacterController>();

        // 设置胶囊碰撞体信息
        this.characterController.height = this.person.height;
        this.characterController.radius = this.person.radius;
        this.characterController.center = this.person.center;

        this.wasdController = GetComponent<WASDController>();

        this.moveAnimationController = GetComponent<MoveAnimationController>();

        this.syncPosRotController = GetComponent<SyncPosRotController>();

        this.areaController = GetComponent<AreaController>();

        this.navMeshPath = new NavMeshPath();
    }


    private float h = 0;
    private float v = 0;
    private bool wasd = false;

    void Update()
    {
        if (!onGround && !isFly)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            flags = characterController.Move(moveDirection * Time.deltaTime);
            onGround = (flags & CollisionFlags.Below) != 0;
            return;
        }

        // h = Input.GetAxis("Horizontal");
        // v = Input.GetAxis("Vertical");

        this.wasdController.getInputHV(ref h, ref v);

        wasd = h != 0 || v != 0;

        if (wasd && navMoveing) stopMove();
        else if (!wasd && !navMoveing)
        {
            //animator.SetBool("run", false);
            //animator.SetFloat("speed", 0f);
            moveAnimationController.OnIdle();
            return;
        }

        //animator.SetBool("run", true);
        //animator.SetFloat("speed", h * h + v * v);
        moveAnimationController.OnRun(h, v);

        if (wasd)
        {
            moveDirection = new Vector3(h, 0, v);

            // 以角色自身坐标朝向
            // moveDirection = transform.TransformDirection(moveDirection);

            // 以镜头坐标朝向
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0f;
            moveDirection *= this.person.finalAbility.speed;
        }
        else if (navMoveing)
        {
            if (Vector3.Distance(transform.position, curAutoPos) < 0.1f)
            {
                if (!nextPoint())
                {
                    stopMove();
                }
                return;
            }

            // 目标点朝向
            moveDirection = curAutoPos - transform.position;
            moveDirection.Normalize();
            moveDirection.y = 0f;
            moveDirection *= this.person.finalAbility.speed;
        }

        // Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.
        // 按下右键后，角色朝向始终与镜头朝向相同
        //        if (Input.GetMouseButton(1))
        //        {
        //            if (IsInWater)
        //            {
        //                transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0);
        //                
        //            }
        //            else
        //            {
        //                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        //            }
        //        }
        //        else
        //        {
        //            transform.Rotate(0, h * rotateSpeed * Time.deltaTime, 0);
        //            transform.Rotate(0, h * rotateSpeed * Time.deltaTime, 0);
        //        }

        // if (moveDirection == Vector3.zero) return;

        flags = characterController.Move(moveDirection * Time.deltaTime);
        onGround = (flags & CollisionFlags.Below) != 0;

        Vector3 v3 = moveDirection - new Vector3(0, moveDirection.y, 0f);
        if (v3 != Vector3.zero)
        {
            Quaternion newQuaternion = Quaternion.LookRotation(v3);
            transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion, 0.2f);
        }

        // 出发区域改变判断
        {
            float x_changed = oldPos.x - transform.position.x;
            if (x_changed > 2f || x_changed < -2f)
            {
                areaController.OnMove(oldPos, transform.position);
                oldPos = transform.position;
                return;
            }

            float z_changed = oldPos.z - transform.position.z;
            if (z_changed > 2f || x_changed < -2f)
            {
                areaController.OnMove(oldPos, transform.position);
                oldPos = transform.position;
                return;
            }
        }

    }

    void OnEnable()
    {
        InvokeRepeating("SyncPosRot", 0.5f, 0.1f);
    }

    void OnDisable()
    {
        CancelInvoke("SyncPosRot");
    }

    #region 同步坐标
    void SyncPosRot()
    {
        if (Vector3.Distance(serverPos, transform.position) > 0.1f)
        {
            this.syncPosRotController.syncPostion(transform.position);
            this.serverPos = transform.position;
        }
        else if (Vector3.Distance(serverRot, moveDirection) > 0.1f)
        {
            this.syncPosRotController.syncDiection(moveDirection);
            this.serverRot = moveDirection;
        }
    }
    #endregion

    #region 点击移动/寻路
    /// <summary>
    /// 是否在导航寻路中
    /// </summary>
    private bool navMoveing = false;
    /// <summary>
    /// 自动寻路信息
    /// </summary>
    private NavMeshPath navMeshPath;
    /// <summary>
    /// The end point.
    /// 最终目的地
    /// </summary>
    private Vector3 endPoint = Vector3.zero;
    /// <summary>
    /// The current auto point.
    /// 当前自动寻路的下个目标点
    /// </summary>
    private Vector3 curAutoPos = Vector3.zero;

    private int nextPointIndex = 0;

    /// <summary>
    /// 自动寻路
    /// </summary>
    /// <param name="endPos"></param>
    public override bool move(Vector3 endPos)
    {
        if (navMoveing) stopMove();

        // endPos.y = transform.position.y;
        if (Vector3.Distance(transform.position, endPos) < 0.5f)
        {
            Debug.Log("移动距离不超过0.5f");
            return false;
        }

        bool canNavMove = NavMesh.CalculatePath(transform.position, endPos, 255, navMeshPath);
        if (canNavMove)
        {
            navMoveing = true;

            nextPointIndex = 0;
            endPoint = endPos;

            nextPoint();
        }
        else
        {
            Debug.LogError("无法向指定点寻路！");
        }
        return canNavMove;
    }

    /// <summary>
    /// 停止自动导航寻路
    /// </summary>
    public override bool stopMove()
    {
        if (navMoveing)
        {
            navMoveing = false;

            nextPointIndex = 0;
            endPoint = Vector3.zero;
            curAutoPos = Vector3.zero;
            moveDirection = Vector3.zero;

            navMeshPath.ClearCorners();
            return true;
        }
        return false;
    }


    /// <summary>
    /// 是否还有未走完的点
    /// </summary>
    /// <returns></returns>
    private bool nextPoint()
    {
        nextPointIndex++;

        bool flag = true;
        if (navMeshPath.corners.Length > nextPointIndex)
        {
            curAutoPos = navMeshPath.corners[nextPointIndex];
        }
        else
        {
            curAutoPos = endPoint;
            flag = false;
        }

        return flag;
    }


    #endregion
}