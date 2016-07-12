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






  

}
