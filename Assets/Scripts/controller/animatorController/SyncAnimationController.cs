using com.game.proto;
using UnityEngine;

public class SyncAnimationController : AnimatorController
{
    #region Animator条件值

    public override void setIdleType(int idleType)
    {
        // 待机不用传
        this.animator.SetInteger("idleType", idleType);
    }

    public override void setH(float h)
    {
        if (this.serverAnimatorInfo.h == h) return;
        this.serverAnimatorInfo.h = h;

        this.animator.SetFloat("h", h);
        this.flag = true;
    }

    public override void setV(float v)
    {
        if (this.serverAnimatorInfo.v == v) return;
        this.serverAnimatorInfo.v = v;

        this.animator.SetFloat("v", v);
        this.flag = true;
    }

    public override void setRuning(bool runing)
    {
        if (this.serverAnimatorInfo.runing == runing) return;
        this.serverAnimatorInfo.runing = runing;

        this.animator.SetBool("runing", runing);
        this.flag = true;
    }

    public override void setAttacking(bool inAttack)
    {
        if (this.serverAnimatorInfo.inAttack == inAttack) return;

        this.serverAnimatorInfo.inAttack = inAttack;

        this.animator.SetBool("inAttack", inAttack);
        this.flag = true;
    }

    public override void setHaveTarget(bool haveTarget)
    {
        if (this.serverAnimatorInfo.havaTarget == haveTarget) return;

        this.serverAnimatorInfo.havaTarget = haveTarget;
        this.animator.SetBool("haveTarget", haveTarget);
        this.flag = true;
    }


    private bool flag = false;

    private AnimatorInfo serverAnimatorInfo = new AnimatorInfo();

    void SyncAimator()
    {
        if (!flag) return;
        flag = false;

        Debug.Log("SyncAimator ... ");
        ReqSyncAnimatorMessage reqSyncAnimatorMessage = new ReqSyncAnimatorMessage();
        reqSyncAnimatorMessage.animatorInfo = serverAnimatorInfo;
        NetManager.Instance.SendMessage(NetMessageBuilder.Encode((int)reqSyncAnimatorMessage.msgID, reqSyncAnimatorMessage));
    }

    void OnEnable()
    {
        InvokeRepeating("SyncAimator", 0.5f, 0.125f);
    }

    void OnDisable()
    {
        CancelInvoke("SyncAimator");
    }

    #endregion
}
