using UnityEngine;

/// <summary>
/// 控制模型特效显示时间
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(EffectItem))]
public class MeshReadererEffect : MonoBehaviour
{
	public Animation animation;

    public MeshRenderer meshRenderer;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    /// <summary>
    /// 模型特效持续时间
    /// </summary>
    public float lastTime = 0f;

    /// <summary>
    /// 是否在Awake/Start后，播放
    /// </summary>
    [HideInInspector]
    public bool playOnAwake = false;

#if UNITY_EDITOR
    [SerializeField]
#endif
    private float cost = 0f;

    void Start()
    {
		this.animation = GetComponent<Animation> ();
		if (this.animation != null) {
			this.animation.playAutomatically = false;
		}

        this.meshRenderer = GetComponentInChildren<MeshRenderer>();
        this.skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

#if UNITY_EDITOR
        if (this.meshRenderer == null && this.skinnedMeshRenderer == null)
        {
            Debug.LogError("错误，在无MeshRenderer的组件上挂载MeshReadererEffect：" + this.name);
        }
#endif
        Debug.LogError("playOnAwake :: " + playOnAwake);

        if (!playOnAwake)
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
        else
        {
            play();
        }
    }

    public void play()
    {
		if (this.animation != null) {
			this.animation.Play ();
		}

        if (this.skinnedMeshRenderer != null)
        {
            this.skinnedMeshRenderer.enabled = true;
        }

        if (this.meshRenderer != null)
        {
            this.meshRenderer.enabled = true;
        }
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
