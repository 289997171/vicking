
using System.Collections.Generic;
using UnityEngine;

public class FileUtil
{
    /// <summary>
    /// 从Resource的指定目录加载所有的图片资源
    /// </summary>
    /// <param name="resourcePath"></param>
    /// <returns></returns>
    public static List<Sprite> loadSprites(string resourcePath)
    {
        Object[] loaded = Resources.LoadAll(resourcePath, typeof (Sprite));
        if (loaded.Length < 1)
        {
            return null;
        }
        List<Sprite> list = new List<Sprite>();
        foreach (Object o in loaded)
        {
            list.Add(o as Sprite);
        }
        return list;
    }
}
