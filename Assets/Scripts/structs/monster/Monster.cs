
public class Monster : Person
{
    public override bool isDie()
    {
        return MonsterState.DIE.compare(this.state);
    }
}
