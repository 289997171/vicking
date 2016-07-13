
using UnityEngine;
public class SkillItem : MonoBehaviour
{


    /// <summary>
    /// 点击使用技能
    /// </summary>
    private void OnClick()
    {
        int skillModelId = 0;
        int skillLv = 0;
        PlayerManager.Instance.LocalPlayer.useSkill(skillModelId, skillLv);
    }
}
