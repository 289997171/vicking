using System;
using UnityEngine;

/// <summary>
/// 冷却类型
/// </summary>
public class CooldownTypes
{
    // 总公共冷却
    public const string PUBLIC = "PUBLIC";
    // 药品公共冷却
    public const string DRUG_PUBLIC = "DRUG_PUBLIC";
    // 药品冷却
    public const string DRUG = "DRUG";
    // 技能公共冷却
    public const string SKILL_PUBLIC = "SKILL_PUBLIC";
    // 技能冷却
    public const string SKILL = "SKILL";
    // 技能触发公共冷却
    public const string SKILL_TRIGGER_PUBLIC = "SKILL_TRIGGER_PUBLIC";
    // 技能触发冷却
    public const string SKILL_TRIGGER = "SKILL_TRIGGER";
    // 包裹整理冷确
    public const string BAG_CLEAR = "BAG_CLEAR";
    // 仓库整理冷确
    public const string STORE_CLEAR = "STORE_CLEAR";
}

/// <summary>
/// 冷却控制器,为什么不用CooldownManager？因为Cooldown一般情况下只用于主角
/// </summary>
[RequireComponent(typeof (Person))]
public class CooldownController : MonoBehaviour
{
    private Person person;

    protected void Start()
    {
        this.person = GetComponent<Person>();
    }

    /// <summary>
    /// 添加冷却
    /// </summary>
    /// <param name="cooldownType"></param>
    /// <param name="key"></param>
    /// <param name="delay"></param>
    public void addCooldown(string cooldownType, string key, long delay)
    {
        string cooldownKey = "";
        //初始化冷却关键字
        if (key == null)
        {
            cooldownKey = cooldownType;
        }
        else
        {
            cooldownKey = cooldownType + "_" + key;
        }
        Cooldown cooldown;
        if (person.cooldowns.TryGetValue(cooldownKey, out cooldown))
        {
            cooldown.start = DateTime.Now.Ticks;
            cooldown.delay = delay;
        }
        else
        {
            //初始化冷却信息
            cooldown = new Cooldown();
            cooldown.type = cooldownType;
            cooldown.key = cooldownKey;
            cooldown.start = DateTime.Now.Ticks;
            cooldown.delay = delay;
            //添加冷却
            person.cooldowns.Add(cooldownKey, cooldown);
        }
    }

    /// <summary>
    /// 获得冷却
    /// </summary>
    /// <param name="cooldownType"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public long getCooldownTime(string cooldownType, string key)
    {
        // 初始化冷却关键字
        string cooldownKey = null;
        if (key == null)
        {
            cooldownKey = cooldownType;
        }
        else {
            cooldownKey = cooldownType + "_" + key;
        }
        Cooldown cooldown;

        if (person.cooldowns.TryGetValue(cooldownKey, out cooldown))
        {
            return cooldown.getRemainTime();
        }
        return 0;
    }

    /// <summary>
    /// 移除冷却
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    public void removeCooldown(string cooldownType, String key)
    {
        //初始化冷却关键字
        String cooldownKey = null;
        if (key == null)
        {
            cooldownKey = cooldownType;
        }
        else {
            cooldownKey = cooldownType + "_" + key;
        }

        person.cooldowns.Remove(cooldownKey);
    }

    /// <summary>
    /// 是否存在这种冷却类型
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool isExistCooldownType(string cooldownType, String key)
    {
        //初始化冷却关键字
        String cooldownKey = null;
        if (key == null)
        {
            cooldownKey = cooldownType;
        }
        else {
            cooldownKey = cooldownType + "_" + key;
        }
        //是否存在
        if (person.cooldowns.ContainsKey(cooldownKey))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 是否在冷却中
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool isCooldowning(string cooldownType, String key)
    {
        //初始化冷却关键字
        String cooldownKey = null;
        if (key == null)
        {
            cooldownKey = cooldownType;
        }
        else {
            cooldownKey = cooldownType + "_" + key;
        }

        Cooldown cooldown;

        if (person.cooldowns.TryGetValue(cooldownKey, out cooldown))
        {
            return DateTime.Now.Ticks <= cooldown.start + cooldown.delay;
        }

        return false;
    }
}
