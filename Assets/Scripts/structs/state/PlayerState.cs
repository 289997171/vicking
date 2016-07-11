

/// <summary>
/// 玩家状态机
/// </summary>
public class PlayerState
{
    //NORMAL		0
    //LOGIN			1
    //QUITING		2
    //QUIT			4
    //NOTHING		0
    //FIGHT			16
    //DIE			32
    //STAND			0
    //RUN			256
    //NOREVIVE		0
    //AOTORELIVE	65536
    //REVIVEEND		0
    //REVIVESTART	1048576
    //NOTRANSACTION	0
    //TRANSACTION	131072
    //NOAUTOFIGHT	0
    //AUTOFIGHT		262144
    //INMAP			0
    //CHANGEMAP		2097152
    //NOGATHER		0
    //GATHER		4194304
    //NOCAST		0
    //CAST		    8388608
    //NOHORSESTATUS 0
    //HORSESTATUS   32768


    //======================================登录状态组 0xFFFFFFF0 | -16 | 1111111111110000
    /**
     * 正常
     */
    public static readonly State NORMAL = new State(0 /*0x00000000*/, -16/*0xFFFFFFF0*/);
    /**
     * 登陆
     */
    public static readonly State LOGIN = new State(1 /*0x00000001*/, -16/*0xFFFFFFF0*/);
    /**
     * 退出中
     */
    public static readonly State QUITING = new State(2 /*0x00000002*/, -16/*0xFFFFFFF0*/);
    /**
     * 退出
     */
    public static readonly State QUIT = new State(4 /*0x00000004*/, -16/*0xFFFFFFF0*/);
    //======================================登录状态组


    //======================================战斗状态组 0xFFFFFF0F | -241 | 1111111100001111
    /**
     * 无状态
     */
    public static readonly State NOTHING = new State(0 /*0x00000000*/, -241 /*0xFFFFFF0F*/);
    /**
     * 战斗
     */
    public static readonly State FIGHT = new State(16 /*0x00000010*/, -241 /*0xFFFFFF0F*/);
    //======================================战斗状态组

    /**
     * 死亡
     */
    public static readonly State DIE = new State(32 /*0x00000020*/, -65521 /*0xFFFF000F*/); // 0xFFFF000F | -65521 | 0000000000001111

    //======================================动作状态组
    /**
     * 站立
     */
    public static readonly State STAND = new State(0 /*0x00000000*/, -65281/*0xFFFF00FF*/); // 0xFFFF00FF | -65281 | 0000000011111111
                                                                                            /**
                                                                                            * 跑
                                                                                            */
    public static readonly State RUN = new State(256/*0x00000100*/, -65281/*0xFFFF00FF*/);

    /**
    * 走
    */
    public static readonly State WALK = new State(512/*0x00000200*/, -65281/*0xFFFF00FF*/);
    //    /**
    //     * 跳跃
    //     */
    //    JUMP(0x00000200, -65281/*0xFFFF00FF*/),
    //    /**
    //     * 二次跳跃
    //     */
    //    DOUBLEJUMP(0x00000400, -65281/*0xFFFF00FF*/),
    //    /**
    //     * 格挡准备
    //     */
    //    BLOCKPREPARE(0x00000800, -65281/*0xFFFF00FF*/),
    //    /**
    //     * 格挡
    //     */
    //    BLOCK(0x00001000, -65281/*0xFFFF00FF*/),
    /**
     * 打坐,冥想
     */
    public static readonly State SIT = new State(8192/*0x00002000*/, -65281/*0xFFFF00FF*/);
    //    /**
    //     * 游泳
    //     */
    //    SWIM(0x00004000, 0xFFFF00FF),
    //======================================动作状态组

    //======================================复活状态组 // 0xFFFF00FF | -65537 | 0000000011111111
    /**
     * 无复活
     */
    public static readonly State NOREVIVE = new State(0 /*0x00000000*/, -65537/*0xFFFEFFFF*/);
    /**
     * 自动复活
     */
    public static readonly State AOTORELIVE = new State(65536 /*0x00010000*/, -65537/*0xFFFEFFFF*/);
    //======================================复活状态组

    //======================================交易状态组
    //        /**
    //         * 非交易状态
    //         */
    //        public static readonly State NOTRANSACTION = new State(0 /*0x00000000*/, -131073 /*0xFFFDFFFF*/);
    //        /**
    //         * 交易
    //         */
    //        public static readonly State TRANSACTION = new State(131072 /*0x00020000*/, -131073 /*0xFFFDFFFF*/);
    //======================================交易状态组

    //======================================挂机状态组
    /**
     * 非挂机状态
     */
    public static readonly State NOAUTOFIGHT = new State(0 /*0x00000000*/, -262145 /*0xFFFBFFFF*/);
    /**
     * 挂机
     */
    public static readonly State AUTOFIGHT = new State(262144 /*0x00040000*/, -262145 /*0xFFFBFFFF*/);
    //======================================挂机状态组

    //======================================地图状态组
    /**
     * 地图中
     */
    public static readonly State INMAP = new State(0 /*0x00000000*/, -2097153 /*0xFFDFFFFF*/);
    /**
     * 换地图中
     */
    public static readonly State CHANGEMAP = new State(2097152 /*0x00200000*/, -2097153 /*0xFFDFFFFF*/);
    //======================================地图状态组

    //======================================采集状态组
    /**
     * 非采集
     */
    public static readonly State NOGATHER = new State(0 /*0x00000000*/, -4194305 /*0xFFBFFFFF*/);
    /**
     * 采集
     */
    public static readonly State GATHER = new State(4194304 /*0x00400000*/, -4194305 /*0xFFBFFFFF*/);
    //======================================采集状态组
    //======================================施法状态组
    /**
     * 非施法
     */
    public static readonly State NOCAST = new State(0 /*0x00000000*/, -8388609 /*0xFF7FFFFF*/);
    /**
     * 施法中
     */
    public static readonly State CAST = new State(8388608 /*0x00800000*/, -8388609 /*0xFF7FFFFF*/);
    //======================================施法状态组
    //======================================骑乘状态组
    /**
     * 非骑乘
     */
    public static readonly State NOHORSESTATUS = new State(0 /*0x00000000*/, -1048577 /*0xFFEFFFFF*/);
    /**
     * 骑乘中
     */
    public static readonly State HORSESTATUS = new State(1048576 /*0x00100000*/, -1048577 /*0xFFEFFFFF*/);
    //======================================骑乘状态组

    public static State[] states = new State[] {
            // NORMAL,
            LOGIN,
            QUITING,
            QUIT,
            // NOTHING,
            FIGHT,
            DIE,
            // STAND,
            RUN,
            WALK,
            SIT,
            // NOREVIVE,
            AOTORELIVE,
            // NOTRANSACTION,
            // TRANSACTION,
            // NOAUTOFIGHT,
            AUTOFIGHT,
            // INMAP,
            CHANGEMAP,
            // NOGATHER,
            GATHER,
            // NOCAST,
            CAST,
            // NOHORSESTATUS,
            HORSESTATUS,
        };


    public static void SetState(Player player, State state)
    {
        int stateValue = player.state;
        int oldStateValue = stateValue;
        stateValue = stateValue & state.Mark;       // 求并
        int changeStateValue = stateValue;
        stateValue = stateValue | state.Value;      // 求合

        if (oldStateValue != stateValue)
        {
            int quitStateValue = oldStateValue - changeStateValue;
            if (quitStateValue != 0)
            {
                foreach (State s in states)
                {
                    if ((quitStateValue & s.Value) != 0)
                    {
                        // TODO 退出状态 s
                        PlayerStateHandler.Exit(player, s);
                    }
                }
            }

            // TODO 进入状态 state
            if (state.Value != 0) PlayerStateHandler.Enter(player, state);
        }
        player.state = stateValue;
    }

    public static bool compare(int state1, int state2)
    {
        return ((state1 & state2) != 0);
    }
}

public class PlayerStateHandler
{
    /// <summary>
    /// 进入某个状态
    /// </summary>
    /// <param name="state"></param>
    public static void Enter(Player player, State state)
    {
        // TODO 进入某个状态
    }


    /// <summary>
    /// 退出某个状态
    /// </summary>
    /// <param name="state"></param>
    public static void Exit(Player player, State state)
    {
        // TODO 退出某个状态
    }
}
