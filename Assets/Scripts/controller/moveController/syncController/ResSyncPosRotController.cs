
using UnityEngine;

/// <summary>
/// 本地角色相应服务器坐标更新
/// </summary>
public class ResSyncPosRotController : MonoBehaviour
{
    private GameObject personObj;

    private Transform personTra;

    private Moveable moveable;


    void Start()
    {
        this.personObj = this.gameObject;
        this.personTra = this.transform;
        this.moveable = this.GetComponent<Moveable>();
    }

    public void syncPostion(Vector3 postion)
    {
        if (Vector3.Distance(postion, transform.position) > 0.1f)
        {
            // TODO 处理坐标改变
            moveable.move(postion);
        }
    }

    public void syncdirection(Vector3 direction)
    {
        if (Vector3.Distance(direction, this.personTra.rotation.eulerAngles) > 0.1f)
        {
            // TODO 处理朝向改变
            moveable.turn(direction);
        }
    }
}
