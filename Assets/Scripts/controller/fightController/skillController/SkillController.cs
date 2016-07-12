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
//        string skillKey = skillModelId +"_" + skillLv;
//
//        QSkill skillModel;
//        if (!DataManager.Instance.QskillMap.TryGetValue(skillKey, out skillModel))
//        {
//            Debug.Log("无法获得技能配置信息:" + skillKey);
//            return;
//        }

        // 播放动画
//        fightAnimationController.castSkill(skillModelId, skillLv, 1f);
    }
}
