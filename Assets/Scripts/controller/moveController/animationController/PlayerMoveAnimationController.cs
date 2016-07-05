
using UnityEngine;


class BaseLayer
{

    // 空闲状态 0，1
    private int idleType = 0;

    // 坐下状态
    private bool sitState = false;

    // 敬礼
    private bool saluteTrigger = false;

    public int IdleType
    {
        get { return idleType; }
        set { idleType = value; }
    }
}


public class PlayerMoveAnimationController : MoveAnimationController
{

    private const string BaseLayer = "BaseLayer";
    private const string AttackLayer = "AttackLayer";
    private const string AttackMove = "AttackMove";
    private const string StateLayer = "StateLayer";


    private AnimatorStateInfo baseLayerStateInfo;
    private AnimatorStateInfo attackLayerStateInfo;
    private AnimatorStateInfo attackMoveStateInfo;
    private AnimatorStateInfo attackMoveTargetStateInfo;

    // 是否处于战斗状态
    private bool inAttack = false;

    // 移动
    private bool runing = false;

    // 目标
    public GameObject target;


    private Animator animator;


    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        InvokeRepeating("UpdateLayer", 0.5f, 0.99f);
    }

    void OnDisable()
    {
        CancelInvoke("UpdateLayer");
    }

    void UpdateLayer()
    {
        this.runing = this.animator.GetBool("runing");
        this.inAttack = this.animator.GetBool("inAttack");

        if (target != null && target.gameObject.activeInHierarchy /*target.isAlive()*/)
        {
            // 判断人物朝向

            // startCon
        }
    }

    private int i = 0;

    public override void OnIdle()
    {
        this.animator.SetBool("runing", false);

        if (inAttack)
        {
            // 战斗待机
        }
        else
        {
            // 处于Base Layer

            // 普通待机
            baseLayerStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (baseLayerStateInfo.IsName("Stand") && baseLayerStateInfo.normalizedTime > 0.9 && i++ > 5)
            {
                i = 0;
                this.animator.SetInteger("idleType", 1);
                // animator.CrossFade("Idle",0.1f);
            }
            else if (baseLayerStateInfo.IsName("IDLE1") && baseLayerStateInfo.normalizedTime > 0.9)
            {
                this.animator.SetInteger("idleType", 0);
            }
        }


    }

    public override void OnTurn(Quaternion rot)
    {
    }

    public override void OnRun(float h = 0, float v = 0)
    {
        this.animator.SetBool("runing", true);

        if (inAttack)
        {

            if (target != null && target.gameObject.activeInHierarchy /*&& target.isLive()*/)
            {
                this.animator.SetFloat("h", h);
                this.animator.SetFloat("v", v);


                // TODO 补偿算法
                

            }
            else
            {


            }
        }
        else
        {
        }

    }

    public override void OnSwing(float h = 0, float v = 0)
    {
    }

    public override void OnFly(float h = 0, float v = 0)
    {
    }

    public override void OnJump()
    {
    }
}
