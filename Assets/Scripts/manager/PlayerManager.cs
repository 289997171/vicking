
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

public class PlayerManager : DDOSingleton<PlayerManager>, IManager
{
    public bool Init()
    {
        return true;
    }

    // 主角
    private Player localPlayer;

    private GameObject localPlayerObj;


    // 3D摄像机，对应主角的
    private WowMainCamera wowMainCamera;

    private GameObject wowMainCameraObj;



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
        person.center = new Vector3(0, person.height / 2, 0);
        person.skinWidth = 0.001f;
        person.finalAbility.speed = 6f;

        // 主要控制移动
        CharacterController characterController = player.getOrAddComponent<CharacterController>();
        // 设置胶囊碰撞体信息
        characterController.height = person.height;
        characterController.radius = person.radius;
        characterController.center = person.center;
        characterController.skinWidth = person.skinWidth;

        // 换装
        PlayerCustomController customController = player.getOrAddComponent<PlayerCustomController>();

        if (isLocalPlayer)
        {
            // 移动控制的输入来源
            // player.getOrAddComponent<PCWASDController>();
            player.getOrAddComponent<JoysticksController>();
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
        else
        {
            // 主角，非主角，都需要添加坐标改变控制器（主角用于如，因为恐惧等情况导致的位移）
            person.UpdateSyncPosRotController = player.getOrAddComponent<UpdateSyncPosRotController>();
        }

        yield return 1;

        if (isLocalPlayer)
        {
            player.getOrAddComponent<PlayerAreaController>();
            yield return 1;
        }

        if (isLocalPlayer)
        {
            player.getOrAddComponent<SyncAnimationController>();
        }
        else
        {
            player.getOrAddComponent<UpdateSyncAnimationController>();
        }
        yield return 1;

        if (job == 1)
        {
            player.getOrAddComponent<PlayerMoveAnimationController>();
            yield return 1;
        }

        if (isLocalPlayer)
        {
            person.Moveable = player.getOrAddComponent<PlayerMoveController>();
        }
    
        yield return 1;

        // 添加允许选择组件
        person.Selectable = player.getOrAddComponent<Selectable>();

        if (isLocalPlayer)
        {
            // 
        }
        else
        {
            // 其他角色允许被点击
            player.getOrAddComponent<Targetable>();
        }

        if (isLocalPlayer)
        {
            // 冷却控制器
            person.CooldownController = player.getOrAddComponent<CooldownController>();
        }



        // 设置相机
        if (isLocalPlayer)
        {
            // 3D 摄像机
            wowMainCameraObj = Camera.main.gameObject;
            wowMainCamera = Camera.main.gameObject.getOrAddComponent<WowMainCamera>();

            wowMainCamera.target = player.transform;
            wowMainCamera.targetPerson = person;

            // 射线
            PhysicsRaycaster physicsRaycaster = wowMainCamera.gameObject.getOrAddComponent<PhysicsRaycaster>();
        }

        if (callback != null) callback(person);
    }

    public Player LocalPlayer
    {
        get
        {
            return localPlayer;
        }
    }

    /// <summary>
    /// 设置主角
    /// </summary>
    /// <param name="player"></param>
    public void setLocalPlayer(Player player)
    {
        this.localPlayer = player;
        this.localPlayerObj = player.gameObject;

        // TODO ...
    }

    public void OnWalkClick(PointerEventData eventData)
    {
        Debug.Log("OnWalkClick !!!");
//        if (localPlayer != null)
//        {
//            localPlayer.Moveable.move(eventData.pointerCurrentRaycast.worldPosition);
//        }
    }

    public void playSound()
    {
        // TODO 播放音效
    }

}
