using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 挂载到地面,
/// </summary>
public class WalkClickController : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick ... ");
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        Debug.Log("WalkClickController : " + worldPosition);

        WalkClickEvts.Instance.OnWalkClick(eventData);
    }
}
