using com.game.proto;
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

            case (int)Protos_TestMove.ResEnterMap:// 返回服务器列表
                var resEnterMap = NetMessageBuilder.Decode<ResEnterMapMessage>(netMessage);
                ResEnterMapHandler.Execute(resEnterMap);
                break;

            case (int)Protos_TestMove.ResSyncPosRot:// 返回服务器列表
                var resSyncPosRot = NetMessageBuilder.Decode<ResSyncPosRotMessage>(netMessage);
                ResSyncPosRotHandler.Execute(resSyncPosRot);
                break;

            case (int)Protos_TestMove.ResSyncAnimator:// 返回服务器列表
                var resSyncAnimator = NetMessageBuilder.Decode<ResSyncAnimatorMessage>(netMessage);
                ResSyncAnimatorHandler.Execute(resSyncAnimator);
                break;

            case (int)Protos_TestMove.ResPing:
                var resPing = NetMessageBuilder.Decode<ResPingMessage>(netMessage);
                ResPingHandler.Execute(resPing);
                break;

            #endregion

            default:
                Debug.LogErrorFormat("无法找到消息对应的处理{0}", netMessage.MessageID);
                break;
        }
    }
}