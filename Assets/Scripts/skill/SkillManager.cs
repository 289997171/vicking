public class SkillManager : UnitySingleton<SkillManager>, IManager
{
    public bool Init()
    {
        return true;
    }

    /// <summary>
    /// 请求学习技能
    /// </summary>
    public void reqStudySkill(int skillModelid, int skillLv)
    {
    }

    /// <summary>
    /// 响应
    /// </summary>
    public void resStudySkill()
    {
    }

    /// <summary>
    /// 是否拥有技能
    /// </summary>
    /// <param name="skillModelId"></param>
    /// <returns></returns>
    public bool isHaveSkill(int skillModelId)
    {
        return true;
    }

    /// <summary>
    /// 按模板Id获得技能
    /// </summary>
    /// <param name="skillModelId"></param>
    /// <returns></returns>
    public Skill getSkillByModelId(int skillModelId)
    {
        return null;
    }

}