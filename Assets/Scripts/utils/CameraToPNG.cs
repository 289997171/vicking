using UnityEngine;

/// <summary>
/// 截图当前Game视图
/// </summary>
public class CameraToPNG : MonoBehaviour
{
    public string pngname;

    public void CaToPNG(string _pngname)
    {
        if (string.IsNullOrEmpty(_pngname))
        {
            Debug.LogError("图片名为空");
            return;
        }
        string name = string.Format("{0}/MiniMap/{1}.png", Application.dataPath, _pngname);
        
        Application.CaptureScreenshot(name, 0);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "生成照片"))
        {
            CaToPNG(pngname);
        }
    }
}
