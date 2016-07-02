using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色基础信息
/// </summary>
[DisallowMultipleComponent]
public abstract class Person : MonoBehaviour, IMapObject
{
    // 类型
    public const byte PERSON_PLAYER = 1;  // 玩家
    public const byte PERSON_MONSTER = 2; // 怪物
    public const byte PERSON_NPC = 3;     // NPC
    public const byte PERSON_PET = 4;     // 宠物

    //创建时间
    public DateTime createTime = DateTime.Now;

    //对象类型
    public byte personType;

    //名字
    public String personName;

    //资源
    public String res;

    //头像
    public String icon;

    //模板Id
    public int modelId;

    //老旧的
    public int oldModelID;

    //时装模板Id
    public int fashionModelID;

    //等级
    public int level;

    //经验
    public long exp;

    //当前生命
    public int hp;

    //当前魔法
    public int mp;

    //地图
    public long mapId;

    //地图模板id
    public int mapModelId;

    //所在线服务器
    public int line;

    //BUFF列表
    public List<Buff> buffs = new List<Buff>();

    //冷却列表
    public Dictionary<string, Cooldown> cooldowns = new Dictionary<string, Cooldown>();

    //战斗状态
    public long fightState;

    //最终计算属性
    public BaseAbility finalAbility = new BaseAbility();

    //属性部分
    public Dictionary<int, BaseAbility> attributes = new Dictionary<int, BaseAbility>();

#region CharacterController needs
    //模型高度
    public float height = 2f;

    //模型半径
    public float radius = 0.5f;

    //模型中心点
    public Vector3 center = Vector3.up;
    
    // 皮肤材质宽度（如果值太大，会影响人物的Y坐标，导致悬空的情况）
    public float skinWidth = 0.001f;
#endregion

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
