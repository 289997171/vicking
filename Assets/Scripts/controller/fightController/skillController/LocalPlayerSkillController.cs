﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能队列中的技能信息
/// </summary>
public class SingleSkill
{
    // 技能配置
    public QSkill qSkill;

    // 技能ID
    public int skillModelId;

    // 是否必须有目标
    public bool mustTarget;

    // 最远攻击距离
    public float maxDistance = 2.5f;

    // 朝向目标的最大角度才能施放法术
    public float maxAngle = 10f;

    // 如果角度不在范围内，允许在一定时间内转向的最长时间
    public float maxTrunTime = 1f;

    // 朝目标攻击后，任然保持朝向的时间
    public float keepLookAtTime = 1.5f;

    public Targetable target;

    // 攻击目标
    // public Person target;

    public SingleSkill(QSkill qSkill, int skillModelId, bool mustTarget, float maxDistance, float maxAngle, float maxTrunTime, float keepLookAtTime, Targetable target = null)
    {
        this.qSkill = qSkill;
        this.skillModelId = skillModelId;
        this.mustTarget = mustTarget;
        this.maxDistance = maxDistance;
        this.maxAngle = maxAngle;
        this.maxTrunTime = maxTrunTime;
        this.keepLookAtTime = keepLookAtTime;
        this.target = target;
    }
}

public class LocalPlayerSkillController : SkillController
{
    private LocalPlayer localPlayer;
    private CooldownController cooldownController;
    private Moveable moveable;

    protected void Start()
    {
        base.Start();
        this.localPlayer = GetComponent<LocalPlayer>();
        this.cooldownController = GetComponent<CooldownController>();
        this.moveable = GetComponent<Moveable>();
    }

    /// <summary>
    /// 主角请求施放技能
    /// </summary>
    /// <param name="target"></param>
    public void reqCastSkill(int skillModelId, int skillLv)
    {

        if (!checkCastCondition(skillModelId, skillLv))
        {
            return;
        }

        // 发送施法请求
        //        com.game.proto.ReqUseSkillMessage useSMsg = new com.game.proto.ReqUseSkillMessage();
        //        useSMsg.SkillID = SkillID;
        //        useSMsg.TargetID = role.ID;
        //        useSMsg.TargetType = (int)role.Type;
        //        NetManager.Instance.GetGameNet().Send(ProtobufSerializer.GetInstance().ProtobufSerToBytes<com.game.proto.ReqUseSkillMessage>((object)useSMsg, (int)useSMsg.msgID));
    }

    public void resCastSkill()
    {

    }

    private List<SingleSkill> skillQueue = new List<SingleSkill>();

    void Update()
    {
        if (skillQueue.Count > 0)
        {
            if (doCastSkill(skillQueue[0]))
            {
                skillQueue.RemoveAt(0);
            }
        }
    }

    public override void castSkill(Person target, int skillModelId, int skillLv)
    {
        // 技能ID
        // int skillModelId;

        // 是否必须有目标
        bool mustTarget = true;

        // 最远攻击距离
        float maxDistance = 2.5f;

        // 朝向目标的最大角度才能施放法术
        float maxAngle = 10f;

        // 如果角度不在范围内，允许在一定时间内转向的最长时间
        float maxAngleTime = 1f;

        // 朝目标攻击后，任然保持朝向的时间
        float keepLookAtTime = 3f;

//        if (target == null)
//        {
            Targetable targetable = this.localPlayer.Selectable.selectedTarget;

            if (mustTarget)
            {

                if (targetable == null)
                {
                    Debug.LogError("没有目标");
                    return;
                }

//                target = targetable.person;
//
//                if (target == null)
//                {
//                    Debug.LogError("没有目标");
//                    return;
//                }

            }
//        }
        

        // TODO 测试
        skillQueue.Add(new SingleSkill(null, skillModelId, mustTarget, maxDistance, maxAngle, maxAngleTime, keepLookAtTime, targetable));;
    }

    private bool doCastSkill(SingleSkill singleSkill)
    {
        if (singleSkill.mustTarget)
        {
            if (!checkConditionDistance(singleSkill))
            {
                // TODO 距离不符合
                return false;
            }

            if (!checkConditionAngle(singleSkill))
            {
                // TODO 角度不符合
                return false;
            }
        }

        this.fightAnimationController.castSkill(singleSkill.skillModelId);
        Debug.LogError("施放成功");

        return true;
    }

    /// <summary>
    /// 检查距离条件
    /// </summary>
    public bool checkConditionDistance(SingleSkill singleSkill)
    {
        float distance = Vector3.Distance(singleSkill.target.transform.position, this.transform.position);
        if (distance > singleSkill.maxDistance)
        {
            if (!this.moveable.isMoving())
                this.moveable.move(singleSkill.target.transform.position);

            return false;
        } else if (this.moveable.isMoving())
        {
            this.moveable.stopMove();
        }

        return true;
    }


    /// <summary>
    /// 检查角度条件
    /// </summary>
    public bool checkConditionAngle(SingleSkill singleSkill)
    {
        singleSkill.maxTrunTime -= Time.deltaTime;
        if (singleSkill.maxTrunTime < 0f)
        {
            Debug.Log("在时间范围内转向任然失败，取消该技能施放");
            skillQueue.Remove(singleSkill);
            return false;
        }

        Vector3 dir = singleSkill.target.transform.position - this.transform.position;
        dir.y = 0f;
        dir.Normalize();
        float currentAngle = Vector3.Angle(this.transform.forward, dir);
        // Debug.LogError("currentAngle === " + currentAngle);

        if (currentAngle > singleSkill.maxAngle)
        {
            // Debug.LogError("超过角度");
            // TODO 设置朝目标看
            if (!(this.moveable as PlayerMoveController).turningForce)
            {
                (this.moveable as PlayerMoveController).turnForce(singleSkill);
            }

            return false;
        }
        //        else
        //        {
        //            if ((this.moveable as PlayerMoveController).turningForce)
        //            {
        //                (this.moveable as PlayerMoveController).turningForce = false;
        ////                (this.moveable as PlayerMoveController).trun
        //            }
        //        }

        return true;
    }



    private bool checkCastCondition(int skillModelId, int skillLv)
    {
        // 判断是否有武器


        // 死亡检测
        if (localPlayer.isDie())
        {
            AlertUtil.Instance.alert(0, "您已死亡");
            return false;
        }

        // 安全区检测

        // 停止冥想
        // ActivitiesManager.getInstance().reqStopMeditation(player);

        // 停止采集
        // NpcManager.getInstance().playerStopGather(player, 0);

        // 停止施法
        // PlayerManager.getInstance().playerStopCast(player);


        // 地图验证

        // 冷却检查
        if (cooldownController.isCooldowning(CooldownTypes.SKILL, skillModelId.ToString()))
        {
            AlertUtil.Instance.alert(0, "施法冷却中");
            return false;
        }

        // 技能判断
        Skill skill = getSkillByModelId(skillModelId);

        if (skill == null)
        {
            AlertUtil.Instance.alert(0, "您尚未习得该技能");
            return false;
        }

        // 技能配置
        QSkill skillModel;

        if (!DataManager.Instance.QskillMap.TryGetValue(skill.skillModelId + "_" + skill.skillLevel, out skillModel))
        {
            AlertUtil.Instance.alert(0, "错误的技能使用");
            return false;
        }

        // 公共冷却检查
        if (cooldownController.isCooldowning(CooldownTypes.SKILL_PUBLIC, skillModel.qPublicCdLevel.ToString()))
        {
            AlertUtil.Instance.alert(0, "公共冷却中");
            return false;
        }

        // 判断是不是定身技能
        if (skillModel.qCanMove == 0)
        {
            moveable.stopMove();
        }

        // 是否主动技能
        if (skillModel.qTriggerType != 1)
        {
            Debug.Log("尝试使用非主动技能");
            return false;
        }

        // 是否人物技能
        if (skillModel.qSkillUser != 1)
        {
            Debug.Log("尝试使用非人物技能");
            return false;
        }

        // 是否职业技能或者通用技能
        if (skillModel.qSkillJob != 0 && skillModel.qSkillJob != localPlayer.job)
        {
            AlertUtil.Instance.alert(0, "非本职业技能");
            return false;
        }

        // 是否符合等级
        if (skillModel.qNeedgrade > localPlayer.level)
        {
            AlertUtil.Instance.alert(0, "等级需要等级：" + skillModel.qNeedgrade);
            return false;
        }

        if (!skillModel.qFlash.Equals(""))
        {
            if (FighterState.compare(FighterState.CANNOT_MOVE, localPlayer.fightState))
            {
                // 施法者定身状态
                AlertUtil.Instance.alert(0, "定身中无法使用该技能");
                return false;
            }
        }

        // 消耗检查
        int costMp = 0;
        int costHp = 0;
        {
            if (skillModel.qNeedMp > 0)
            {
                if (skillModel.qNeedMpType == 0)
                {
                    // 固定魔法值
                    costMp = skillModel.qNeedMp;
                }
                else if (skillModel.qNeedMpType == 1)
                {
                    // 最大魔法值万份比
                    costMp = localPlayer.finalAbility.mpmax * skillModel.qNeedMp / 10000;
                }
                else if (skillModel.qNeedMpType == 2)
                {
                    // 当前魔法值万份比
                    costMp = localPlayer.mp * skillModel.qNeedMp / 10000;
                }
            }
            if (localPlayer.mp < costMp)
            {
                AlertUtil.Instance.alert(0, "法力不足，无法释放");
                return false;
            }

            if (skillModel.qNeedHp > 0)
            {
                if (skillModel.qNeedHpType == 0)
                {
                    // 固定魔法值
                    costHp = skillModel.qNeedHp;
                }
                else if (skillModel.qNeedHpType == 1)
                {
                    // 最大魔法值万份比
                    costHp = localPlayer.finalAbility.hpmax * skillModel.qNeedHp / 10000;
                }
                else if (skillModel.qNeedHpType == 2)
                {
                    // 当前魔法值万份比
                    costHp = localPlayer.hp * skillModel.qNeedHp / 10000;
                }
            }
            if (localPlayer.hp < costHp)
            {
                AlertUtil.Instance.alert(0, "血量不足，无法释放");
                return false;
            }
        }


        // 获得目标
        Person target = null;
        Targetable selectedTarget = localPlayer.Selectable.selectedTarget;
        if (selectedTarget != null)
        {
            target = selectedTarget.person;
        }
        else
        {
            // TODO 自动选择目标
        }


        // 目标安全区检测


        // 是敌人判断是否允许攻击,否则判断是否允许释放技能
        if (skillModel.qTarget == 4)
        {
            // （1自己，2友好目标，4敌对目标，8当前目标，16场景中鼠标的当前坐标点，32主人）
            if (!localPlayer.checkEnemy(target))
            {
                AlertUtil.Instance.alert(0, "非敌对目标无法攻击");
                return false;
            }
            // 是否能攻击
            if (!localPlayer.canAttack())
            {
                AlertUtil.Instance.alert(0, "处于无法攻击状态");
                return false;
            }
        }
        else {
            // 是否能释放技能
            if (!localPlayer.canUseSkill())
            {
                AlertUtil.Instance.alert(0, "处于沉默状态");
                return false;
            }
        }

        // 距离检查
        double distance = Vector3.Distance(localPlayer.transform.position, target.transform.position);
        if (distance > skillModel.qRangeLimit + target.radius)
        {
            AlertUtil.Instance.alert(0, "超出攻击距离");

            {
                // TODO 朝目标移动
                // TODO 设置自动攻击状态，依赖Update，判断是否靠近目标，可以攻击？

            }

            return false;
        }


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
        Skill skill;
        if (localPlayer.skills.TryGetValue(skillModelId, out skill))
        {
            return skill;
        }
        return null;
    }
}
