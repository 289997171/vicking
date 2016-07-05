
using UnityEngine;

/// <summary>
/// 本地角色相应服务器坐标更新
/// </summary>
public class UpdateSyncPosRotController : MonoBehaviour
{
    private GameObject personObj;

    private Transform personTra;

    private Person person;

    private Moveable moveable;

    private bool updatePos = false;

    private Vector3 serverPos;

    private bool updateRot = false;

    private float serverRot;

    private Quaternion playerNewRot;

    // 是否在地面上
    private bool onGround = false;
    private CollisionFlags flags;

    void Start()
    {
        this.personObj = this.gameObject;
        this.personTra = this.transform;
        this.person = this.GetComponent<Person>();
        this.moveable = this.GetComponent<Moveable>();
    }

    public void syncPos(Vector3 syncPos)
    {
        if (Vector3.Distance(syncPos, transform.position) > 0.1f)
        {
            // TODO 处理坐标改变
            // moveable.move(syncPos);

            serverPos = syncPos;
            updatePos = true;
        }
    }

    public void syncRot(float syncRot)
    {
        if (Mathf.Abs(personTra.localEulerAngles.y - syncRot) > 0.1f)
        {
            // TODO 处理角度改变
            // moveable.turn(new Vector3(0, serverRot, 0));
            
            playerNewRot = Quaternion.Euler(new Vector3(0, syncRot, 0));
            serverRot = syncRot;
            updateRot = true;
        }
    }


    void Update()
    {
        if (updatePos)
        {
            personTra.position = Vector3.Lerp(personTra.position, serverPos, Time.deltaTime * this.person.finalAbility.speed);

            if (Vector3.Distance(personTra.position, serverPos) < 0.1f)
            {
                personTra.position = serverPos;
                updatePos = false;
            }
        }

        if (updateRot)
        {
            personTra.rotation = Quaternion.Slerp(personTra.rotation, playerNewRot, 0.2f);
            if (Mathf.Abs(personTra.localEulerAngles.y - serverRot) < 0.4f)
            {
                personTra.rotation = playerNewRot;
                updateRot = false;
            }
        }
    }

}
