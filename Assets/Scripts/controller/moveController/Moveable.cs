using UnityEngine;


/// <summary>
/// 移动相关处理
/// </summary>
[DisallowMultipleComponent]
public abstract class Moveable : MonoBehaviour
{
    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="endPos"></param>
    public abstract bool move(Vector3 endPos);


    /// <summary>
    /// 停止移动
    /// </summary>
    public abstract bool stopMove();
}
