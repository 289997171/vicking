
using com.game.proto;
using UnityEngine;

public class UpdateSyncAnimationController : MoveAnimatorController
{

    public void SyncAimator(AnimatorInfo serverAnimatorInfo)
    {
        setH(serverAnimatorInfo.h);
        setV(serverAnimatorInfo.v);
        setRuning(serverAnimatorInfo.runing);
        setAttacking(serverAnimatorInfo.inAttack);
        setHaveTarget(serverAnimatorInfo.haveTarget);
    }

    #region Animator条件值

    public override void setIdleType(int idleType)
    {
        // 待机不用传
        this.animator.SetInteger("idleType", idleType);
    }

    public override void setH(float h)
    {
        this.animator.SetFloat("h", h);
    }

    public override void setV(float v)
    {
        this.animator.SetFloat("v", v);
    }

    public override void setRuning(bool runing)
    {
        this.animator.SetBool("runing", runing);
    }

    public override void setAttacking(bool inAttack)
    {
        this.animator.SetBool("inAttack", inAttack);
    }

    public override void setHaveTarget(bool haveTarget)
    {
        this.animator.SetBool("haveTarget", haveTarget);
    }


    #endregion
}
