using System;
using System.Collections;
using System.Collections.Generic;
using com.game.proto;
using UnityEngine;

public class NetManager : DDOSingleton<NetManager>, IManager
{
    // 网络基础
    private NetBase gameNet = new NetBase();

    // 消息队列
    private Queue<NetMessage> _msgQueue;

    // Ping相关处理
    private PingController pingController;

    public NetBase GameNet
    {
        get { return gameNet; }
        set { gameNet = value; }
    }


    public bool Init()
    {
        _msgQueue = new Queue<NetMessage>();
        return _msgQueue != null;
    }

    public void Connect(string ip, int port, Action<NetState> callback)
    {
        StartCoroutine(connect(ip, port, callback));
    }

    public void Quit()
    {
        if (gameNet != null)
            gameNet.Quit();
    }

    private void FixedUpdate()
    {
        DoMessage(); // 处理消息
    }

    private void DoMessage()
    {
        while (_msgQueue.Count > 0)
        {
            NetMessageHandler.OnMessage(DequeueMsg());
        }
    }

    public void EnqueueMsg(NetMessage msg)
    {
        lock (this)
        {
            _msgQueue.Enqueue(msg);
        }
    }

    public void ClearAllMessage()
    {
        lock (this)
        {
            _msgQueue.Clear();
        }
    }

    public NetMessage DequeueMsg()
    {
        lock (this)
        {
            return _msgQueue.Dequeue();
        }
    }

    public void SendMessage(byte[] data)
    {
        gameNet.Send(data, data.Length);
    }

    /// <summary>
    /// 连接游戏服务器
    /// </summary>
    private IEnumerator connect(string ip, int port, Action<NetState> callback)
    {
        Debug.LogError("开始链接服务器");
        yield return new WaitForSeconds(1f);
        gameNet.Connect(ip, port);
        while (gameNet.IsConnecting)
            yield return 0;

        if (gameNet.State == NetState.Connected)
        {
            Debug.LogError("链接成功");

            // ping，链接成功后,开启ping
            pingController = this.gameObject.getOrAddComponent<PingController>();
            pingController.enabled = true;
        }
        else
        {
            Debug.Log("连接失败，请检查网络.");

            // ping，链接失败后,禁用ping
            pingController = this.gameObject.getOrAddComponent<PingController>();
            pingController.enabled = false;
        }

        callback(gameNet.State);
    }


    public void OnPing()
    {
        pingController.OnPing();
    }


    //#if UNITY_EDITOR

    [SerializeField] private string host = "192.168.1.77";
    [SerializeField] private int port = 7777;

    void OnGUI()
    {
        if (NetManager.Instance.GameNet.State != NetState.Connected)
        {
            if (GUI.Button(new Rect(100, 10, 100, 30), "链接服务器"))
            {
                NetManager.Instance.Connect(host, port, (netstate) =>
                {
                    if (netstate == NetState.TimeOut)
                    {
                        Debug.LogError("链接超时");
                        return;
                    }

                    if (netstate == NetState.Error)
                    {
                        Debug.LogError("链接异常");
                        return;
                    }

                    if (netstate == NetState.Connected)
                    {
                        Debug.LogError("链接成功");
                        ReqEnterMapMessage reqEnterMapMessage = new ReqEnterMapMessage();
                        NetManager.Instance.SendMessage(NetMessageBuilder.Encode((int)reqEnterMapMessage.msgID, reqEnterMapMessage));
                    }

                });
            }
        }
    }
//#endif
    
}
