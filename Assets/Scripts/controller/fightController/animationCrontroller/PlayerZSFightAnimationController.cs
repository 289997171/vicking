
using UnityEngine;

public class PlayerZSFightAnimationController : FightAnimationController
{

    private Animator animator;

    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }


//    public override void castSkill(int skillId, int skillLv, float skillCost)
//    {
//
//    }
}
