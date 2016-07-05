

using System.Collections;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    private long id = 0;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "创建主角"))
        {
            PlayerManager.Instance.createPlayer(id++, "Vicky", 1, true);
        }
        if (GUI.Button(new Rect(10, 40, 100, 30), "创建其他角色"))
        {
            PlayerManager.Instance.createPlayer(id++, "Lily", 1, false);
        }
    }

}
