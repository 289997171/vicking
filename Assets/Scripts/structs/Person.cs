using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色基础信息
/// </summary>
public abstract class Person : MonoBehaviour, IMapObject
{
    // 类型
    public const byte PERSON_PLAYER = 1;  // 玩家
    public const byte PERSON_MONSTER = 2; // 怪物
    public const byte PERSON_NPC = 3;     // NPC
    public const byte PERSON_PET = 4;     // 宠物

    //创建时间
    protected DateTime createTime = DateTime.Now;

    //对象类型
    protected byte personType;

    //名字
    protected String personName;

    //资源
    protected String res;

    //头像
    protected String icon;

    //模板Id
    protected int modelId;

    //老旧的
    protected int oldModelID;

    //时装模板Id
    protected int fashionModelID;

    //等级
    protected int level;

    //经验
    protected long exp;

    //当前生命
    protected int hp;

    //当前魔法
    protected int mp;

    //地图
    protected long mapId;

    //地图模板id
    protected int mapModelId;

    //所在线服务器
    protected int line;

    //方向
    protected Vector3 direction = Vector3.zero;

    //BUFF列表
    protected List<Buff> buffs = new List<Buff>();

    //冷却列表
    protected Dictionary<string, Cooldown> cooldowns = new Dictionary<string, Cooldown>();

    //战斗状态
    protected long fightState;

    //最终计算属性
    protected BaseAbility finalAbility = new BaseAbility();

    //属性部分
    protected Dictionary<int, BaseAbility> attributes = new Dictionary<int, BaseAbility>();

    // 模型半径,一般用于体形巨大的怪物上,如炎魔
    protected float modelRadius;

    public long getMapId()
    {
        return mapId;
    }

    public int getMapModelId()
    {
        return mapModelId;
    }

    public bool canSee(Person person)
    {
        return true;
    }
}
