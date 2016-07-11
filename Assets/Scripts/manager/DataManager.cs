
using System.Collections.Generic;

public class DataManager : DDOSingleton<DataManager>, IManager
{
    private static readonly Dictionary<string, QSkill> qskillMap = new Dictionary<string, QSkill>();

    public bool Init()
    {
        // 初始化技能
        return true;
    }

    public Dictionary<string, QSkill> QskillMap
    {
        get { return qskillMap; }
    }
}
