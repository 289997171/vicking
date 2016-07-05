using com.game.proto;
using UnityEngine;

public class ResEnterMapHandler {


    public static void Execute(ResEnterMapMessage message)
    {
        Debug.Log("接收到服务器消息：" + message);

        bool isLocalPlayer = message.isLocalPlayer;
        long playerId = message.playerId;
        PosRot pr = message.pr;

        PlayerManager.Instance.createPlayer("P" + playerId, 1, isLocalPlayer, player =>
        {
            if (message.isLocalPlayer)
            {
                PlayerManager.Instance.localPlayer = player;
            }
            else
            {
                PlayerManager.Instance.players.Add(playerId, player);
            }
        });
        
    }

}
