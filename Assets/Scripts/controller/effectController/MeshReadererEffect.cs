using UnityEngine;

/// <summary>
/// 控制模型特效显示时间
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(EffectItem))]
public class MeshReadererEffect : MonoBehaviour
{
    // 动画片段
    public Animation animation;

    public MeshRenderer meshRenderer;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    /// <summary>
    /// 模型特效持续时间,使用动画
    /// </summary>
    public float lastTime = 0f;

    /// <summary>
    /// 是否在Awake/Start后，播放。（只有在follow = false的情况下）
    /// </summary>
    public bool playOnAwake = false;

    /// <summary>
    /// 已经持续的时间
    /// </summary>
#if UNITY_EDITOR
    [SerializeField]
#endif
    private float cost = 0f;

    void Start()
    {
        this.animation = GetComponentInChildren<Animation>();
        this.meshRenderer = GetComponentInChildren<MeshRenderer>();
        this.skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (this.animation != null)
        {
            this.animation.playAutomatically = false;

            // 默认持续时间使用动画片段时间
            this.lastTime = this.animation.clip.length;
        }

#if UNITY_EDITOR
        if (this.meshRenderer == null && this.skinnedMeshRenderer == null)
        {
            Debug.LogError("错误，在无MeshRenderer的组件上挂载MeshReadererEffect：" + this.name);
        }
#endif

        Debug.LogError("playOnAwake :: " + playOnAwake);

        if (playOnAwake)
        {
            playAwake();
        }
        else
        {
            if (this.skinnedMeshRenderer != null)
            {
                this.skinnedMeshRenderer.enabled = false;
            }

            if (this.meshRenderer != null)
            {
                this.meshRenderer.enabled = false;
            }
        }
    }

    private void playAwake()
    {
        if (this.skinnedMeshRenderer != null) this.skinnedMeshRenderer.enabled = true;

        if (this.meshRenderer != null) this.meshRenderer.enabled = true;

        if (this.animation != null) this.animation.Play();
    }

    public void play()
    {
        playAwake();

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
                if (this.skinnedMeshRenderer != null)
                {
                    this.skinnedMeshRenderer.enabled = false;
                }

                if (this.meshRenderer != null)
                {
                    this.meshRenderer.enabled = false;
                }
            }
        }
    }


}
