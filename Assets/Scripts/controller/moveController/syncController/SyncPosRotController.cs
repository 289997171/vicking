
using com.game.proto;
using UnityEngine;

/// <summary>
/// 主角向服务器发送坐标更新,一般情况下只有主角挂载该脚本
/// </summary>
public class SyncPosRotController : MonoBehaviour
{

    private GameObject personObj;

    private Transform personTra;

    private Moveable moveable;

    // 上次同步给服务器的坐标
    private Vector3 serverPos;

    private float serverRot;

    void Start()
    {
        this.personObj = this.gameObject;
        this.personTra = this.transform;
        this.moveable = this.GetComponent<Moveable>();

        this.serverPos = this.personTra.position;
        this.serverRot = this.personTra.localEulerAngles.y;
    }

    protected  void OnEnable()
    {
        // 完整消息 4 * float(4) + 2 = 18字节（每个消息）
        // 18 * 8 = 144 字节/秒 （最多）
        // 小时 = 518400 字节
        // 1M =  1024000 字节
        // 1小时最多发送0.5M
        // 即便10个角色同屏，不停的跑，也才5M/小时
        InvokeRepeating("SyncPosRot", 0.5f, 0.125f); // 每秒最多同步8次坐标
    }

    protected  void OnDisable()
    {
        CancelInvoke("SyncPosRot");
    }

    #region 主角，向服务器同步坐标
    void SyncPosRot()
    {
        if (Vector3.Distance(serverPos, transform.position) > 0.1f)
        {
            this.serverPos = transform.position;

            // TODO 发送请求
            ReqSyncPosRotMessage reqSyncPosRotMessage = new ReqSyncPosRotMessage();
            reqSyncPosRotMessage.pr = new PosRot();
            reqSyncPosRotMessage.pr.posX = this.serverPos.x;
            reqSyncPosRotMessage.pr.posY = this.serverPos.y;
            reqSyncPosRotMessage.pr.posZ = this.serverPos.z;


            float currentRot = this.personTra.localEulerAngles.y;
            if (Mathf.Abs(this.serverRot - currentRot) > 10f)
            {
                this.serverRot = currentRot;

                // TODO 发送消息
                reqSyncPosRotMessage.pr.rotY = this.serverRot;
            }

            NetManager.Instance.SendMessage(NetMessageBuilder.Encode((int)reqSyncPosRotMessage.msgID, reqSyncPosRotMessage));
        }
        else
        {
            float currentRot = this.personTra.localEulerAngles.y;
            if (Mathf.Abs(this.serverRot - currentRot) > 10f)
            {
                this.serverRot = currentRot;

                // TODO 发送消息
                ReqSyncPosRotMessage reqSyncPosRotMessage = new ReqSyncPosRotMessage();
                reqSyncPosRotMessage.pr = new PosRot();
                reqSyncPosRotMessage.pr.rotY = this.serverRot;
                NetManager.Instance.SendMessage(NetMessageBuilder.Encode((int)reqSyncPosRotMessage.msgID, reqSyncPosRotMessage));
            }
        }
    }
    #endregion
}
