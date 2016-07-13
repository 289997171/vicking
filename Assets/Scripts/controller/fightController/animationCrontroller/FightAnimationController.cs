using System.Collections;
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

        AnimatorClipInfo[] clipInfos = this.animator.GetCurrentAnimatorClipInfo(1);
        Debug.LogError("clipInfos : " + clipInfos.Length);
        foreach (AnimatorClipInfo clip in clipInfos)
        {
            Debug.LogError("clip :: " + clip.clip.name);
        }

        StartCoroutine(resetAction());
    }

    private IEnumerator resetAction()
    {
        yield return 1;


//        AnimatorClipInfo[] clipInfos = this.animator.GetCurrentAnimatorClipInfo(1);
//        Debug.LogError("clipInfos : " + clipInfos.Length);
//        foreach (AnimatorClipInfo clip in clipInfos)
//        {
//            Debug.LogError("clip :: " + clip.clip.name);
//        }

        this.animator.SetInteger("iAction", 0);

        
    }
}
