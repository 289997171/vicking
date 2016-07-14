using com.game.proto;
using UnityEngine;


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

    private bool inAttack = false;

    // 目标
    public GameObject target;

    private Animator animator;

    private MoveAnimatorController animatorController;


    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.animatorController = GetComponent<MoveAnimatorController>();
    }

    

    public override void OnIdle()
    {
        animatorController.setRuning(false);

        if (inAttack)
        {
            // 战斗待机
        }
        else
        {
            // 处于Base Layer

            // 普通待机
            baseLayerStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (baseLayerStateInfo.IsName("Stand") && baseLayerStateInfo.normalizedTime > 0.9)
            {
                //this.animator.SetInteger("idleType", 1);
                animatorController.setIdleType(1);
            }
            else if (baseLayerStateInfo.IsName("IDLE1") && baseLayerStateInfo.normalizedTime > 0.9)
            {
                //this.animator.SetInteger("idleType", 0);
                animatorController.setIdleType(0);
            }
        }

    }

    public override void OnTurn(Quaternion rot)
    {
    }

    public override void OnRun(float h = 0, float v = 0)
    {
        //this.animator.SetBool("runing", true);
        animatorController.setRuning(true);

        if (inAttack)
        {

            if (target != null && target.gameObject.activeInHierarchy /*&& target.isLive()*/)
            {
                //this.animator.SetFloat("h", h);
                //this.animator.SetFloat("v", v);

                animatorController.setH(h);
                animatorController.setV(v);

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
