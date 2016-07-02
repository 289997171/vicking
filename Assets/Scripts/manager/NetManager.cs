using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : DDOSingleton<NetManager>, IManager
{

    private NetBase gameNet = new NetBase();


    // 消息队列
    private Queue<NetMessage> _msgQueue;

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

    public void Connect(string ip, int port)
    {
        gameNet.Connect(ip, port);
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
    private IEnumerator _connect(string ip, int port)
    {
        Debug.LogError("开始链接服务器");
        yield return new WaitForSeconds(1f);
        gameNet.Connect(ip, port);
        while (gameNet.IsConnecting)
            yield return 0;
        if (gameNet.State == NetState.Connected)//已经连接上了
        {
            //ReqTokenLoginMessage msg = new ReqTokenLoginMessage();
            //msg.username = username;
            //msg.token = token;
            //msg.userID = userID;
            //NetManager.Instance.GameNet.Send(NetMessageBuilder.Encode((int)msg.msgID, msg));
            Debug.LogError("链接成功");
        }
        else
        {
            Debug.Log("连接失败，请检查网络.");
        }
        yield break;
    }
}
