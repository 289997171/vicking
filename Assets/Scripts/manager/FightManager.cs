using UnityEngine;

public class FightManager : DDOSingleton<FightManager>, IManager
{
    public bool Init()
    {
        return true;
    }

//    /// <summary>
//    /// 服务器反馈施放技能
//    /// </summary>
//    public void resCastSkill(com.game.proto.ResBeginSkillMessage SkillResult)
//    {
//        if (SkillResult.CasterType == Person.PERSON_MONSTER)
//        {
//            resMonsterCastSkill(castSkill);
//        } else if (SkillResult.CasterType == Person.PERSON_PLAYER)
//        {
//            resPlayerCastSkill(SkillResult);
//        }
//
//    }
//
//
//    /// <summary>
//    /// 玩家施放技能
//    /// </summary>
//    /// <param name="attckerType"></param>
//    /// <param name="attackerId"></param>
//    /// <param name="targetType"></param>
//    /// <param name="targetID"></param>
//    /// <param name="skillModelID"></param>
//    public void resPlayerCastSkill(com.game.proto.ResBeginSkillMessage SkillResult)
//    {
//        Player obj = PlayerManager.Instance.GetPlayer(SkillResult.CasterID);
//        if (obj != null)
//        {
//            if (SkillResult.TargetID != 0) //如果x和y都为0，那么当前攻击为锁定目标攻击，否则为地点攻击，不锁定目标.
//            {
//                // 锁定攻击
//                obj.PlaySkill(SkillResult.SkillID, SkillLevel, SkillResult.TargetID, SkillResult.TargetType);
//            }
//            else
//            {
//                // 非锁定攻击 
//                obj.PlaySkill(SkillResult.SkillID, SkillLevel, new Vector3((float)SkillResult.x, 0, (float)SkillResult.y));
//            }
//
//
//            if (PlayerManager.Instance.LocalPlayer.id == attackerId)
//            {
//                // 本地角色攻击
//                // 设置技能CD等信息
//            }
//        }
//    }
//
//    /// <summary>
//    /// 怪物施放技能
//    /// </summary>
//    public void resMonsterCastSkill(com.game.proto.ResBeginSkillMessage SkillResult)
//    {
//        // 1.找到施法怪物
//        Monster obj = MonsterManager.Instance.GetMonster(SkillResult.CasterID);
//
//        // 2.
//        if (obj == null)
//        {
//            Debug.Log("无法找到施法怪物：" + SkillResult.CasterID);
//            return;
//        }
//
//        // 3.
//        if (!obj.IsAlive && obj.wMp > 0) //增加一个保护，如果怪物是死亡状态，这个时候收到服务器返回的攻击消息，再加上怪物血量大于0，那么让他是活着状态。
//        {
//            obj.SetIsAlive(true);
//        }
//
//        // 4.怪物停止移动
//        obj.Moveable.stopMove();
//
//        // 5.播放技能
//        if (SkillResult.TargetID != 0) //如果x和y都为0，那么当前攻击为锁定目标攻击，否则为地点攻击，不锁定目标.
//        {
//            //非锁定攻击
//            obj.PlaySkill(SkillResult.SkillID, SkillLevel, SkillResult.TargetID, SkillResult.TargetType);
//        }
//        else 
//        {
//            //锁定攻击
//            obj.PlaySkill(SkillResult.SkillID, SkillLevel, new Vector3((float)SkillResult.x, 0, (float)SkillResult.y));
//        }
//    }

}
