using UnityEngine;

[DisallowMultipleComponent]
public abstract class SyncPosRotController : MonoBehaviour
{
    /// <summary>
    /// 同步坐标
    /// </summary>
    /// <param name="postion"></param>
    public abstract void syncPostion(Vector3 postion);

    /// <summary>
    /// 同步朝向
    /// </summary>
    /// <param name="diection"></param>
    public abstract void syncDiection(Vector3 diection);
}
