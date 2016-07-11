
public class Monster : Person
{
    //怪物类型(普通怪,中立怪物,炮台等)
    public int type;

    public override bool isDie()
    {
        return MonsterState.DIE.compare(this.state);
    }
}
