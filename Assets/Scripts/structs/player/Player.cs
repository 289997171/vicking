using UnityEngine;

public class Player : Person
{
    //是否是主角
    public bool isLocalPlayer = false;

    //职业
    public int job;

    // PK值.(红名值)
    public int pkSword;

    // PK模式 0-和平 1-组队 2-帮会 3-帮会战 4-善恶 5-全体 6-战场模式A组 7-战场模式B组
    public int pkState;

    public SyncPosRotController SyncPosRotController;

    public UpdateSyncPosRotController UpdateSyncPosRotController;

    public CooldownController CooldownController;

    public Targetable Targetable;

    public override bool isDie()
    {

        return PlayerState.DIE.compare(this.state);
    }

    public bool canAttack()
    {
        return !FighterState.compare(FighterState.CANNOT_ATTACK, this.fightState);// && !PlayerState.compare(PlayerState.CHANGEMAP.Value, this.state);
    }

    public bool canUseSkill()
    {
        return !FighterState.compare(FighterState.CANNOT_USESKILL, this.fightState);// && !PlayerState.compare(PlayerState.CHANGEMAP.Value, this.state);
    }

    public bool canMove()
    {
        return !FighterState.compare(FighterState.CANNOT_MOVE, this.fightState);// && !PlayerState.compare(PlayerState.CHANGEMAP.Value, this.state);
    }
}
