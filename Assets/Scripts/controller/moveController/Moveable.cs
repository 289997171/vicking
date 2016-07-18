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
    /// 当前朝向移动
    /// </summary>
    /// <param name="value">移动值</param>
    /// <param name="speed">移动速度</param>
    /// <param name="canNav">true：类似魔兽世界战士冲锋，如果有阻挡的情况，会转弯，false的情况下，移动到第一个转角</param>
    /// <returns></returns>
    public abstract bool moveBy(Vector3 value, float speed, bool canNav);


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

    /// <summary>
    /// 是否允许改变朝向
    /// </summary>
    /// <param name="canMove"></param>
    public abstract void setCanMove(bool canMove);

    protected bool fly;

    /// <summary>
    /// 是否飞行中
    /// </summary>
    /// <param name="fly"></param>
    public void setFly(bool fly)
    {
        this.fly = fly;
    }

    protected bool onGround;

    public void setOnGround(bool onGround)
    {
        this.onGround = onGround;
    }
}
