
using UnityEngine;

/// <summary>
/// 本地角色相应服务器坐标更新
/// </summary>
public class ResSyncPosRotController : MonoBehaviour
{
    private GameObject personObj;

    private Transform personTra;

    private Person person;

    private Moveable moveable;


    void Start()
    {
        this.personObj = this.gameObject;
        this.personTra = this.transform;
        this.person = this.GetComponent<Person>();
        this.moveable = this.GetComponent<Moveable>();
    }

    public void syncPostion(Vector3 syncPos)
    {
        if (Vector3.Distance(syncPos, transform.position) > 0.1f)
        {
            // TODO 处理坐标改变
            moveable.move(syncPos);
            // personTra.position = Vector3.Lerp(personTra.position, syncPos, Time.deltaTime * this.person.finalAbility.speed);
        }
    }

    public void syncRot(float rot)
    {
       
    }

}
