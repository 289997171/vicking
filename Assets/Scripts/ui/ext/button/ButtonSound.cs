
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{

    private Button button;

    [SerializeField] private AudioClip onClickSound;


    void Start()
    {
        this.button = GetComponent<Button>();

        if (onClickSound != null)
        {
            int i = 0;
            this.button.onClick.AddListener(playSound);
        }
    }

    public void playSound()
    {
        // TODO 播放音效
    }


}
