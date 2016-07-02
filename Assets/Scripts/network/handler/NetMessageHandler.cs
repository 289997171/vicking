using UnityEngine;

public class NetMessageHandler
{
    private static bool find = true;

    public static void OnMessage(NetMessage netMessage)
    {
        //Debug.LogErrorFormat("收到消息{0}", netMessage.MessageID);
        find = true;
        // TODO 消息派发
        switch (netMessage.MessageID)
        {
                #region 例子

                //                case (int)Protos_Login.ResServerInfos:// 返回服务器列表
            //                    var ResServerInfos = NetMessageBuilder.Decode<ResServerInfosMessage>( netMessage);
            //                    ResServerInfosHandler.Execute(ResServerInfos);
            //                    break;

                #endregion

            default:
                Debug.LogErrorFormat("无法找到消息对应的处理{0}", netMessage.MessageID);
                break;
        }
    }
}