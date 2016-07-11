
/// <summary>
/// 战斗状态
/// </summary>
public class FighterState
{
    // 无法移动
    public const long CANNOT_MOVE = (0x00000001); // 1

    // 无法攻击
    public const long CANNOT_ATTACK = (0x00000002); // 2

    // 无法被攻击
    public const long CANNOT_BEATTACK = (0x00000004); // 4

    // 无法使用技能
    public const long CANNOT_USESKILL = (0x00000008); // 8

    // 无法使用物品
    public const long CANNOT_USEGOODS = (0x00000010); // 16

    // 无法隐身
    public const long CANNOT_HIDE = (0x00000020); // 32

    // 无法加血
    public const long CANNOT_ADDHP = (0x00000040); // 64

    // 无法加蓝
    public const long CANNOT_ADDMP = (0x00000080); // 128

    // 256 

    // 隐身中
    public const long ON_HIDE = (0x00001000); // 4096

    // 保护盾(吸收伤害)
    public const long ON_SHIELD = (0x00002000); // 8192

    // 免疫(删除当前所有DEBUFF,并且免疫之后的所有DEBUFF)
    public const long ON_IMMUNE = (0x00004000); // 16384

    // 死亡中
    public const long ON_DIE = (0x00008000); // 32768

    // 狂暴
    public const long ON_CRAZY = (0x00010000); // 65536

    //    // 自动复活中
    //    ON_AOTORELIVE(0x00010000),        // 65536

    // 恐惧(这个状态,仅仅是乱跑的状态,真实的恐惧状态应该是 131099 =  CANNOT_MOVE | CANNOT_ATTACK | CANNOT_USESKILL | CANNOT_USEGOODS | ON_FEAR)
    public const long ON_FEAR = (0x00020000); // 131072

    // 和平状态,无法被玩家攻击,一旦攻击他人将删除该状态
    public const long ON_PICE = (0x00040000);//262144

    // 被攻击加成状态 该状态下受到的攻击会加成
    public const long ON_EMBATTLEDUP = (0x00080000);//524288

    // 攻击加成状态 该状态下受到的攻击会加成
    public const long ON_ATTACKUP = (0x00100000);//1048576

    public static bool compare(long state1, long state2)
    {
        return ((state1 & state2) != 0);
    }
}
