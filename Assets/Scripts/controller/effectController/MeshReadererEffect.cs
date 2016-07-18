using UnityEngine;

/// <summary>
/// 控制模型特效显示时间
/// </summary>
[DisallowMultipleComponent]
public class MeshReadererEffect : MonoBehaviour
{
	public Animation animation;

	public SkinnedMeshRenderer[] meshRenderers;

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
		this.animation = GetComponent<Animation> ();
		if (this.animation != null) {
			this.animation.playAutomatically = false;
		}

		this.meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();// GetComponent<MeshRenderer>();
		foreach (SkinnedMeshRenderer meshRenderer in meshRenderers) {
			meshRenderer.enabled = false;
		}
    }

    public void play()
    {
		if (this.animation != null) {
			this.animation.Play ();
		}
		foreach (SkinnedMeshRenderer meshRenderer in meshRenderers) {
			meshRenderer.enabled = true;
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
				foreach (SkinnedMeshRenderer meshRenderer in meshRenderers) {
					meshRenderer.enabled = false;
				}
            }
        }
    }

    
}
