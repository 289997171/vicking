
using UnityEngine.EventSystems;

public class WalkClickEvts : NormalSingleton<WalkClickEvts>, IManager
{
    public delegate void WalkClickEvt(PointerEventData eventData);

    public WalkClickEvt OnWalkClick;

    public bool Init()
    {
        OnWalkClick += PlayerManager.Instance.OnWalkClick;
        return true;
    }
}
