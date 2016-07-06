using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestImage : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private Image image;

    void Start()
    {

        this.image = GetComponent<Image>();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 200, 100, 30), "切换图片"))
        {
            this.image.sprite = sprites[Random.Range(0, 3)];
        }
    }
}
