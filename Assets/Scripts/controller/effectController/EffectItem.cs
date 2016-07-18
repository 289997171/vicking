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

    #region 特效是跟随的，还是动态创建的
    // 是否跟随模型
    [HideInInspector]
    public bool follow = true;

    // 特效持续时间
    [HideInInspector]
    public float lastTime = 0f;

    // 是否是根特效
    [HideInInspector]
    public bool isRootEffect = false;
#endregion

    void Awake()
    {
        // Debug.LogError("EffectItem Awake()");

        this.particleSystem = GetComponent<ParticleSystem>();
        this.meshReadererEffect = GetComponent<MeshReadererEffect>();

        if (this.particleSystem == null && this.meshReadererEffect == null)
        {
            Debug.LogError("EffectItem 无法获得对应的特效！！！");
        }
        
        // 不跟随，动态创建
        if (!follow && !this.name.EndsWith("(Clone)"))
        {
            initNotFollow();

            this.isRootEffect = this.transform.parent.GetComponent<EffectGroup>() != null;
        }
    }

    public void initNotFollow()
    {
        this.follow = false;

        if (this.particleSystem != null)
        {
            float t = this.particleSystem.startDelay + this.particleSystem.startLifetime;
            if (t > lastTime)
            {
                lastTime = t;
            }
        }

        if (this.meshReadererEffect != null)
        {
            float t = this.meshReadererEffect.lastTime;
            if (t > lastTime)
            {
                lastTime = t;
            }
        }

        if (this.particleSystem != null)
        {
            this.particleSystem.playOnAwake = true;
        }

        if (this.meshReadererEffect != null)
        {
            this.meshReadererEffect.playOnAwake = true;
        }
    }


    //    void Start()
    //    {
    //        if (!follow)
    //        {
    //            this.isRootEffect = this.transform.parent.GetComponent<EffectGroup>() != null;
    //        }
    //    }

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
