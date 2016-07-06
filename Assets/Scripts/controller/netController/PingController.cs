using System;
using com.game.proto;
using UnityEngine;

public class PingController : MonoBehaviour
{

    private ReqPingMessage reqPingMessage = new ReqPingMessage();

    private long lastSendTime = 0;

    private void sendPing()
    {
        if (!NetManager.Instance.GameNet.IsConnect())
        {
            // 网络断开链接了
            this.enabled = false;

            GameNetCloseEvts.Instance.OnGameNetCloseEvent();
            return;
        }

        lastSendTime = DateTime.Now.Ticks;
        NetManager.Instance.SendMessage(NetMessageBuilder.Encode((int)reqPingMessage.msgID, reqPingMessage));
    }

    public void OnPing()
    {
        // 计算延迟
        long ping = DateTime.Now.Ticks - lastSendTime;
        Debug.Log("网络延迟：" + ping);
    }


    void OnEnable()
    {
        InvokeRepeating("sendPing", 5f, 5.1f);
    }

    void OnDisable()
    {
        CancelInvoke("sendPing");
    }
}
