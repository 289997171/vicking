
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerManager : DDOSingleton<PlayerManager>, IManager
{
    public bool Init()
    {
        return true;
    }

    // 主角
    public Player localPlayer;

    // 其他角色
    public Dictionary<long, Player> players = new Dictionary<long, Player>(); 

    public void createPlayer(long id, string name, int job, bool isLocalPlayer, Action<Player> callback = null)
    {
        StartCoroutine(_createPlayer(id, name, job, isLocalPlayer, callback));
    }

    private IEnumerator _createPlayer(long id, string name, int job, bool isLocalPlayer, Action<Player> callback = null)
    {
        string modelPath = "";
        if (job == 1)
        {
            modelPath = "Prefab/Player_Worrior_niutou@NewWorrior";
        }
        Object model = Resources.Load(modelPath);
        GameObject player = Instantiate(model) as GameObject;
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.identity;
        yield return 1;

        Player person = player.getOrAddComponent<Player>();
        person.isLocalPlayer = isLocalPlayer;

        person.id = id;
        person.name = name;

        person.height = 2f;
        person.radius = 0.5f;
        person.center = Vector3.up;
        person.skinWidth = 0.001f;
        person.finalAbility.speed = 6f;

        PlayerCustomController customController = player.getOrAddComponent<PlayerCustomController>();

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
            person.SyncPosRotController = player.getOrAddComponent<SyncPosRotController>();
        }

        // 主角，非主角，都需要添加坐标改变控制器（主角用于如，因为恐惧等情况导致的位移）
        person.UpdateSyncPosRotController = player.getOrAddComponent<UpdateSyncPosRotController>();

        yield return 1;

        if (isLocalPlayer)
        {
            player.getOrAddComponent<PlayerAreaController>();
            yield return 1;
        }

        if (job == 1)
        {
            player.getOrAddComponent<PlayerMoveAnimationController>();
            yield return 1;
        }

        if (isLocalPlayer)
        {
            player.getOrAddComponent<PlayerMoveController>();
        }
    
        yield return 1;


        

        // 设置相机
        if (isLocalPlayer)
        {
            WowMainCamera mainCamera = Camera.main.gameObject.getOrAddComponent<WowMainCamera>();
            mainCamera.target = player.transform;
        }

        if (callback != null) callback(person);
    }
}
