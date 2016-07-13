
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
    private LocalPlayer localPlayer;

    private GameObject localPlayerObj;


    // 3D摄像机，对应主角的
    private WowMainCamera wowMainCamera;

    private GameObject wowMainCameraObj;



    // 其他角色
    public Dictionary<long, GameObject> players = new Dictionary<long, GameObject>(); 

    public void createPlayer(long playerId, string playerName, int job, bool isLocalPlayer, Action<GameObject> callback = null)
    {
        StartCoroutine(_createPlayer(playerId, playerName, job, isLocalPlayer, callback));
    }

    private IEnumerator _createPlayer(long playerId, string playerName, int job, bool isLocalPlayer, Action<GameObject> callback = null)
    {
        string modelPath = "";
        if (job == 1)
        {
            modelPath = "Prefab/Player_Worrior_niutou@NewWorrior";
        }
        Object model = Resources.Load(modelPath);
        GameObject playerObj = Instantiate(model) as GameObject;
        playerObj.transform.position = Vector3.zero;
        playerObj.transform.rotation = Quaternion.identity;
        yield return 1;

        if (isLocalPlayer)
        {
            createLocalPlayer(ref playerObj, playerId, playerName);
        }
        else
        {
            createPlayer(ref playerObj, playerId, playerName);
        }

        yield return 1;

        if (callback != null) callback(playerObj);
    }

    private void createLocalPlayer(ref GameObject playerObj,long playerId, string playerName)
    {
        LocalPlayer localPlayer = playerObj.getOrAddComponent<LocalPlayer>();

        localPlayer.id = playerId;
        localPlayer.name = playerName;

        localPlayer.height = 2f;
        localPlayer.radius = 0.5f;
        localPlayer.center = new Vector3(0, localPlayer.height / 2, 0);
        localPlayer.skinWidth = 0.001f;
        localPlayer.finalAbility.speed = 6f;

        {
            // 主要控制移动
            CharacterController characterController = playerObj.getOrAddComponent<CharacterController>();
            // 设置胶囊碰撞体信息
            characterController.height = localPlayer.height;
            characterController.radius = localPlayer.radius;
            characterController.center = localPlayer.center;
            characterController.skinWidth = localPlayer.skinWidth;
        }

        // 换装
        PlayerCustomController customController = playerObj.getOrAddComponent<PlayerCustomController>();

        // 移动控制的输入来源
        // PCWASDController pcWASDController = layerObj.getOrAddComponent<PCWASDController>();
        JoysticksController joysticksController = playerObj.getOrAddComponent<JoysticksController>();


        // 只有主角才需要添加向服务器同步坐标请求
        localPlayer.SyncPosRotController = playerObj.getOrAddComponent<SyncPosRotController>();

        playerObj.getOrAddComponent<PlayerAreaController>();

        playerObj.getOrAddComponent<SyncAnimationController>();

        playerObj.getOrAddComponent<PlayerMoveAnimationController>();
        
        localPlayer.Moveable = playerObj.getOrAddComponent<PlayerMoveController>();

        // 添加允许选择组件
        localPlayer.Selectable = playerObj.getOrAddComponent<Selectable>();

        // 其他角色允许被点击
        localPlayer.Targetable = playerObj.getOrAddComponent<Targetable>();

        // 冷却控制器
        localPlayer.CooldownController = playerObj.getOrAddComponent<CooldownController>();

        playerObj.getOrAddComponent<FightAnimationController>();

        localPlayer.LocalPlayerSkillController = playerObj.getOrAddComponent<LocalPlayerSkillController>();

        playerObj.getOrAddComponent<FightController>();

        // 设置相机
        {
            // 3D 摄像机
            wowMainCameraObj = Camera.main.gameObject;
            wowMainCamera = Camera.main.gameObject.getOrAddComponent<WowMainCamera>();

            wowMainCamera.target = playerObj.transform;
            wowMainCamera.targetPerson = localPlayer;

            // 射线
            PhysicsRaycaster physicsRaycaster = wowMainCamera.gameObject.getOrAddComponent<PhysicsRaycaster>();
        }
        
    }


    private void createPlayer(ref GameObject playerObj, long playerId, string playerName)
    {
        Player player = playerObj.getOrAddComponent<Player>();

        player.id = playerId;
        player.name = playerName;

        player.height = 2f;
        player.radius = 0.5f;
        player.center = new Vector3(0, player.height / 2, 0);
        player.skinWidth = 0.001f;
        player.finalAbility.speed = 6f;

        {
            // 主要控制移动
            CharacterController characterController = playerObj.getOrAddComponent<CharacterController>();
            // 设置胶囊碰撞体信息
            characterController.height = player.height;
            characterController.radius = player.radius;
            characterController.center = player.center;
            characterController.skinWidth = player.skinWidth;
        }

        // 换装
        PlayerCustomController customController = playerObj.getOrAddComponent<PlayerCustomController>();

        // 移动控制的输入来源
        // PCWASDController pcWASDController = playerObj.getOrAddComponent<PCWASDController>();
        JoysticksController joysticksController = playerObj.getOrAddComponent<JoysticksController>();

        // 主角，非主角，都需要添加坐标改变控制器（主角用于如，因为恐惧等情况导致的位移）
        player.UpdateSyncPosRotController = playerObj.getOrAddComponent<UpdateSyncPosRotController>();

        playerObj.getOrAddComponent<UpdateSyncAnimationController>();

        playerObj.getOrAddComponent<PlayerMoveAnimationController>();


        // 添加允许选择组件
        player.Selectable = playerObj.getOrAddComponent<Selectable>();

        // 其他角色允许被点击
        playerObj.getOrAddComponent<Targetable>();

        // 冷却控制器
        player.CooldownController = playerObj.getOrAddComponent<CooldownController>();

        playerObj.getOrAddComponent<SkillController>();
    }

    public LocalPlayer LocalPlayer
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
    public void setLocalPlayer(GameObject localPlayerObj)
    {
        this.localPlayer = localPlayerObj.GetComponent<LocalPlayer>();
        this.localPlayerObj = localPlayerObj;

        // TODO ...
    }

    public void OnWalkClick(PointerEventData eventData)
    {
        Debug.Log("OnWalkClick !!!");
        if (localPlayer != null)
        {
            localPlayer.Moveable.move(eventData.pointerCurrentRaycast.worldPosition);
        }
    }

    public void playSound()
    {
        // TODO 播放音效
    }


}
