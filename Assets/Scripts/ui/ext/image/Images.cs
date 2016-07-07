using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Images : MonoBehaviour
{

    [SerializeField] private Sprite[] sprites;

    private Image image;

    void Start()
    {
        this.image = GetComponent<Image>();
    }

    public void changeImage(int index)
    {
        this.image.sprite = sprites[index];
    }

    public void changeImage(string name)
    {
        foreach (Sprite sp in sprites)
        {
            if (sp.name.Equals(name))
            {
                this.image.sprite = sp;
            }
        }
    }
}

public static class ImageExt
{
    public static void setName(this Image image, string name)
    {
        Images images = image.gameObject.GetComponent<Images>();
        if (images != null)
        {
            Debug.LogError("未找到Images!");
            images.changeImage(name);
        }
    }
}
