
using System.Collections.Generic;

public class LocalPlayer : Player
{

    // 前PK模式0-和平 1-组队 2-帮会 3-帮会战 4-善恶 5-全体 6-战场模式A组 7-战场模式B组
    public int prePkState;

    /**
     * 技能,技能模版ID，同类型的技能，最高等级的技能
     */
    public Dictionary<int, Skill> skills = new Dictionary<int, Skill>();

    
    public LocalPlayerSkillController LocalPlayerSkillController;


    /**
     * 检查该人物是否是自己的敌对目标
     *
     * @param player
     * @param person
     * @return boolean true 敌对目标 false 不是
     */
    public bool checkEnemy(Person person)
    {
        if (person.id == this.id)
        {
            return false;
        }

        if (person.personType == PERSON_NPC) {
            return false;
        }

        if (person.personType == PERSON_PLAYER) {
            if (person.level < Global.NEWBIE_LEVEL)
            {
                AlertUtil.Instance.alert(0, "目标处于新手保护期");
                return false;
            }
        } 
        else {
            // TODO 目前版本中立怪物永远无法被攻击

            int type = ((Monster)person).type;
            if (type == 6)
            {
                return false;
            }
            else if (type == 11 && pkState == PkStateType.PKMODEL_BATTLE_GROUP_A)
            { // A组防御塔
                return false;
            }
            else if (type == 12 && pkState == PkStateType.PKMODEL_BATTLE_GROUP_B)
            { // B组防御塔
                return false;
            }
        }

        // PK状态验证
        switch (pkState)
        {
            case PkStateType.PKMODEL_PEACEMODE: // 和平模式：只对怪物进行攻击起效（不包括玩家宠物）
            {
                return person.personType == PERSON_MONSTER;
            }
            case PkStateType.PKMODEL_TEAMMODE: // 队伍模式：对怪物以及非本队伍的玩家进行攻击起效
                {
                    if (person.personType == PERSON_MONSTER) {
                        return true;
                    } else if (person.personType == PERSON_PLAYER)
                    {
                        return true; // !TeamManager.getInstance().isSameTeam(player, (Player)person);
                    } else {
                        return false;
                    }
                }
            case PkStateType.PKMODEL_GUILDMODE: // 行会模式：对怪物以及非本行会的玩家进行攻击起效
                {
                    if (person.personType == PERSON_MONSTER) {
                        return true;
                    } else if (person.personType == PERSON_PLAYER) {
                        // TODO 帮会相关
                        //long guildId = GuildManager.getInstance().getGuildId(player);
                        //if (guildId == 0 || guildId != GuildManager.getInstance().getGuildId((Player)person))
                        //{
                            return true;
                        //}
                        //else {
                        //    return false;
                        //}
                    } else {
                        return false;
                    }
                }
            case PkStateType.PKMODEL_GOODANDEVILMODE: // 善恶模式：对怪物以及无保护状态的玩家进行攻击起效（灰名，红名玩家,包含灰名，红名玩家的宝宝）
                {
                    if (person.personType == PERSON_MONSTER) {
                        return true;
                    } else {
                        return person.personType == PERSON_PLAYER && (/*(Player) person).getPkRunTime() != 0 ||*/((Player)person).pkSword >= 200);
                    }
                }
            case PkStateType.PKMODEL_ALLTHEMODE: // 全体模式：对所有怪物和玩家进行攻击起效
                {
                    return true;
                }
            case PkStateType.PKMODEL_BATTLE_GROUP_A: // 战场PK模式A组,如果对方是玩家,那么只能攻击PK模式是B组的玩家
                {
                    if (person.personType == PERSON_PLAYER) {
                        return ((Player)person).pkState == PkStateType.PKMODEL_BATTLE_GROUP_B;
                    }
                    return true;
                }
            case PkStateType.PKMODEL_BATTLE_GROUP_B: // 战场PK模式B组,如果对方是玩家,那么只能攻击PK模式是A组的玩家
                {
                    if (person.personType == PERSON_PLAYER) {
                        return ((Player)person).pkState == PkStateType.PKMODEL_BATTLE_GROUP_A;
                    }
                    return true;
                }
        }
        return true;
    }

    public void useSkill(int skillModelId, int skillLv)
    {
        // this.LocalPlayerSkillController.reqCastSkill();
    }
}
