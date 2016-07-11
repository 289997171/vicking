
public class Global
{
    /**
     * 万分比
     */
    public const int MAX_PROBABILITY = 10000;

    /**
     * 最大等级
     */
    public const int MAX_LEVEL = 70;

    /**
     * 初始背包数量
     */
    public const int DEFAULT_BAG_CELLS = 24;

    /**
     * 最大背包数量
     */
    public const int DEFAULT_BAG_CELLS_MAX = 64;
    /**
     * 小金库的最大格子数
     */
    public const int DEFAULT_BAG_TEMP_MAX = 432;

    /**
     * 玩家等级超过怪物等级不再掉落东西
     */
    public const int DEFAULT_DROP_LEVELC_I = 15;

    /**
     * 掉落物品在地图存在时间周期
     */
    public const int DEFAULT_DROP_LOSETIME = 2 * 10 * 1000;

    /**
     * 掉落物品在地图存在玩家专属时间周期
     */
    public const int DEFAULT_DROP_LOSEPLAYERTIME = 2 * 10 * 1000;

    // 初始仓库数量
    public const int DEFAULT_STORE_CELLS = 10;

    // 最大仓库数量
    public const int DEFAULT_STORE_CELLS_MAX = 125;

    // 击杀精英怪物有效任务计数所需的伤害比例
    public static double TASK_EFFECTIVE_JINYIN_DAMAGE_RATIO = 0.01;

    // 击杀怪物有效任务计数所需的伤害比例
    public static double TASK_EFFECTIVE_DAMAGE_RATIO = 0.02;

    // boss死亡后站立时间
    public const int BOSS_DIEING = 300000;

    // 自动回复间隔
    public const long RECOVER_TIME = 20000;

    // 与NPC的交易/对话距离(米)
    public const float NPC_TRADE_DISTANCE = 8;

    // 怪物回血间隔
    public static int MONSTER_RECOVERY_INTERVALTIME = 5 * 1000;

    // BUFF最小作用间隔
    public static int BUFF_INTERVAL = 500;

    // 新手保护等级
    public static int NEWBIE_LEVEL = 18;

    // 战斗状态过期时间.也就是说,6秒内,玩家没攻击,或被攻击,那么玩家退出战斗状态
    public static int FIGHT_OVERDUE = 6000;

    // 组队BUFF距离限制
    public static int TEAM_BUFF = 10;
}
