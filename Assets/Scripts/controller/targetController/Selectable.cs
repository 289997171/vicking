using UnityEngine;

/// <summary>
/// Targetable 的子类定义了目标可以被选择
/// Selectable 的子类定义了目标可以选择（一般只有Person类才能有该功能）
/// </summary>
public class Selectable : MonoBehaviour
{

    // 当前选择的目标
    public Targetable selectedTarget { get; set; }

    // 当自己被销毁后，目标的销毁事件需要取消当前监听
    protected void OnDestroy()
    {
        if (selectedTarget != null)
        {
            selectedTarget.onDestroy -= OnTargetedDestroyed;
        }
    }

    public virtual void ClearTarget()
    {
        Debug.Log("ClearTarget: " + selectedTarget);
        
        if (selectedTarget == null) return;

        // 清除掉队列中的技能
        // actor.ClearQueuedSkill(); 

        // 停止交互
        // StopInteract();

        selectedTarget.onDestroy -= OnTargetedDestroyed;

        selectedTarget = null;
    }


    public virtual void SelectTarget(Targetable t)
    {
        Debug.Log("SelectTarget: " + t);

        if (selectedTarget != t) ClearTarget(); // 清理以前的目标

        if (t == null) return;

        selectedTarget = t;

        selectedTarget.onDestroy += OnTargetedDestroyed;
    }

    // 当目标被销毁
    private void OnTargetedDestroyed(object obj, object[] args)
    {
        Targetable t = obj as Targetable;
        if (selectedTarget == t && selectedTarget != null)
        {
            ClearTarget();
        }
    }
}
