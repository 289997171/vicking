using UnityEngine;


/// <summary>
/// 角色挂载点
/// </summary>
public abstract class PersonMount : MonoBehaviour
{
    protected Transform BindUp;         // 头顶
    protected Transform BindChest;      // 胸部
    protected Transform BindDown;       // 底部

    public virtual void initMount()
    {
        BindUp = transform.findInChildrens("BindUp");
        BindChest = transform.findInChildrens("BindChest");
        BindChest = transform.findInChildrens("BindDown");
    }

}
