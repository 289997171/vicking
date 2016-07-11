
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

            GameObject playerObj;
            if (PlayerManager.Instance.players.TryGetValue(personId, out playerObj))
            {
                if (posRot.posX != 0f || posRot.posY != 0f || posRot.posZ != 0f)
                {
                    playerObj.GetComponent<Player>().UpdateSyncPosRotController.syncPos(new Vector3(posRot.posX, posRot.posY, posRot.posZ));
                }
                if (posRot.rotY != 0f)
                {
                    playerObj.GetComponent<Player>().UpdateSyncPosRotController.syncRot(posRot.rotY);
                }
            }
        }
    }
}
