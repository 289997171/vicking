
using UnityEngine;

public class PlayerZSRunFightAnimationController : FigtAnimationController
{

    private Animator animator;

    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }


    public override void castSkill(int skillId, int skillLv)
    {

    }
}
