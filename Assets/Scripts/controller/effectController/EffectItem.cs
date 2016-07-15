using UnityEngine;

/// <summary>
/// 特效优先级
/// </summary>
[SerializeField]
public enum EffectPriority
{

    LOW = 0,        // 低效
    NORMAL = 1,     // 普通或更高设置显示
    EXCELLENT,      // 优秀或更高设置显示
    PERFECT,        // 完美模式显示

}

public class EffectItem : MonoBehaviour
{
    public EffectPriority priority = EffectPriority.LOW;

    private ParticleSystem particleSystem;

    private MeshReadererEffect meshReadererEffect;

    void Start()
    {
        this.particleSystem = GetComponent<ParticleSystem>();
        this.meshReadererEffect = GetComponent<MeshReadererEffect>();
        if (this.particleSystem == null && this.meshReadererEffect == null)
        {
            Debug.LogError("EffectItem 无法获得对应的特效！！！");
        }
    }

    public void play()
    {
        if (this.particleSystem != null)
        {
            this.particleSystem.Play(false);
        }
        if (this.meshReadererEffect != null)
        {
            this.meshReadererEffect.play();
        }
    }
}
