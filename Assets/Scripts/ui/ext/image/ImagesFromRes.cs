using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ImagesFromRes : MonoBehaviour
{

#if UNITY_EDITOR
    // 目录中的所有图片
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();
#else
    private List<Sprite> sprites = new List<Sprite>();
    private bool inited = false;
#endif

    // 图片目录
    [SerializeField]
    private string directory;


    protected IEnumerator Start()
    {
        if (string.IsNullOrEmpty(directory)) yield break;
        
        yield return 1;
        List<Sprite> list = FileUtil.loadSprites(directory);
        sprites.Clear();
        sprites.AddRange(list);
    }

    public void setSpritesDirectory(string directory)
    {
        StartCoroutine(loadSprites());
    }

    private IEnumerator loadSprites()
    {
        yield return 1;
        List<Sprite> list = FileUtil.loadSprites(directory);
        sprites.Clear();
        sprites.AddRange(list);
    }

}
