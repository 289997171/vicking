using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public abstract class AnimatorController : MonoBehaviour
{
    protected Person person;

    protected Animator animator;

    protected void Start()
    {
        this.person = GetComponent<Person>();
        this.animator = GetComponent<Animator>();
    }

    public void SetFloat(string animatorCondition, float value)
    {
        this.animator.SetFloat(animatorCondition, value);
    }
    
    public void SetBool(string animatorCondition, bool value)
    {
        this.animator.SetBool(animatorCondition, value);
    }
    
    public void SetInteger(string animatorCondition, int value)
    {
        this.animator.SetInteger(animatorCondition, value);
    }
    
    public void SetTrigger(string animatorCondition)
    {
        this.animator.SetTrigger(animatorCondition);
    }






    public abstract void setIdleType(int idleType);


    public abstract  void setH(float h);


    public abstract  void setV(float v);


    public abstract  void setRuning(bool runing);

    public abstract  void setAttacking(bool inAttack);


    public abstract  void setHaveTarget(bool haveTarget);

}
