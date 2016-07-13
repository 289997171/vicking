using UnityEngine;

public class SkillController : MonoBehaviour
{
    protected Person person;

    protected FightAnimationController fightAnimationController;

    protected void Start()
    {
        this.person = GetComponent<Person>();
        this.fightAnimationController = GetComponent<FightAnimationController>();
    }

    public void castSkill(Person target, int skillModelId, int skillLv)
    {

    }
}
