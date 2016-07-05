
using com.game.proto;
using UnityEngine;

public class ResSyncPosRotHandler
{
    public static void Execute(ResSyncPosRotMessage message)
    {
        Debug.Log("接收到服务器消息：" + message);

        int personType = message.personType;
        long personId = message.personID;
        PosRot posRot = message.pr;
        float speed = message.speed;
        float time = message.time;


        if (personType == Person.PERSON_PLAYER)
        {
            Player player;
            if (PlayerManager.Instance.players.TryGetValue(personId, out player))
            {
                if (posRot.posX != 0f || posRot.posY != 0f || posRot.posZ != 0f)
                {
                    player.ResSyncPosRotController.syncPostion(new Vector3(posRot.posX, posRot.posY, posRot.posZ));
                } else if (posRot.rotY != 0f)
                {
                    player.ResSyncPosRotController.syncRot(posRot.rotY);
                }
                else
                {
                    Debug.LogError("既没同步坐标，又没同步朝向");
                }
            }
        }
    }
}
