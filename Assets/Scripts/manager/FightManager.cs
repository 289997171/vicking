
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
        // 获得主角
        Player player = PlayerManager.Instance.LocalPlayer;

        // 死亡检测
        if (player.isDie())
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
        if (CooldownManager.getInstance().isCooldowning(player, CooldownTypes.SKILL, String.valueOf(skillModelID)))
        {
            AlertUtil.Instance.alert(0, "施法冷却中");
            return;
        }

        // 公共冷却检查
        if (CooldownManager.getInstance()
            .isCooldowning(player, CooldownTypes.SKILL_PUBLIC, String.valueOf(skillModel.getQPublicCdLevel())))
        {
            AlertUtil.Instance.alert(0, "公共冷却中");
            return;
        }

        // 技能判断
        Skill skill = SkillManager.Instance.getSkillByModelId(player, skillModelId);

        if (skill == null)
        {
            AlertUtil.Instance.alert(0, "您尚未习得该技能");
            return;
        }

        // 技能配置
        QSkill skillModel =
            DataManager.getInstance().getQSkillMap().get(skill.getSkillModelId() + "_" + skill.getSkillLevel());
        if (skillModel == null)
        {
            AlertUtil.Instance.alert(0, "错误的技能使用");
            return;
        }

        // 判断是不是定身技能
        if (skillModel.getQCanMove() == 0)
        {
            PlayerManager.getInstance().playerStopRun(player, false);
        }

        // 是否主动技能
        if (skillModel.getQTriggerType() != 1)
        {
            Debug.Log("尝试使用非主动技能");
            return;
        }

        // 是否人物技能
        if (skillModel.getQSkillUser() != 1)
        {
            Debug.Log("尝试使用非人物技能");
            return;
        }

        // 是否职业技能或者通用技能
        if (skillModel.getQSkillJob() != 0 && skillModel.getQSkillJob() != player.getJob().getValue())
        {
            AlertUtil.Instance.alert(0, "非本职业技能");
            return;
        }

        // 是否符合等级
        if (skillModel.getQNeedgrade() > player.getLevel())
        {
            AlertUtil.Instance.alert(0, "等级需要等级：" + skillModel.getQNeedgrade());
            return;
        }

        if (!skillModel.getQFlash().equals(""))
        {
            if (FighterState.CANNOT_MOVE.compare(player.getFightState()))
            {
                // 施法者定身状态
                MessageUtil.tell_player_message(player,
                    getFightFailedBroadcastMessage(player, skill.getSkillModelId(), 1));
                MessageUtil.notify_player(player, Notifys.ERROR, 321018);
                return;
            }
        }

        // 消耗检查
        int costMp = 0;
        int costHp = 0;
        {
            if (skillModel.getQNeedMp() > 0)
            {
                if (skillModel.getQNeedMpType() == 0)
                {
                    // 固定魔法值
                    costMp = skillModel.getQNeedMp();
                }
                else if (skillModel.getQNeedMpType() == 1)
                {
                    // 最大魔法值万份比
                    costMp = player.getFinalAbility().getMpmax()*skillModel.getQNeedMp()/10000;
                }
                else if (skillModel.getQNeedMpType() == 2)
                {
                    // 当前魔法值万份比
                    costMp = player.getMp()*skillModel.getQNeedMp()/10000;
                }
            }
            if (player.getMp() < costMp)
            {
                AlertUtil.Instance.alert(0, "法力不足，无法释放");
                return;
            }

            if (skillModel.getQNeedHp() > 0)
            {
                if (skillModel.getQNeedHpType() == 0)
                {
                    // 固定魔法值
                    costHp = skillModel.getQNeedHp();
                }
                else if (skillModel.getQNeedHpType() == 1)
                {
                    // 最大魔法值万份比
                    costHp = player.getFinalAbility().getHpmax()*skillModel.getQNeedHp()/10000;
                }
                else if (skillModel.getQNeedHpType() == 2)
                {
                    // 当前魔法值万份比
                    costHp = player.getHp()*skillModel.getQNeedHp()/10000;
                }
            }
            if (player.getHp() < costHp)
            {
                AlertUtil.Instance.alert(0, "血量不足，无法释放");
                return;
            }
        }

        // 目标安全区检测

        // 距离检查
        double distance = Vector3.Distance(player.transform.position, target.transform.position);
        if (distance > skillModel.getQRangeLimit() + target.radius)
        {
            AlertUtil.Instance.alert("超出攻击距离");
            return;
        }

        // 是敌人判断是否允许攻击,否则判断是否允许释放技能
        if (skillModel.getQTarget() == 4)
        {
            // （1自己，2友好目标，4敌对目标，8当前目标，16场景中鼠标的当前坐标点，32主人）
            if (!PlayerManager.getInstance().checkEnemy(player, target))
            {
                AlertUtil.Instance.alert(0, "非敌对目标无法攻击");
                return;
            }
            // 是否能攻击
            if (!player.canAttack())
            {
                AlertUtil.Instance.alert(0, "处于无法攻击状态");
                return;
            }
        }
        else {
            // 是否能释放技能
            if (!player.canUseSkill())
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
            playerAttack(int attckerType, long attackerId, targetType, targetID, skillModelID);
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
