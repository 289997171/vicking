using UnityEngine;

/// <summary>
/// 移动导致的动画相关处理
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public abstract class MoveAnimationController : MonoBehaviour
{
     string idleAnimation;

    /// <summary>
    /// 空闲
    /// </summary>
    public abstract void OnIdle();


    /// <summary>
    /// 原地朝向改变
    /// </summary>
    /// <param name="dir"></param>
    public abstract void OnTurn(Quaternion rot);

    /// <summary>
    /// 奔跑
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public abstract void OnRun(float h = 0f, float v = 0f, float angle = 0f);

    /// <summary>
    /// 游泳
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public abstract void OnSwing(float h = 0f, float v = 0f);

    /// <summary>
    /// 飞行
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public abstract void OnFly(float h = 0f, float v = 0f);

    /// <summary>
    /// 跳跃
    /// </summary>
    public abstract void OnJump();
}
