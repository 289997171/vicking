
using com.game.proto;
using UnityEngine;

public class ResSyncPosRotHandler
{
    public static void Execute(ResSyncPosRotMessage message)
    {
        // Debug.Log("接收到服务器消息：" + message);

        int personType = message.personType;
        long personId = message.personID;
        PosRot posRot = message.pr;
        float speed = message.speed;
        float time = message.time;


        if (personType == Person.PERSON_PLAYER)
        {
            // TODO 临时测试需要的，以后可以删除
            if (personId == PlayerManager.Instance.LocalPlayer.id)
            {
                return;
            }

            Player player;
            if (PlayerManager.Instance.players.TryGetValue(personId, out player))
            {
                if (posRot.posX != 0f || posRot.posY != 0f || posRot.posZ != 0f)
                {
                    player.UpdateSyncPosRotController.syncPos(new Vector3(posRot.posX, posRot.posY, posRot.posZ));
                }
                if (posRot.rotY != 0f)
                {
                    player.UpdateSyncPosRotController.syncRot(posRot.rotY);
                }
            }
        }
    }
}
