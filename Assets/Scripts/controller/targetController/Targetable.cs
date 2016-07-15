using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Targetable 的子类定义了目标可以被选择
/// Selectable 的子类定义了目标可以选择（一般只有LocalPlayer主角才能有该功能）
/// 可以被选择的目标,如，玩家，怪物，NPC，当前场景中某些特定的物体也可以被选择（地面，floor，walk不能添加）
/// 被选择的物体，必须有碰撞器
/// </summary>
[RequireComponent(typeof(Collider))]
public class Targetable : MonoBehaviour, IPointerClickHandler
{

    // 辅助边缘距离计算
    // 物体半径
    public float zRadius = 0f;
    public Vector3 centerOffs = Vector3.zero;

    // 是否是Person类型（可以为NULL)
    public Person person;

    // Assign to this a delegate to respond to this object being destroyed
    public event EventCenter.GeneralCallback onDestroy
        {
            add { onDestroyInvoke += value; Debug.Log("onDestroy +++++++"); }
            remove { onDestroyInvoke -= value; Debug.Log("onDestroy ------"); }
        }
    private EventCenter.GeneralCallback onDestroyInvoke; // Because iOS doesn't have a JIT - Multi-cast function pointer.

    protected void Start()
    {
        // 判断是否是角色
        this.person = GetComponent<Person>();
        if (this.person != null)
        {
            this.zRadius = this.person.radius;
            this.centerOffs = this.person.center;
        }
        // 其他物体
        else
        {
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                CapsuleCollider cap = col as CapsuleCollider;
                if (cap != null)
                {
                    zRadius = cap.radius * transform.localScale.z;
                    centerOffs = cap.center;
                }
                else
                {
                    SphereCollider sphere = col as SphereCollider;
                    if (sphere != null)
                    {
                        zRadius = sphere.radius * transform.localScale.z;
                        centerOffs = sphere.center;
                    }
                    else
                    {
                        BoxCollider box = col as BoxCollider;
                        if (box != null)
                        {
                            zRadius = box.size.z * transform.localScale.z / 2f;
                            centerOffs = box.center;
                        }
                    }
                }
            }
        }
    }

    protected void OnDestroy()
    {
        if (onDestroyInvoke != null) onDestroyInvoke(this, null);
    }

    /// <summary>
    /// 子类可以覆盖，获得当前可选对象的类型
    /// </summary>
    /// <returns></returns>
    public virtual byte TargetableType()
    {
        if (this.person != null) return this.person.personType;
        return Person.Unknow;
    }

    /// <summary>
    /// 点击该对象与之交互的时候，是否必须面对它
    /// </summary>
    /// <returns></returns>
    public virtual bool MustFaceToInteract()
    {
        return true;
    }

    /// <summary>
    /// 只能是主摄像机发射的射线，也就是或，这个函数一般情况下只能是主角的点击请求
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Targetable OnPointerClick : " + eventData.pointerCurrentRaycast.gameObject);

        PlayerManager.Instance.LocalPlayer.Selectable.SelectTarget(this);

        faceToTarget();
    }


    private void faceToTarget()
    {
        Vector3 direction = this.transform.position - PlayerManager.Instance.LocalPlayer.transform.position;
        direction.y = 0f;
        direction.Normalize();

        PlayerManager.Instance.LocalPlayer.Moveable.turn(direction);
    }
}
