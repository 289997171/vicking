    using UnityEngine;
/// <summary>
/// 控制九宫格区域改变
/// </summary>
[DisallowMultipleComponent]
public abstract class AreaController : MonoBehaviour
{
    public abstract void OnMove(Vector3 oldPos, Vector3 position);
}
