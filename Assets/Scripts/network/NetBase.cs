using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

/// <summary>
///     Net state.
///     网络层状态
/// </summary>
public enum NetState
{
    Null,
    UnConnected,
    Connected,
    TimeOut,
    Error
}

/// <summary>
///     游戏通信基类
/// </summary>
public class NetBase
{
#if UNITY_WP8
    #region WP8连接
    /// <summary>
    /// 尝试用TCP套接字连接到指定主机端口
    /// </summary>
    public string ConnectWP8(string hostName, int portNumber)
    {
        string result = string.Empty;
        IPEndPoint hostEntry = new IPEndPoint(IPAddress.Parse(hostName), portNumber);
        _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
        socketEventArg.RemoteEndPoint = hostEntry;

        socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
        {//连接成功
            result = e.SocketError.ToString();
            ConnectCompleted();
        });

        _clientSocket.ConnectAsync(socketEventArg);
        return result;
    }
    void ConnectCompleted()
    {
        try
        {
            if (IsConnect())
            {
                State = NetState.Connected;
                ReceiveAsync();
                Helper.Log("Socket connected to " + _clientSocket.RemoteEndPoint.ToString());
            }
            else
            {
                Helper.LogError("没有连接上服务器！");
                State = NetState.UnConnected;
            }
            //State = NetState.TimeOut;
            //Helper.LogError("Connect Error.");
            IsConnecting = false;
        }
        catch (Exception e)
        {
            State = NetState.Error;
            IsConnecting = false;
            Helper.LogError(e.ToString());
        }
    }
    #endregion
    #region WP8发送
    /// <summary>
    /// 向连接的服务器发送信息
    /// </summary>
    string SendWP8(byte[] data)
    {
        string response = "操作超时";

        //套接字是否准备好
        if (_clientSocket != null)
        {
            //套接字上下文
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();

            socketEventArg.RemoteEndPoint = _clientSocket.RemoteEndPoint;
            socketEventArg.UserToken = null;

            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                response = e.SocketError.ToString();
                msgTotalBytes += (ulong)data.Length;
                sendDone.Set();
            });
            socketEventArg.SetBuffer(data, 0, data.Length);
            sendDone.ResetToBeginning();
            _clientSocket.SendAsync(socketEventArg);
            sendDone.WaitOne();
        }
        else
        {
            response = "套接字没有准备好";
        }

        return response;
    }
    #endregion
    #region WP8接受
    /// <summary>
    /// 从连接服务器接收数据
    /// </summary>
    string ReceiveWP8()
    {
        string response = "操作超时";

        if (_clientSocket != null)
        {
            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = _clientSocket.RemoteEndPoint;

            //设置接收数据的缓冲区
            socketEventArg.SetBuffer(new byte[32 * 1024], 0, 32 * 1024);

            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                if (e.SocketError == SocketError.Success)
                {
                    // Retrieve the data from the buffer
                    //ReceiveCompleted(e);
                    //response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                    //response = response.Trim('\0');
                    msgTotalBytes += (ulong)e.BytesTransferred;
                    Array.Copy(e.Buffer, e.Offset, _recvBuff, 0, e.BytesTransferred);
                    ReadData(e.BytesTransferred);
                    if (e.BytesTransferred>0)
                    {
                        _clientSocket.ReceiveAsync(socketEventArg);
                    }
                }
                else
                {
                    response = "错误：" + e.SocketError.ToString();
                }
            });

            _clientSocket.ReceiveAsync(socketEventArg);
        }
        else
        {
            response = "套接字没有准备好";
        }

        return response;
    }
    #endregion
#endif

    // TODO 记录总的接受与发送的网络数据
    // public ulong MsgTotalBytes = 0;

    #region 基本信息

    public const int TCP_MAX_RECV = 10240;
    public const int TCP_MAX_SEND = 1024;
    public ulong msgTotalBytes = 0;

    public string Ip = "127.0.0.1";
    public int Port = 12345;

    public NetState State { get; set; }

    private IPEndPoint _serverInfo;
    private Socket _clientSocket;

    // TODO 因为客户端处理的消息反序列化与序列化量并不大，故不需要单独开辟线程处理
    // 使用多线程来处理网络接收
    // private bool _createRecvThread = true; //是否需要创建线程
    // protected Thread _thread = null;
    // ManualResetEvent _connectDone = new ManualResetEvent(false);

    #endregion

    // 是否连接中
    public bool IsConnecting;
    private static readonly ManualResetEvent sendDone = new ManualResetEvent(false);

    #region Init

    public NetBase()
    {
        State = NetState.Null;
        IsConnecting = false;

        // 定义一个IPV4，TCP模式的Socket,连接时候赋值
        // _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public void Clear()
    {
        State = NetState.Null;
        IsConnecting = false;
        // msgTotalBytes = 0;
    }

    #endregion

    #region Connect & Disconnect

    public bool IsConnect()
    {
        if (_clientSocket != null)
        {
            return _clientSocket.Connected;
        }
        return false;
    }

    /// <summary>
    ///     连接服务器
    /// </summary>
    /// <param name="Ip">服务器ip</param>
    /// <param name="Port">端口</param>
    public void Connect(string Ip, int Port)
    {
        this.Ip = Ip;
        this.Port = Port;

        Connect();
    }

    /// <summary>
    ///     连接服务器
    /// </summary>
    public void Connect()
    {
        // UIManager.Instance.ShowWaitting();

        if (IsConnecting)
        {
            throw new InvalidOperationException("连接中!" + _clientSocket);
        }

        if (IsConnect())
        {
            throw new InvalidOperationException("处于连接中!" + _clientSocket);
        }

        // IP验证
        if (
            !new Regex(
                @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))")
                .IsMatch(Ip))
        {
            var ipHost = Dns.GetHostEntry(Ip);
            Ip = ipHost.AddressList[0].ToString();
        }

        // _connectDone.Reset();

        try
        {
            //客户端连接服务端指定IP端口，Sockket
            IsConnecting = true;
#if UNITY_WP8
                ConnectWP8(Ip, Port);
#else
            IPAddress ipa;
            if (IPAddress.TryParse(Ip, out ipa))
            {
                _serverInfo = new IPEndPoint(ipa, Port);
            }
            else
            {
                var iph = Dns.GetHostEntry(Ip); // Dns.GetHostByName(Ip);
                for (var i = 0; i < iph.AddressList.Length;)
                {
                    _serverInfo = new IPEndPoint(iph.AddressList[i], Port);
                    break;
                }
            }

            // 创建TCP的Socket
            _clientSocket = new Socket(_serverInfo.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // 设置socket参数
            _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, TCP_MAX_RECV);
            _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, TCP_MAX_SEND);
            _clientSocket.NoDelay = true;

            _clientSocket.BeginConnect(_serverInfo, ConnectCallback, _clientSocket);
#endif
        }
        catch (Exception e)
        {
            IsConnecting = false;
            State = NetState.Error;
            Debug.LogError("SOCKETERROR:" + e);
            Quit();

            // TODO 显示等待
            // UIManager.Instance.ShowServerWait(true, 3);
        }

    }


    /// <summary>
    ///     连接回调
    /// </summary>
    private void ConnectCallback(IAsyncResult ar)
    {

        // UIManager.Instance.CloseWaiting();

#if !UNITY_WP8
        try
        {
            _clientSocket.EndConnect(ar);

            // _connectDone.WaitOne();

            if (IsConnect())
            {
                State = NetState.Connected;
                Debug.LogError("Socket 连接上了: " + _clientSocket.RemoteEndPoint);

                // 连接上后，同步接收数据
                ReceiveAsync();

                // Debug.Log("new thread rece~~~");
                // if (_createRecvThread) //只有没创建线程才必须创建线程
                // {
                //    _createRecvThread = false;
                //    _thread = new Thread(new ThreadStart(ReceiveAsync)); // 连接上后，使用独立线程接收获得数据
                //    _thread.IsBackground = true;
                //    _thread.Start();
                // }

                // 派发连接服务器成功事件
                //MessageCenter.Instance.SendMessage(MessageConst.OnConnectedServer, null);
            }
            else
            {
                State = NetState.UnConnected;
                Debug.LogError("没有连接上服务器！");
            }

            // _connectDone.Set();

            IsConnecting = false;
        }
        catch (Exception e)
        {
            State = NetState.Error;
            IsConnecting = false;
            Debug.LogError(e.ToString());
        }
#endif
    }


    /// <summary>
    ///     断开连接；退出
    /// </summary>
    public void Quit()
    {
        try
        {
            // if (_thread != null) _thread.Abort(); //关闭线程
            // _createRecvThread = true; //需要再次创建一个线程

            if (_clientSocket == null) return;

            if (_clientSocket.Connected)
            {
                //禁用发送和接受
                _clientSocket.Shutdown(SocketShutdown.Both);
                //关闭套接字，不允许重用
                _clientSocket.Disconnect(false);
            }
            _clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
        finally
        {
            State = NetState.Null;
            IsConnecting = false;
            _clientSocket = null;
        }
    }

    #endregion

    #region Receive

    private void ReceiveAsync()
    {
        try
        {
#if UNITY_WP8
                ReceiveWP8();
#else
            _clientSocket.BeginReceive(_recvBuff, 0, TCP_MAX_RECV, SocketFlags.None, ReceiveCallback, _clientSocket);
#endif
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
#if !UNITY_WP8
        try
        {
            // Debug.LogError("开始接受服务器消息");
            if (_clientSocket == null)
            {
                Debug.LogError("ReceiveCallback. _clientSocket is null");
                return;
            }
            if (!_clientSocket.Connected)
            {
                State = NetState.Null;
                Debug.LogError("this is _game ： _clientSocket is unConnected.");
                return;
            }

            var len = _clientSocket.EndReceive(ar);
            if (len > 0)
            {
                // Debug.LogError(“this is _game ： Get bytes : " + len);
                // MsgTotalBytes += (ulong)len;
                ReadData(len);
                _clientSocket.BeginReceive(_recvBuff, 0, _recvBuff.Length, SocketFlags.None, ReceiveCallback,
                    _clientSocket);
            }
            else
            {
                Debug.LogError("this is _game : This _clientSocket is close.");
                _clientSocket.EndReceive(ar);
                _clientSocket.Close();
            }
        }
        catch (Exception e)
        {
            if (_clientSocket != null) _clientSocket.EndReceive(ar);
            Debug.LogError("this is _game : Exception " + e);
        }
#endif
    }


    // 每次从网络层获取的数据
    private readonly byte[] _recvBuff = new byte[TCP_MAX_RECV];
    // 获取数据的缓存区；
    private byte[] _recvCache = new byte[TCP_MAX_RECV];
    // 当前缓存区所保存数据的实际长度；
    private readonly int cacheLength = 0;

    //    // 解密后的数据
    //    byte[] dataBuff = new byte[32 * 1024];
    //    int dataLength = 0;


    // 缓冲区数据的下标
    private int _cacheIndex;
    private List<byte> _oldBytes = new List<byte>(2);


    // TODO 处理接收到的数据，需要优化

    private const int MSGLENGTH = 2;
    private const int MSGID = 2;

    private const int MINPACKAGELENGTH = MSGLENGTH + MSGID;

    // 新版本short
    //        public void ReadData(int len)
    //        {
    //            _recvCache = new byte[len]; //重新分配Recvcache的内存空间
    //            Array.Copy(_recvBuff, 0, _recvCache, 0, len); //把数据读到缓存区
    //            if (_oldBytes.Count > 0) //如果有之前为解析完的消息
    //            {
    //                _oldBytes.AddRange(_recvCache); //添加到list的尾部
    //                _recvCache = _oldBytes.ToArray(); //转换为byte【】重新赋值给RecvCache
    //                _oldBytes = new List<byte>(2); //重新分配oldBytes的内存空间
    //            }
    //
    //            _cacheIndex = 0;
    //            Labelf: //加入标签，
    //            if (_recvCache.Length < MINPACKAGELENGTH) //缓冲区数据不足4，则继续等待接收
    //            {
    //                var oldMessBytes = new byte[_recvCache.Length];
    //                Array.Copy(_recvCache, _cacheIndex, oldMessBytes, 0, oldMessBytes.Length);
    //                _oldBytes.AddRange(oldMessBytes); //存入list
    //                Debug.Log("The length is too short. length: " + cacheLength);
    //                return;
    //            }
    //
    //            // int messLenth = (_recvCache[_cacheIndex + 3] & 0xFF) | ((_recvCache[_cacheIndex + 2] & 0xFF) << 8) | ((_recvCache[_cacheIndex + 1] & 0xFF) << 16) | ((_recvCache[_cacheIndex] & 0xFF) << 24);
    //            // int messLenth = MessageBuilder.GetIntBytes(_recvCache, 0);
    //            var messLenth = (_recvCache[0] << 8) | _recvCache[1];
    //            //当前的 长度的值不包含自己所占的byte大小
    //            if (_recvCache.Length - 2 < messLenth) //缓存区数据小于完整数据,数据没收受完毕
    //            {
    //                var oldMessBytes = new byte[_recvCache.Length];
    //                Array.Copy(_recvCache, _cacheIndex, oldMessBytes, 0, oldMessBytes.Length);
    //                _oldBytes.AddRange(oldMessBytes);
    //            }
    //            else if (_recvCache.Length - 2 == messLenth) //缓存区数据等于需要接受的数据，刚好接受完毕一个包的数据
    //            {
    //                var dataBuff = new byte[_recvCache.Length];
    //                Array.Copy(_recvCache, _cacheIndex, dataBuff, 0, _recvCache.Length);
    //                SaveQueueMsg(dataBuff);
    //            }
    //            else if (_recvCache.Length - 2 > messLenth) //缓存区数据大于需要接受逇数据
    //            {
    //                var dataBuff = new byte[messLenth + 2]; //加2个字节是因为增加当前的长度所占用的字节
    //                Array.Copy(_recvCache, _cacheIndex, dataBuff, 0, messLenth + 2);
    //                SaveQueueMsg(dataBuff); //取出相应的大小进行存入队列**把剩下的数据封装进list
    //                _cacheIndex += messLenth + 2;
    //                var oldRecv = new byte[_recvCache.Length - _cacheIndex];
    //                Array.Copy(_recvCache, _cacheIndex, oldRecv, 0, oldRecv.Length);
    //                _recvCache = new byte[oldRecv.Length];
    //                Array.Copy(oldRecv, 0, _recvCache, 0, oldRecv.Length);
    //                _cacheIndex = 0;
    //                goto Labelf; //到labelf进行继续循环
    //            }
    //        }


    //        private const int MSGLENGTH = 4;
    //        private const int MSGID = 4;

    // 老版本int
    public void ReadData(int len)
    {
        _recvCache = new byte[len]; //重新分配Recvcache的内存空间
        Array.Copy(_recvBuff, 0, _recvCache, 0, len); //把数据读到缓存区
        if (_oldBytes.Count > 0) //如果有之前为解析完的消息
        {
            _oldBytes.AddRange(_recvCache); //添加到list的尾部
            _recvCache = _oldBytes.ToArray(); //转换为byte【】重新赋值给RecvCache
            _oldBytes = new List<byte>(2); //重新分配oldBytes的内存空间
        }

        _cacheIndex = 0;
        Labelf: //加入标签，
        if (_recvCache.Length < MINPACKAGELENGTH) //缓冲区数据不足4，则继续等待接收
        {
            var oldMessBytes = new byte[_recvCache.Length];
            Array.Copy(_recvCache, _cacheIndex, oldMessBytes, 0, oldMessBytes.Length);
            _oldBytes.AddRange(oldMessBytes); //存入list
            Debug.Log("The length is too short. length: " + cacheLength);
            return;
        }

        //int messLenth = (_recvCache[_cacheIndex + 3] & 0xFF) | ((_recvCache[_cacheIndex + 2] & 0xFF) << 8) | ((_recvCache[_cacheIndex + 1] & 0xFF) << 16) | ((_recvCache[_cacheIndex] & 0xFF) << 24);
        int messLenth = _recvCache[0] << 8 | _recvCache[1] & 0xFF;
        //当前的 长度的值不包含自己所占的byte大小
        if (_recvCache.Length - MSGLENGTH < messLenth) //缓存区数据小于完整数据,数据没收受完毕
        {
            var oldMessBytes = new byte[_recvCache.Length];
            Array.Copy(_recvCache, _cacheIndex, oldMessBytes, 0, oldMessBytes.Length);
            _oldBytes.AddRange(oldMessBytes);
        }
        else if (_recvCache.Length - MSGLENGTH == messLenth) //缓存区数据等于需要接受的数据，刚好接受完毕一个包的数据
        {
            var dataBuff = new byte[_recvCache.Length];
            Array.Copy(_recvCache, _cacheIndex, dataBuff, 0, _recvCache.Length);
            SaveQueueMsg(dataBuff);
        }
        else if (_recvCache.Length - MSGLENGTH > messLenth) //缓存区数据大于需要接受逇数据
        {
            var dataBuff = new byte[messLenth + MSGID]; //加2个字节是因为增加当前的长度所占用的字节
            Array.Copy(_recvCache, _cacheIndex, dataBuff, 0, messLenth + MSGID);
            SaveQueueMsg(dataBuff); //取出相应的大小进行存入队列**把剩下的数据封装进list
            _cacheIndex += messLenth + MSGID;
            var oldRecv = new byte[_recvCache.Length - _cacheIndex];
            Array.Copy(_recvCache, _cacheIndex, oldRecv, 0, oldRecv.Length);
            _recvCache = new byte[oldRecv.Length];
            Array.Copy(oldRecv, 0, _recvCache, 0, oldRecv.Length);
            _cacheIndex = 0;
            goto Labelf; //到labelf进行继续循环
        }
    }


    /// <summary>
    ///     buff 包含了 数据长度 + 协议ID + 数据
    /// </summary>
    /// <param name="buff"></param>
    private void SaveQueueMsg(byte[] buff)
    {
        // TODO 将二进制数据放入队列中
        // TODO 是否需要对象池？
        var message = new NetMessage(buff);
        NetManager.Instance.EnqueueMsg(message);
    }

    #endregion

    #region Send

    public void Send(byte[] buff)
    {
        Send(buff, buff.Length);
    }

    public void Send(byte[] buff, int length)
    {
        if (_clientSocket == null || !_clientSocket.Connected)
        {
            Debug.LogError("_clientSocket is null or _clientSocket unConnected.");
            State = NetState.Null;
            //NetManager.Instance.OnOffline();
            return;
        }
        lock (this)
        {
            try
            {
#if UNITY_WP8
                SendWP8(bytes);
#else
                _clientSocket.BeginSend(buff, 0, length, SocketFlags.None, SendCallback, _clientSocket);
                sendDone.WaitOne();
#endif
                //打印消息 Helper.Log("[Send]第 "+sendIndex+" 条消息: Cmd:"+buff[0]+" .Param: "+buff[1]+" .Length:"+bytes.Length);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Quit();
            }
        }
    }

    private void SendCallback(IAsyncResult ar)
    {
#if !UNITY_WP8
        try
        {
            SocketError err;
            /*int bytesSent = */
            _clientSocket.EndSend(ar, out err);
            //MsgTotalBytes += (ulong)bytesSent;
            sendDone.Set();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception SendCallback: " + e.Data);
        }
#endif
    }

    #endregion
}
