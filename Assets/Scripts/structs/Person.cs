using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 角色基础信息
/// </summary>
[DisallowMultipleComponent]
public abstract class Person : MonoBehaviour, IMapObject
{
    private FightAnimationController figtAnimationController;

    // 类型
    public const byte PERSON_PLAYER = 1;  // 玩家
    public const byte PERSON_MONSTER = 2; // 怪物
    public const byte PERSON_NPC = 3;     // NPC
    public const byte PERSON_PET = 4;     // 宠物
    public const byte Item = 5;           // 物品
    public const byte Obj = 6;            // 其他物体
    public const byte Unknow = 255;       // 未知类型

    //创建时间
    public DateTime createTime = DateTime.Now;

    //对象类型
    public byte personType;

    //唯一ID
    public long id;

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
    public int mapModelId = 1;

    //所在线服务器
    public int line;

    //BUFF列表
    public List<Buff> buffs = new List<Buff>();

    //冷却列表
    public Dictionary<string, Cooldown> cooldowns = new Dictionary<string, Cooldown>();

    //战斗状态
    public long fightState;

    // 状态
    public int state;

    //最终计算属性
    public BaseAbility finalAbility = new BaseAbility();

    //属性部分
    public Dictionary<int, BaseAbility> attributes = new Dictionary<int, BaseAbility>();

    //旋转速度
    public float rotSpeed = 10;

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



    #region controllers

    public Moveable Moveable;

    

    public Selectable Selectable;

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

    // 角色是否已死亡
    public abstract bool isDie();

    /**
     * 检查是否能攻击
     *
     * @return boolean true 能 false 不能
     */
    public bool canAttack()
    {
        return true;
    }

    /**
     * 是否允许释放技能
     *
     * @return
     */
    public bool canUseSkill()
    {
        return true;
    }

    /**
     * 检查是否能移动
     *
     * @return boolean true 能 false 不能
     */
    public bool canMove()
    {
        return true;
    }

    /// <summary>
    /// 固定点施放技能
    /// </summary>
    /// <param name="skillModelId"></param>
    /// <param name="skillLv"></param>
    /// <param name="targetPos"></param>
    public void PlaySkill(int skillModelId, int skillLv, Vector3 targetPos)
    {

    }

    /// <summary>
    /// 目标点施放技能
    /// </summary>
    /// <param name="skillID"></param>
    /// <param name="lv"></param>
    /// <param name="targetID"></param>
    /// <param name="targetType"></param>
    public void PlaySkill(int skillID, int lv, long targetID, int targetType)
    {
//        // 1.获得技能配置
//        SkillModel skillInfo = DataManager.Instance.GetSkillByID(skillID, lv);
//        if (skillInfo == null)
//        {
//            Helper.LogError("技能空ID:" + skillID + "等级:" + lv);
//            return;
//        }
//        if (personType == PERSON_PLAYER)
//        {
//            if (((Player)this).nomalSkillID == skillID)
//                normalContinueAttactTime = 2f;
//            else
//                skillindex = -1;
//
//            // 技能是否导致震动
//            if (IsMain && skillInfo.ShakeType != 0)
//            {
//                PlayerManager.Instance.GetMainPlayer().SetShakeByType(skillInfo.ShakeType, skillInfo.ShakeDelayTime);
//            }
//        }
//       
//        playSkill(skillInfo, this, RoleManager.Instance.GetRole((uint)targetType, targetID));
        
    }

//    private void playSkill(SkillModel skillInfo, Person target)
//    {
//        // 1.播放技能音效
//        if (!LocalSetting.SysSetting.CloseMusic)
//        {
//            if (personType == PERSON_PLAYER)
//            {
//                SoundManager.Instance.PlaySoundForce(skillInfo.Sound, false);
//            }
//            else if (personType == PERSON_MONSTER)
//            {
//                MonsterBase mobj = ((Monster)this).Baseinfo;
//                if (null != mobj)
//                {
//                    // 优先播放技能音效
//                    if (!string.IsNullOrEmpty(skillInfo.Sound) && !skillInfo.Sound.Equals("0"))
//                    {
//                        SoundManager.Instance.PlaySoundForce(skillInfo.Sound, false);
//                    }
//                    // 播放怪物音效
//                    else if (!string.IsNullOrEmpty(mobj.AtkSound))
//                        SoundManager.Instance.PlaySoundForce(mobj.AtkSound, false);
//                }
//            }
//        }
//
//        // 2.技能持续时间
//        float skillLastTime = skillInfo.SkillLastTime;
//        canUseSkill = false;
//        bSkillDo = true;
//        bSkillWait = true;
//        bSkillFindWay = false;
//        
//        // 当前技能是否能移动施放
//        curSkillCanMove = skillInfo.CanMove == 1;
//        // 当前施放的技能
//        curSkillID = skillInfo.SkillID;
//
//        // 改变朝向
//        if (target != null)
//        {
//            FightDir = target.transform.position - transform.position;
//            FightDir.y = 0f;
//            FightDir.Normalized();
//               
//            
//            if (FightDir != Vector3.zero)
//            {
//                transform.rotation = Quaternion.LookRotation(FightDir);
//            }
//        }
//        else target = this;
//
//        int actionRandom = 0;
//        if (isAlive)
//        {
//            string strSound = string.Empty;
//
//            // 获得技能列表
//            for (int i = 0; i < skillInfo.skillEffects.Count; ++i)
//            {
//                Skilleffect effectI = skillInfo.skillEffects[i];
//
//                if (effectI.Dimianmofa == 1)//如果是地面魔法不在这儿处理
//                    continue;
//                if ((LocalSetting.SysSetting.HideOtherEffect || !bShowModel) && !PlayerManager.Instance.IsMainPlayer(ID) &&
//                    !(SceneManager.Instance.CurSceneType == SCENETYPE.ROOM_USAGE_ARENAPUBLIC ||
//                    SceneManager.Instance.CurSceneType == SCENETYPE.ROOM_USAGE_LEVELARENA ||
//                    SceneManager.Instance.CurSceneType == SCENETYPE.ROOM_USAGE_EXPRESS))
//                {//系统设定 屏蔽了其他玩家，则不播放特效
//                    continue;
//                }
//                if (effectI.TargetType == 2 && null == AttackRole)
//                    continue;
//                if ((LocalSetting.SysSetting.HideOtherEffect || LocalSetting.SysSetting.HideNotFriend) && User.ID != PlayerManager.Instance.GetMainPlayer().ID)//屏蔽其他玩家和屏蔽其他玩家特效
//                    continue;
//                //Helper.LogError(transform.name + "  effectName  " + EffectManager.Instance.GetEffectNameByID(effectI.EffectID));
//                Transform targetTran = effectI.TargetType == 1 ? GetBindTrans(User, effectI.BindPos) : GetBindTrans(AttackRole, effectI.BindPos);//fanchao   
//                Role affectRole = effectI.TargetType == 1 ? User : AttackRole;
//                strSound = effectI.Sound;
//                switch (effectI.TYPE)
//                {
//                    case 1:
//
//                        StartCoroutine(EffectManager.Instance.PlayerEffect(effectI.EffectID, targetTran.position, effectI.LastTime, effectI.bLoop == 1 ? true : false, User.transform.localRotation, strSound, effectI.DelayTime, effectI.Scale));
//                        break;
//                    case 2:
//                        StartCoroutine(EffectManager.Instance.PlayerEffect(effectI.EffectID, User.transform, FightDir, effectI.LastTime, effectI.bLoop == 1 ? true : false, effectI.Speed, effectI.MaxDistance, AttackRole.transform.localRotation, strSound, effectI.DelayTime, effectI.Scale));
//                        break;
//                    case 3:
//                        StartCoroutine(EffectManager.Instance.PlayerEffect(effectI.EffectID, targetTran, effectI.LastTime, effectI.bLoop == 1 ? true : false, AttackRole.transform.localRotation, strSound, effectI.DelayTime, affectRole, 1, 0, effectI.Scale));
//                        break;
//                    case 4:
//                        targetTran = GetBindTrans(AttackRole, 2);//特效攻击目标默认打目标的中心
//                        StartCoroutine(EffectManager.Instance.PlayerEffect(effectI.EffectID, GetBindPos(effectI.BindPos), targetTran, effectI.LastTime, effectI.bLoop == 1 ? true : false, effectI.Speed, effectI.MaxDistance, transform.localRotation, strSound, effectI.DelayTime, effectI.Scale));
//                        break;
//                }
//                effectI = null;
//                targetTran = null;
//            }
//
//            if (skillInfo.skillActions.Count > 1)
//            {
//                //actionRandom = Random.Range(0, skillPlay.skillActions.skillAction.Count);
//                skillindex++;
//                if (skillindex >= skillInfo.skillActions.Count || skillindex < 0)
//                    skillindex = 0;
//                actionRandom = skillindex;
//                //Helper.LogError("动作ID：" + actionRandom);
//                if (Type == OBJECT_TYPE.NTYPE_PLAYER)
//                    if (((Player)this).byOccupation == (byte)EOCCUPATION.OC_BERSERKER)
//                    {
//                        if (!((LocalSetting.SysSetting.HideOtherEffect || LocalSetting.SysSetting.HideNotFriend) && User.ID != PlayerManager.Instance.GetMainPlayer().ID))////屏蔽其他玩家和屏蔽其他玩家特效
//                        {
//                            float DelayT = 0f;
//                            StartCoroutine(EffectManager.Instance.PlayerEffect(114 + skillindex, GetBindPos(2).position, 2f, false, User.transform.localRotation, string.Empty, DelayT, 1f));
//                            string sound = "qs_AEffect0" + (skillindex + 1);
//                            SoundManager.Instance.PlaySoundForce(sound, false);
//                        }
//                    }
//                    else if (((Player)this).byOccupation == (byte)EOCCUPATION.OC_MADWARRIOR)
//                    {
//                        string sound = "zs_attack0" + (skillindex + 1);
//                        SoundManager.Instance.PlaySoundForce(sound, false);
//                    }
//                    else if (((Player)this).byOccupation == (byte)EOCCUPATION.OC_SHAMAN)
//                    {
//                        float DelayT = 0f;
//                        int bindpos = 0;
//                        if (skillindex == 0)
//                            bindpos = 5;
//                        if (skillindex == 1)
//                            bindpos = 4;
//                        if (skillindex == 2)
//                        {
//                            bindpos = 5;
//                            StartCoroutine(EffectManager.Instance.PlayerEffect(428, GetBindTrans(User, 4), 1f, true, User.transform.localRotation, string.Empty, 0, User, 1, 0, 1f));
//                        }
//                        StartCoroutine(EffectManager.Instance.PlayerEffect(428, GetBindTrans(User, bindpos), 1f, true, User.transform.localRotation, string.Empty, 0, User, 1, 0, 1f));
//                    }
//            }
//            SetAttack(true);
//            //if have many skillaction ,random one 
//            //animator.SetAttack(skillPlay.skillActions.skillAction[actionRandom].ActionID, skillPlay.skillActions.skillAction[actionRandom].Loop);
//            if (skillInfo.skillActions.Count > 0)
//            {
//                if (skillInfo.skillActions[actionRandom].SkillAction_bLoop == 1 ? true : false)
//                {
//                    if (animator != null)
//                    {
//                        if (Type == OBJECT_TYPE.NTYPE_MONSTER && skillInfo.skillActions[actionRandom].ActionID == 1 && ((Monster)this).Baseinfo.BThreeactions == 1)//BThreeactions表示是否为普通攻击三段随机的怪物 fanchao
//                        {
//                            animator.SetAttack(Random.Range(1, 4));
//                        }
//                        else
//                            animator.SetAttack(skillInfo.skillActions[actionRandom].ActionID);
//                    }
//                }
//                else
//                {
//                    if (animator != null)
//                    {
//                        if (Type == OBJECT_TYPE.NTYPE_MONSTER && skillInfo.skillActions[actionRandom].ActionID == 1 && ((Monster)this).Baseinfo.BThreeactions == 1)
//                        {
//                            animator.SetAttack(Random.Range(1, 4), skillLastTime);
//                        }
//                        else
//                        {
//                            //Helper.LogError("释放的动作ID:" + skillInfo.skillActions[actionRandom].ActionID);
//                            animator.SetAttack(skillInfo.skillActions[actionRandom].ActionID, skillLastTime);
//                        }
//                    }
//
//                }
//            }
//        }
//        //if (SceneManager.Instance.InFightShow)
//        //{
//        //	SkillInColosseum(skillInfo, lv, skillInfo.CalculateTimes, AttackRole, AutoPlay, skillPlay.LastTime, skillPlay.skillActions.skillAction[actionRandom]);
//        //}
//        //else
//        //{
//        SkillWaitTime = skillLastTime;
//        //}
//        if (skillInfo.skillActions.Count > 0)
//            SkillWaitLoop = !(skillInfo.skillActions[actionRandom].SkillAction_bLoop == 1 ? true : false);
//    }
}
