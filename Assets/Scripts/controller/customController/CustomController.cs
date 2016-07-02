using UnityEngine;
using System.Collections;

/// <summary>
/// 换装
/// </summary>
[DisallowMultipleComponent]
public abstract class CustomController : MonoBehaviour
{
    // 头部
    public const string Head = "Head";

    // 胸部
    public const string Body = "Body";

    // 肩膀
    public const string Clavicle = "Clavicle";

    // 手部
    public const string Hand = "Hand";

    // 裙子（大腿）
    public const string Skirt = "Skirt";

    // 脚部（小腿）
    public const string Foot = "Foot";

    // 披风
    public const string Cape = "Cape";

    public abstract void changeHead(string head);

    public abstract void changeBody(string body);

    public abstract void changeClavicle(string clavicle);

    public abstract void changeHand(string hand);

    public abstract void changeSkirt(string skirt);

    public abstract void changeFoot(string foot);

    public abstract void changeCape(string cape);
}
