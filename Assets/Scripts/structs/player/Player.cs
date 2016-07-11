using UnityEngine;

public class Player : Person
{
    //是否是主角
    public bool isLocalPlayer = false;

    public SyncPosRotController SyncPosRotController;

    public UpdateSyncPosRotController UpdateSyncPosRotController;

    public CooldownController CooldownController;

    public override bool isDie()
    {

        return PlayerState.DIE.compare(this.state);
    }
}
