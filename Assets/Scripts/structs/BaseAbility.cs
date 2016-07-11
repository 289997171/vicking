public class BaseAbility
{
    //物理攻击下限
    public int dcmin;
    //物理攻击上限
    public int dcmax;
    //魔法攻击下限
    public int mcmin;
    //魔法攻击上限
    public int mcmax;
    //物理防御下限
    public int acmin;
    //物理防御上限
    public int acmax;
    //魔法防御下限
    public int macmin;
    //魔法防御上限
    public int macmax;
    //闪避
    public int juckmin;
    //闪避
    public int juckmax;
    //命中率
    public int hitmin;
    //命中率
    public int hitmax;
    //韧性
    public int toughnessmin;
    //韧性
    public int toughnessmax;
    //暴击几率
    public int critmin;
    //暴击几率
    public int critmax;
    //暴击值
    public int critvaluemin;
    //暴击值
    public int critvaluemax;
    //格挡下限
    public int gedangmin;
    //格挡上限
    public int gedangmax;
    //自动回血
    public int autohp;
    //自动回魔法值
    public int automp;
    //最大血量
    public int hpmax;
    //最大魔法
    public int mpmax;
    //升级的最大经验值
    public long expmax;
    //速度
    public float speed;

    //穿透
    public int damagemin;
    //穿透
    public int damagemax;

    //物理减伤
    public int dcharmdelmin;
    //物理减伤
    public int dcharmdelmax;
    //魔法减伤
    public int mcharmdelmin;
    //魔法减伤
    public int mcharmdelmax;

    //物理反伤
    public int undcharmdelmin;
    //物理反伤
    public int undcharmdelmax;
    //魔法反伤
    public int unmcharmdelmin;
    //魔法反伤
    public int unmcharmdelmax;

    //吸血属性
    public int drains;

    //--------------上面需要传到前端，下面不需要----------------//
    //经验加成
    public int expmultiple;

    //属性提供的战斗力
    public int fight;
}
