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

     
}
