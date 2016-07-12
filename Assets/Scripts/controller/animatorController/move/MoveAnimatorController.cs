
/// <summary>
/// 移动相关动画控制器
/// </summary>
public abstract class MoveAnimatorController : AnimatorController
{
    public abstract void setIdleType(int idleType);

    public abstract void setH(float h);

    public abstract void setV(float v);

    public abstract void setRuning(bool runing);

    public abstract void setAttacking(bool inAttack);

    public abstract void setHaveTarget(bool haveTarget);
}
