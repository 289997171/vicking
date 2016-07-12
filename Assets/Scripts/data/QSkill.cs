
public class QSkill
{

    //技能编号_技能等级
    public string qskillIDqgrade;

    //技能编号
    public int qskillID;

    //    //技能名称
    //    
    //    public string qskillName;

    //技能等级
    public int qSkillLevel;

    //显示所需人物等级
    //    
    //    public int qShowNeedgrade;
    //武功面板上的简单描述（需支持加色，换行，加粗Html语法）
    //    
    //    
    //    public string qDesc;
    //武功面板上显示的SWF动画
    //    
    //    public string qSwf;
    //鼠标TIPS界面描述信息（支持Html语法）
    //    
    //    
    //    public string qTips;

    //学习所需人物等级
    public int qStudyNeedgrade;

    //是否默认学会（1默认学会，0不学会）

    public int qDefaultStudy;

    //学习所需技能书编号

    public int qStudyNeedbook;

    //    //技能书出处描述信息
    //    
    //    public string qBookDesc;
    //技能熟练度需求
    //    
    //    public int qSkillNeednum;

    //技能所需职业要求
    public int qSkillJob;

    //是否需要起手动作（0不需要，1需要）
    //    
    //    public int qSkillNohandsup;

    //使用者（1人物技能，2怪物技能，3宠物技能）
    public int qSkillUser;

    //使用方式（1主动技能，2被动技能 3.buff触发技能:如buff触发的单体技能一般都配成被动技能）
    public int qTriggerType;

    //使用距离限制（自身与目标之间的距离）（单位：米）

    public float qRangeLimit;

    //攻击范围(目前只能是圆形,这个值为半径).如果这个值大于0,那么就是范围攻击,否则就是单体攻击.

    public float qRangeLimit2;

    //    //触发方式（0不是被动触发，1在攻击时触发，2在挨打时触发,3攻击与挨打时都触发,4攻击前触发,5被攻击前触发，6侍宠攻击时触发，7侍宠被攻击时触发，8侍宠攻击、被攻击均触发，9主角死亡时触发）
    //    
    //    public int qPassiveAction;
    //
    //    //被动触发几率（本处填万分比的分子）
    //    
    //    public int qPassiveProb;

    //作用对象（1自己:旋风斩等只需要给自己加个BUFF的技能，2友好目标，4敌对目标，8当前目标，16场景中鼠标的当前坐标点，32主人）,一般情况下只配 1 和 4
    public int qTarget;

    //    //作用范围形状（1=单一目标点，2=单一直线类型（疾光电影），3=半月弯刀作用范围，4=刺杀剑术作用范围，5=野蛮冲撞技能范围，6=十方斩技能范围，7=抗拒火环技能范围，8=3*3伤害魔法区域（爆裂火焰、冰咆哮），9=火墙术技能区域，10=地狱雷光技能区域，11=焰天火雨技能区域，12=魄冰刺技能区域，13=3*3增益魔法区域（集体遁形术、幽灵战甲术、群体治愈术），14=直接对自身释放的技能（破血狂杀、瞬息移动、抗拒火环、魔法盾、遁形术、妙影无踪、召唤骨卫、召唤神兽、召唤超级骨卫）15=战士近战单体技能（攻杀、烈火、普通攻击、莲月））16=治愈术 17=神兽吐息 18=9*9范围（触龙神环形攻击、剧毒）19=半径20格（赤月地刺）20=半径40格（压力吸引）
    //    
    //    public int qAreaShape;

    //作用范围中心点（1自身为中心，2目标为中心）,需要与q_target对应,当q_target = 1的时候,必须是1
    public int qAreaTarget;

    //作用人数上限,默认99
    public int qTargetMax = 99;

    //是否可以设为自动施放技能（0不可设为自动释放技能，1可以设为自动释放技能）
    //    
    //    public int qDefaultEnable;
    //是否可以注册快捷栏（1可以，2不可以）
    //    
    //    public int qShortcut;

    //冷却时间
    public int qCd;

    //公共冷却时间（毫秒）
    public int qPublicCd;

    //公共冷却层级
    public int qPublicCdLevel;

    //    //该技能互斥技能编号
    //    
    //    public int qRestriction;
    //    //是否触发一次战斗公式的伤害（0不触发，1触发）
    //    
    //    public int qTriggerFigthHurt;

    //技能加伤百分比（万分比）,默认为10000,表示无加成与减成  7000 表示只能造成  伤害*0.7 的 15000 伤害*1.5
    public int qHurtCorrectFactor;

    //技能加成攻击力值
    public int qAttackAddition;

    //目标血量伤害系数(万分比）
    public int qHpRatio;

    //    //技能等级
    //    
    //    public int qGrade;

    //学习或升级技能所需人物等级
    public int qNeedgrade;

    //学习或升级技能需要的  内功等级.目前版本为历练
    public int qNeedng;

    //学习或升级技能需要的  内功等级.目前版本为历练
    public int qNeedMoney;

    //======================施放技能需要的魔法值或者血量值==========================

    //使用消耗魔法值
    public int qNeedMp;

    //使用消耗魔法值(0是普通魔法硬值 1是消耗最大魔法值百分比 2是消耗当前魔法值百分比)
    public int qNeedMpType;

    //使用消耗血量值
    public int qNeedHp;

    //使用消耗血量值(0是普通血量硬值 1是消耗最大血量值百分比 2是消耗当前血量值百分比)
    public int qNeedHpType;

    //======================施放技能需要的魔法值或者血量值==========================
    //    //显示类型（1近战单攻，2近战群攻，3远程单攻，4远程群攻，5单体辅助，6群体辅助，7单体控制，8群体控制，9持续伤害）
    //    
    //    public int qShowtype;

    //技能类型（1，普通技能2，地面瞬时技能 3，地面持续技能 4，永久被动技能,5，预警技能 ）
    public int qSkiitype;

    //每次造成怪物仇恨值
    public int qEnmity;

    //技能造成无视防御伤害值,也就是穿透值 1-10000之间
    public int qIgnoreDefence;

    //使用技能否为施法者添加buff（格式：BUFF编号;BUFF编号）
    public string qAddAtkBuff;

    //只是为施法者添加buff的技能,不计算任何伤害的.如旋风斩,实际上只是通过技能添加了一个buff,之后的伤害完全是因为buff出来的单体技能造成的.  0 不是  1 是(必须要配置buff)
    //如, 旋风斩,往往在这种情况下. q_target=1   q_area_target=1
    //作用对象（1自己:旋风斩等只需要给自己加个BUFF的技能，2友好目标，4敌对目标，8当前目标，16场景中鼠标的当前坐标点，32主人）,一般情况下只配 1 和 4
    public int qAddAtkBuffOnly;

    //命中后,给目标+的buff
    public string qAddTargetBuff;

    //命中后,只是给目标+的buff,无需计算伤害.如(减速怪物,只需要给怪物添加一个buff,但不计算任何伤害)
    public int qAddTargetBuffOnly;

    //    //成功施加BUFF系数（万分比）
    //    
    //    public int qBufqTriggerFactor;
    //技能调用攻击动作编号
    //    
    //    public int qAttackId;

    //延迟命中时间（单位：毫秒）
    public int qDelayTime;

    //弹道飞行速度（单位：米/秒）,一定要和客户端的一直,不然就会出现,客户端子弹已经命中,但迟迟不显示伤害,或还未命中就已经显示伤害
    public int qTrajectorySpeed;

    //技能施法特效编号

    public string qUseEffect;
    //技能弹道特效编号

    public string qTrajectoryEffect;
    //技能命中特效编号

    public string qHitEffect;
    //技能持续特效编号

    public string qSeriesEffect;

    //技能小图标编号（36*36）

    public int qSmallIco;

    //技能施法音效

    public string qUseSound;
    //技能命中音效

    public string qHitSound;

    //最大等级
    public int qMaxLevel;

    //是否自动锁定（0不锁定，1锁定）
    public int qSkillBoautolocked;

    //锁定目标是否切换(0否1是）
    public int qSkillLockedchange;

    //触发脚本编号,BOSS怪释放技能高级算法实现.
    public int qScript;

    //是否预警技能
    public int iswarning;

    //预警时间（毫秒）
    public int warningTime;

    //预警特效
    public int warningEffectid;

    //预警坐标类型（1.周围50个坐标刷新15个坐标）
    public int warningPosType;

    //怪物释放技能时是否说话（0：不说话 1：只有怪物释放该技能时说话 2：只有宠物施放该技能时说话 3：只有玩家施放改技能时说话 4：任何人施放该技能时说话）
        
    public int qSay;
    
    //说话内容
    public string qSayContent;
    

    //客户端技能播放时间,在这段时间内,怪物不能移动,攻击...理解为施法条
    public int qSkillLastTime;

    //是否允许移动施法,旋风斩就是移动施法
    public int qCanMove;

    // 一使用技能就位移(以自为中心)
    // 如:1,3,1,100 表示,以攻击者朝被攻击者的方向,击退3米,类似LOL中VN的E技能,击退,   1不转向 2转向 3移动补偿  玩家冲锋,需要转向,  怪物被玩家技能推开,不需要转向.   100表示,每产生1米的位移,动作播放的时间
    // 如:0,3,2,100 表示,以被攻击者朝攻击者的方向,拉过来3米,类似WOW中DK的死亡之握,  1不转向 2转向 3移动补偿  玩家冲锋,需要转向,  怪物被玩家技能推开,不需要转向.    100表示,每产生1米的位移,动作播放的时间
    public string qFlash;

    // 技能命中后位移
    // 如:1,3,1,100 表示,以攻击者朝被攻击者的方向,击退3米,类似LOL中VN的E技能,击退,   1不转向 2转向 3移动补偿  玩家冲锋,需要转向,  怪物被玩家技能推开,不需要转向.   100表示,每产生1米的位移,动作播放的时间
    // 如:0,3,2,100 表示,以被攻击者朝攻击者的方向,拉过来3米,类似WOW中DK的死亡之握,  1不转向 2转向 3移动补偿  玩家冲锋,需要转向,  怪物被玩家技能推开,不需要转向.    100表示,每产生1米的位移,动作播放的时间
    public string qTargetFlash;

    // 技能命中后位移
    // 如:1,3,1,100 表示,以攻击者朝被攻击者的方向,击退3米,类似LOL中VN的E技能,击退,   1不转向 2转向 3移动补偿  玩家冲锋,需要转向,  怪物被玩家技能推开,不需要转向.   100表示,每产生1米的位移,动作播放的时间
    // 如:0,3,2,100 表示,以被攻击者朝攻击者的方向,拉过来3米,类似WOW中DK的死亡之握,  1不转向 2转向 3移动补偿  玩家冲锋,需要转向,  怪物被玩家技能推开,不需要转向.    100表示,每产生1米的位移,动作播放的时间
    public string qSelfFlash;

    //地图魔法持续时间,对应q_skiitype = 3的情况下才配置,如火墙,末日降临等
    public int qMapMagicLastTime;

    //施法者血量改变值
    public int qAtkHpChange;

    //施法者血量改变值,依赖施法者当前的最大血量的百分比.如配置1000 表示玩家最大血量 * 0.1
    public int qAtkHpChangePercent;

    //技能作用的角度1-4之间(0 表示以前的360°  1 左右22.5°表示正前方45°   2 左右 2 * 22.5° 正前方 90°   3  左右 3 * 22.5° 正前方  135°  4 坐标 4 * 22.5°表示 正前方180°)
    public int qAngle;

    //技能作用的方向,默认值为0.表示当前方向. 0-7之间. 1表示当前朝向左45° 2 表示当前朝向左90° 3 表示当前朝向左135° 4表示当前朝向左180°(向后)  7
    public int qDir;

    //被动技能 增加属性配置 dcmin:50,dcmax:50,mcmin:40,mcmax:40,acmin:40,acmax:40,macmin:50,macmax:50
    public string qBonus;

    //升级技能需要的消耗
    public string qGoodxiaohao;

    //加血百分比
    public int qMatkHpChangePercent;

    //命中加成
    public int qScoreaHit;

    //无伤害的预警
    public int qWarningNoHit;


    // TODO
    //技能对应的动画
    public string qAnimation;
}
