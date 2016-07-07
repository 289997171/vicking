using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Image))]
public class ImageFromRes : MonoBehaviour
{
    // 对应的Image组件
    private Image image;

    // 换图片其他图片的路径
    [SerializeField] private string resourcePath;

    void Start()
    {
        this.image = GetComponent<Image>();
        if (!resourcePath.EndsWith("/"))
        {
            Object load = Resources.Load(resourcePath, typeof (Sprite));
            // 设置的是一张图片
            if (load != null)
            {
                this.image.sprite = load as Sprite;

                // 使用图片默认大小
                image.SetNativeSize();

                int index = resourcePath.LastIndexOf('/');
                resourcePath = resourcePath.Remove(index + 1);
            }
            // 设置的是一个路径
            else
            {
                resourcePath += "/";
            }
        }
    }

    /// <summary>
    /// 切换图片
    /// </summary>
    /// <param name="name"></param>
    /// <param name="setNativeSize"></param>
    /// <returns></returns>
    private IEnumerator changeImage(string name, bool setNativeSize)
    {
        yield return 1;
        Object res = Resources.Load(resourcePath + name, typeof (Sprite));
        if (res == null)
        {
            yield break;
        }

        Sprite newSprite = res as Sprite;

        Sprite oldSprite = image.sprite;

        image.sprite = newSprite;

        if (oldSprite != null && !oldSprite.packed /*不是图集中的，那么肯定是Resource加载的*/)
        {
            Resources.UnloadAsset(oldSprite);
        }

        if (setNativeSize)
        {
            Debug.Log("setNativeSize!!");
            image.SetNativeSize();
        }
    }
}
