using UnityEngine;

/// <summary>
/// 网络链接断开触发
/// </summary>
public class GameNetCloseEvts : NormalSingleton<GameNetCloseEvts>, IManager
{

    public delegate void GameNetClose();

    public GameNetClose OnGameNetCloseEvent;

    public bool Init()
    {
        OnGameNetCloseEvent += OnGameNetClose;
        return true;
    }

    public void OnGameNetClose()
    {
        Debug.Log("OnGameNetClose");

        if (PlayerManager.Instance.LocalPlayer != null)
        {
            PlayerManager.Instance.LocalPlayer.gameObject.SetActive(false);
        }
    }
}