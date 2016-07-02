using UnityEngine;

/// <summary>
/// 2点间的直线运动，一般用于服务器控制怪物,NPC等的移动。由于服务器计算了阻挡信息，故此处不计算寻路！
/// </summary>

[RequireComponent(typeof(CharacterController))]
public class MoveController : Moveable, IPersonController
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
    private float gravity = 20;

    // 是否移动中
    private bool moveing = false;

    // 是否在地面上
    public bool onGround = false;

    private CollisionFlags flags;

    // 移动朝向
    private Vector3 moveDirection = Vector3.zero;

    // 移动终点
    private Vector3 endPos;

    // 是否在旋转中
    private bool turning = false;

    // 最终朝向
    private Vector3 endDir = Vector3.zero;

    void Start()
    {
        this.person = GetComponent<Person>();

        this.characterController = GetComponent<CharacterController>();
        
        // 设置胶囊碰撞体信息
        this.characterController.height = this.person.height;
        this.characterController.radius = this.person.radius;
        this.characterController.center = this.person.center;

        // 设置移动动画控制器
        this.moveAnimationController = GetComponent<MoveAnimationController>();
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
            transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion, 0.2f);
            moveAnimationController.OnTurn(newQuaternion);
            return;
        }

        if (!moveing)
        {
            //animator.SetBool("run", false);
            moveAnimationController.OnIdle();
            return;
        }

        x = endPos.x - transform.position.x;
        z = endPos.z - transform.position.z;

        if (x * x + z * z < 0.1f)
        {
            stopMove();
            return;
        }

        //animator.SetBool("run", true);
        moveAnimationController.OnRun();

        // 目标点朝向
        moveDirection = endPos - transform.position;
        moveDirection.Normalize();
        moveDirection.y = 0f;
        moveDirection *= person.finalAbility.speed;


        flags = characterController.Move(moveDirection * Time.deltaTime);
        onGround = (flags & CollisionFlags.Below) != 0;

        Vector3 v3 = moveDirection - new Vector3(0, moveDirection.y, 0f);
        if (v3 != Vector3.zero)
        {
            Quaternion newQuaternion = Quaternion.LookRotation(v3);
            transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion, 0.2f);
        }

    }


    public override bool move(Vector3 worldPos)
    {
        moveing = true;
        endPos = worldPos;
        return true;
    }

    public override bool stopMove()
    {
        if (moveing)
        {
            moveing = false;
            return true;
        }
        return false;

    }

    public override bool isMoving()
    {
        return moveing;
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
}