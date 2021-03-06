﻿using com.game.proto;
using UnityEngine;

public class ResSyncAnimatorHandler
{
    public static void Execute(ResSyncAnimatorMessage message)
    {
        // Debug.Log("接收到服务器消息：" + message);
        long playerId = message.playerID;
        if (playerId  == PlayerManager.Instance.LocalPlayer.id)
        {
            return;
        }
        GameObject playerObj;
        if (PlayerManager.Instance.players.TryGetValue(playerId, out playerObj))
        {
            UpdateSyncAnimationController controller = playerObj.GetComponent<UpdateSyncAnimationController>();
            controller.SyncAimator(message.animatorInfo);
        }
    }
}
