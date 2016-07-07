using System;
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
    [SerializeField]
    private string resourcePath;

    // 相对原始尺寸缩放比例
    [SerializeField] private float ratio = 1f;

    void Start()
    {
        this.image = GetComponent<Image>();
        if (!resourcePath.EndsWith("/"))
        {
            Object load = Resources.Load(resourcePath, typeof(Sprite));
            // 设置的是一张图片
            if (load != null)
            {
                Sprite newSprite = load as Sprite;

                this.image.sprite = newSprite;

                // 使用图片默认大小
                if (ratio == 1f)
                {
                    image.SetNativeSize();
                }
                else
                {
                    Rect rect = newSprite.rect;
                    this.image.rectTransform.sizeDelta = new Vector2(rect.size.x * ratio, rect.size.y * ratio);
                }

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

    public void changeImage(string name, float ratio)
    {
        StartCoroutine(_changeImage(name, ratio));
    }

    /// <summary>
    /// 切换图片
    /// </summary>
    /// <param name="name"></param>
    /// <param name="mapSize"></param>
    /// <param name="ratioSize"></param>
    /// <returns></returns>
    private IEnumerator _changeImage(string name, float ratio)
    {
        yield return 1;
        Object res = Resources.Load(resourcePath + name, typeof(Sprite));
        if (res == null)
        {
            yield break;
        }

        Sprite newSprite = res as Sprite;

        Rect rect = newSprite.rect;

        Sprite oldSprite = image.sprite;

        image.sprite = newSprite;

        if (oldSprite != null && !oldSprite.packed /*不是图集中的，那么肯定是Resource加载的*/)
        {
            Resources.UnloadAsset(oldSprite);
        }

        if (ratio == 1f)
        {
            image.SetNativeSize();
        }
        else
        {
            this.ratio = ratio;
            image.rectTransform.sizeDelta = new Vector2(rect.size.x * ratio, rect.size.y * ratio);
        }
    }
}
