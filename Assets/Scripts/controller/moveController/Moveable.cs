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

    /// <summary>
    /// 是否处于移动中
    /// </summary>
    /// <returns></returns>
    public abstract bool isMoving();

    /// <summary>
    /// 处理转向
    /// </summary>
    /// <param name="direction"></param>
    public abstract bool turn(Vector3 direction);

    /// <summary>
    /// 停止转向
    /// </summary>
    /// <returns></returns>
    public abstract bool stopTrun();

    /// <summary>
    /// 是否转向中
    /// </summary>
    /// <returns></returns>
    public abstract bool isTurning();
}
