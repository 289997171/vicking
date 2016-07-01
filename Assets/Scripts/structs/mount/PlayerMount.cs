using UnityEngine;

/// <summary>
/// 角色挂载点
/// </summary>
public class PlayerMount : PersonMount
{
    protected Transform BindLeftHand;       // 左手
    protected Transform BindRightHand;      // 右手


    public override void initMount()
    {
        base.initMount();

        BindLeftHand = transform.findInChildrens("BindLeftHand");
        BindRightHand = transform.findInChildrens("BindRightHand");
    }
}
