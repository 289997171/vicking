
using UnityEngine;

public class FightManager : DDOSingleton<FightManager>, IManager
{
    public bool Init()
    {
        return true;
    }

    /// <summary>
    /// 玩家攻击
    /// </summary>
    /// <param name="target">攻击方</param>
    /// <param name="skillModelId">技能模版ID</param>
    public void reqPlayerAttack(Person target, int skillModelId)
    {
        // 获得主角（也只能是主角才能发送使用技能请求）
        LocalPlayer localPlayer = PlayerManager.Instance.LocalPlayer;
        CooldownController cooldownController = localPlayer.CooldownController;

        // 死亡检测
        if (localPlayer.isDie())
        {
            AlertUtil.Instance.alert(0, "您已死亡");
            return;
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
            return;
        }

        // 技能判断
        Skill skill = SkillManager.Instance.getSkillByModelId(skillModelId);

        if (skill == null)
        {
            AlertUtil.Instance.alert(0, "您尚未习得该技能");
            return;
        }

        // 技能配置
        QSkill skillModel;

        if (!DataManager.Instance.QskillMap.TryGetValue(skill.skillModelId + "_" + skill.skillLevel, out skillModel))
        {
            AlertUtil.Instance.alert(0, "错误的技能使用");
            return;
        }

        // 公共冷却检查
        if (cooldownController.isCooldowning(CooldownTypes.SKILL_PUBLIC, skillModel.qPublicCdLevel.ToString()))
        {
            AlertUtil.Instance.alert(0, "公共冷却中");
            return;
        }

        // 判断是不是定身技能
        if (skillModel.qCanMove == 0)
        {
            localPlayer.Moveable.stopMove();
        }

        // 是否主动技能
        if (skillModel.qTriggerType != 1)
        {
            Debug.Log("尝试使用非主动技能");
            return;
        }

        // 是否人物技能
        if (skillModel.qSkillUser != 1)
        {
            Debug.Log("尝试使用非人物技能");
            return;
        }

        // 是否职业技能或者通用技能
        if (skillModel.qSkillJob != 0 && skillModel.qSkillJob != localPlayer.job)
        {
            AlertUtil.Instance.alert(0, "非本职业技能");
            return;
        }

        // 是否符合等级
        if (skillModel.qNeedgrade > localPlayer.level)
        {
            AlertUtil.Instance.alert(0, "等级需要等级：" + skillModel.qNeedgrade);
            return;
        }

        if (!skillModel.qFlash.Equals(""))
        {
            if (FighterState.compare(FighterState.CANNOT_MOVE, localPlayer.fightState))
            {
                // 施法者定身状态
                AlertUtil.Instance.alert(0, "定身中无法使用该技能");
                return;
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
                return;
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
                return;
            }
        }

        // 目标安全区检测

        // 距离检查
        double distance = Vector3.Distance(localPlayer.transform.position, target.transform.position);
        if (distance > skillModel.qRangeLimit + target.radius)
        {
            AlertUtil.Instance.alert(0, "超出攻击距离");
            return;
        }

        // 是敌人判断是否允许攻击,否则判断是否允许释放技能
        if (skillModel.qTarget == 4)
        {
            // （1自己，2友好目标，4敌对目标，8当前目标，16场景中鼠标的当前坐标点，32主人）
            if (!localPlayer.checkEnemy(target))
            {
                AlertUtil.Instance.alert(0, "非敌对目标无法攻击");
                return;
            }
            // 是否能攻击
            if (!localPlayer.canAttack())
            {
                AlertUtil.Instance.alert(0, "处于无法攻击状态");
                return;
            }
        }
        else {
            // 是否能释放技能
            if (!localPlayer.canUseSkill())
            {
                AlertUtil.Instance.alert(0, "处于沉默状态");
                return;
            }
        }

        // 发送请求

    }

    public void resPlayerAttack(int attckerType, long attackerId, int targetType, long targetID, int skillModelID)
    {
        if (PlayerManager.Instance.LocalPlayer.id == attackerId)
        {
            // 本地角色攻击
            localPlayerAttack(targetType, targetID, skillModelID);
        }
        else
        {
            playerAttack(attckerType, attackerId, targetType, targetID, skillModelID);
        }
    }

    private void localPlayerAttack(int targetType, long targetID, int skillModelID)
    {
        Player player = PlayerManager.Instance.LocalPlayer;
    }

    private void playerAttack(int attckerType, long attackerId, int targetType, long targetID, int skillModelID)
    {

    }
}
