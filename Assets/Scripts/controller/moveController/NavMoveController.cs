using UnityEngine;

/// <summary>
/// 2点之间寻路，客户端需要判断阻挡点，寻路层
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class NavMoveController : Moveable, IPersonController
{
    public void updated()
    {
        stopMove();

        Start();
    }

    private Person person;

    private CharacterController characterController;

    private MoveAnimationController moveAnimationController;

    // 重力加速度
    public float gravity = 20;
    // 是否在地面上
    public bool onGround = false;

    // 移动朝向
    private Vector3 moveDirection = Vector3.zero;

    private CollisionFlags flags;

    void Start()
    {
        this.person = GetComponent<Person>();

        this.characterController = GetComponent<CharacterController>();

        // 设置胶囊碰撞体信息
        this.characterController.height = this.person.height;
        this.characterController.radius = this.person.radius;
        this.characterController.center = this.person.center;
        this.characterController.skinWidth = this.person.skinWidth;

        // 设置移动动画控制器
        this.moveAnimationController = GetComponent<MoveAnimationController>();

        this.navMeshPath = new NavMeshPath();
    }

    private float x;
    private float z;

    void Update()
    {
        if (!onGround)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            flags = characterController.Move(moveDirection * Time.deltaTime);
            onGround = (flags & CollisionFlags.Below) != 0;
            return;
        }

        // 朝向改变
        if (turning)
        {
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            if (Vector3.Distance(eulerAngles, endDir) < 0.1f)
            {
                turning = false;
                return;
            }
            Quaternion newQuaternion = Quaternion.LookRotation(endDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion, this.person.rotSpeed * Time.deltaTime /*0.2f*/);
            moveAnimationController.OnTurn(newQuaternion);
            return;
        }

        if (!navMoveing)
        {
            moveAnimationController.OnIdle();
            return;
        }

        moveAnimationController.OnRun();

        x = curAutoPos.x - transform.position.x;
        z = curAutoPos.z - transform.position.z;

        if (x * x + z * z < 0.1f)
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

        flags = characterController.Move(moveDirection * Time.deltaTime);
        onGround = (flags & CollisionFlags.Below) != 0;

        Vector3 v3 = moveDirection - new Vector3(0, moveDirection.y, 0f);
        if (v3 != Vector3.zero)
        {
            Quaternion newQuaternion = Quaternion.LookRotation(v3);
            transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion, this.person.rotSpeed * Time.deltaTime /*0.2f*/);
        }
    }


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

    // 是否在旋转中
    private bool turning = false;

    // 最终朝向
    private Vector3 endDir = Vector3.zero;

    /// <summary>
    /// 自动寻路
    /// </summary>
    /// <param name="endPos"></param>
    public override bool move(Vector3 endPos)
    {
        if (navMoveing) stopMove();

        // endPos.y = transform.position.y;
        if (Vector3.Distance(transform.position, endPos) < 0.2f)
        {
            Debug.Log("移动距离不超过0.2f");
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

    public override bool isMoving()
    {
        return navMoveing;
    }

    public override bool turn(Vector3 direction)
    {
        turning = true;
        endDir = direction;
        return true;
    }

    public override bool stopTrun()
    {
        if (turning)
        {
            turning = false;
            return true;
        }
        return false;
    }

    public override bool isTurning()
    {
        return turning;
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