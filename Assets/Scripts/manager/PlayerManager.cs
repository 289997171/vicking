
using System.Collections;
using UnityEngine;

public class PlayerManager : DDOSingleton<PlayerManager>, IManager
{
    public bool Init()
    {
        return true;
    }

    public void createPlayer(string name, int job, bool isLocalPlayer)
    {
        StartCoroutine(_createPlayer(name, job, isLocalPlayer));
    }

    private IEnumerator _createPlayer(string name, int job, bool isLocalPlayer)
    {
        string modelPath = "";
        if (job == 1)
        {
            modelPath = "person/player/female/Female";
        }
        Object model = Resources.Load(modelPath);
        GameObject player = Instantiate(model) as GameObject;
        yield return 1;

        Player person = player.getOrAddComponent<Player>();
        person.isLocalPlayer = isLocalPlayer;

        person.height = 2f;
        person.radius = 0.5f;
        person.center = Vector3.up;
        person.finalAbility.speed = 6f;

        if (isLocalPlayer)
        {
            player.getOrAddComponent<PCWASDController>();
            yield return 1;
        }
        else
        {
            // 非本地主角不添加WASD移动
        }

        if (isLocalPlayer)
        {
            // 只有主角才需要添加向服务器同步坐标请求
            player.getOrAddComponent<ReqSyncPosRotController>();
        }

        // 主角，非主角，都需要添加坐标改变控制器（主角用于如，因为恐惧等情况导致的位移）
        player.getOrAddComponent<ResSyncPosRotController>();
        yield return 1;

        if (isLocalPlayer)
        {
            player.getOrAddComponent<PlayerAreaController>();
            yield return 1;
        }

        if (job == 1)
        {
            player.getOrAddComponent<PlayerFemaleMoveAnimationController>();
            yield return 1;
        }

        if (isLocalPlayer)
        {
            player.getOrAddComponent<PlayerMoveController>();
        }
        else
        {
            player.getOrAddComponent<MoveController>();
        }
        yield return 1;

        // 设置相机
        if (isLocalPlayer)
        {
            WowMainCamera mainCamera = Camera.main.gameObject.getOrAddComponent<WowMainCamera>();
            mainCamera.target = player.transform;
        }
    }
}
