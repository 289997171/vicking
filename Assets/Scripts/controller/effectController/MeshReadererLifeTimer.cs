using UnityEngine;

/// <summary>
/// 控制模型特效显示时间
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(MeshRenderer))]
public class MeshReadererLifeTimer : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    /// <summary>
    /// 模型特效持续时间
    /// </summary>
    public float lastTime = 0f;

#if UNITY_EDITOR
    [SerializeField]
#endif
    private float cost = 0f;

    void Start()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();
        this.meshRenderer.enabled = false;
    }

    public void play()
    {
        this.meshRenderer.enabled = true;
        this.cost = lastTime;
    }

    /// <summary>
    /// 处理模型特效的消失
    /// </summary>
    void LateUpdate()
    {
        if (cost > 0f)
        {
            cost -= Time.deltaTime;
            if (cost < 0f)
            {
                this.meshRenderer.enabled = false;
            }
        }
    }

    
}
