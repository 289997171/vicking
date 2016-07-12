using com.game.proto;
using UnityEngine;

public class ResEnterMapHandler {


    public static void Execute(ResEnterMapMessage message)
    {
        Debug.Log("接收到服务器消息：" + message);

        bool isLocalPlayer = message.isLocalPlayer;
        long playerId = message.playerId;
        PosRot pr = message.pr;

        PlayerManager.Instance.createPlayer(playerId, "Player:" + playerId, 1, isLocalPlayer, playerObj =>
        {
            if (message.isLocalPlayer)
            {
                Debug.Log("创建主角");
                PlayerManager.Instance.setLocalPlayer(playerObj);
            }
            else
            {
                PlayerManager.Instance.players.Add(playerId, playerObj);
            }

            playerObj.transform.position = new Vector3(pr.posX, pr.posY, pr.posZ);
            playerObj.transform.rotation = Quaternion.Euler(new Vector3(0, pr.rotY, 0));
        });
        
    }

}
