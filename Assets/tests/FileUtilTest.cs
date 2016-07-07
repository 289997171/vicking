
using System.Collections.Generic;
using UnityEngine;

public class FileUtilTest : MonoBehaviour
{

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "测试加载图片"))
        {
            List<Sprite> loadSprites = FileUtil.loadSprites("Texture/UI/MainUI/mimap");
            Debug.Log("loadSprites.Count = " + loadSprites.Count);
        }
    }
}
