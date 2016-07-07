using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 添加到角色身上，主角一般不需要添加
/// 依赖CharacterController的胶囊碰撞体
/// </summary>
public class PlayerClickController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("PlayerClickController ... ");
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        GameObject pointerPress = eventData.pointerPress;
        Debug.Log("pointerPress : " + pointerPress);
    }
}
