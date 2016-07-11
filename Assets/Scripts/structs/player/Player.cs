using UnityEngine;

public class Player : Person
{
    //是否是主角
    public bool isLocalPlayer = false;

    public override bool isDie()
    {

        return PlayerState.DIE.compare(this.state);
    }
}
