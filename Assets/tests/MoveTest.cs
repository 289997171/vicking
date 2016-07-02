

using System.Collections;
using UnityEngine;

public class MoveTest : MonoBehaviour
{


    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "创建主角"))
        {
            PlayerManager.Instance.createPlayer("Vicky", 1, true);
        }
        if (GUI.Button(new Rect(10, 40, 100, 30), "创建其他角色"))
        {
            PlayerManager.Instance.createPlayer("Lily", 1, false);
        }
    }

}
