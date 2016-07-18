using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillTest : MonoBehaviour
{

    public List<string> inputkeys = new List<string>();
    public List<int> skillIds = new List<int>();
    public LocalPlayer localPlayer;


    void Start()
    {
        // 表示，按下数字键1，施放技能1
        inputkeys.Add("Alpha1");
        skillIds.Add(1);
    }


    void OnGUI()
    {

        if (Input.anyKeyDown)
        {
            Event current = Event.current;
            if (current != null && current.isKey)
            {
                // Debug.Log("current.keyCode.ToString() = " + current.keyCode.ToString());
                int indexOf = inputkeys.IndexOf(current.keyCode.ToString());
                if (indexOf > -1)
                {
                    if (localPlayer == null)
                    {
                        localPlayer = GameObject.FindObjectOfType<LocalPlayer>();
                    }

                    if (localPlayer == null)
                    {
                        Debug.LogError("无法找到测试对象");
                        return;
                    }

                    SkillController skillController = localPlayer.GetComponent<SkillController>();
                    skillController.castSkill(null, skillIds[indexOf], 1);
                }
            }
        }
    }

}
