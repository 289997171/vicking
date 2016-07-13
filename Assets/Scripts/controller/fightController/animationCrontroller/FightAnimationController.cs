using UnityEngine;

public class FightAnimationController : MonoBehaviour
{
    protected Person person;
    protected Animator animator;
    

    protected void Start()
    {
        this.person = GetComponent<Person>();
        this.animator = GetComponent<Animator>();
    }


    public void castSkill(int skillModelId)
    {
        this.animator.SetInteger("iAction", skillModelId);
    }
}
