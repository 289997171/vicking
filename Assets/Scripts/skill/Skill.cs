
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{
    #region properties

    //技能模板Id
    public int skillModelId;
    //技能等级
    public int skillLevel;


    /// <summary> 技能是如何激活 </summary>
    public enum Activation
    {
        User = 0,                   // 主动施法
        Auto = 1,					// 自动施放
        Passive = 2                 // 被动技能
    }

    /// <summary> 技能朝向 </summary>
    public enum TargetLocation
    {
        ActorDirection,         //!< 角色朝向
        MouseDirection,         //!< 鼠标朝向
        SelectedDirection,      //!< 选择的目标朝向
        AtClickPosition,        //!< 点击坐标点，需要玩家点击一个地方，将收集目标指定的半径
        AtSelected,             //!< 必须有目标
        MouseOver,              //!< 鼠标悬停，这就像典型的ARPG鼠标点击有效敌人的法术在它(在这种情况下悬停鼠标和按1,2,3还允许执行技能)
        CameraForward,          //!< 相机朝向
        CameraDirection,        //!< 相机朝向，忽略相机的倾斜(上/下)
        CrosshairOver,          //!< 射线接触的所有目标
    }


    /// <summary> 技能的效果是如何实现的 </summary>
    public enum DeliveryMethod
    {
        Instant,                //!< 即时:用于近战,自我治疗,包括非弹技能
        Projectile,             //!< 弹道:在目中目标前，应该创建对应的特效
        //Pathing				//!< 与弹道类似:在目中目标前，应该创建对应的特效
    }

    /// <summary> 技能如何收集可能的目标 </summary>
    public enum TargetingMethod
    {
        Self = 1,               //!< 包含施法者
        Selected = 2,           //!< 包含目标
        Auto = 4,               //!< 技能有效范围目标
    }

    /// <summary> 技能的有效目标 </summary>
    [System.Flags]
    public enum ValidTargets
    {
        Player = 1,             //!< 所有角色可以成为目标，无论用户的状态与派系
        FriendlyActor = 2,      //!< 友好目标，依赖派系
        NeutralActor = 4,       //!< 中立角色
        HostileActor = 8,       //!< 敌对角色
        RPGObject = 16,         //!< 任意目标对象
    }

    /// <summary> 弹道如何运动 </summary>
    public enum ProjectileMoveMethod
    {
        ActorFaceDirection,     //!< 角色朝向运动，可能击中，也可能不击中
        AngledFromEach,         //!< 只能用在创建多个弹道的情况，多弹道在朝向的角度随机发射
        DirectToTarget,         //!< 弹会找到一个目标,朝着它。热导(moveMethod_b_opt = true)可以设置为这个弹丸将搬到其他位置的目标是,在创建的时候弹
    }

    [HideInInspector]
    public float executionTimeout = 0f;     //!< (秒)需要多长时间执行技巧。在这次的演员不能开始一项新技能
    [HideInInspector]
    public float cooldownTimeout = 1f;      //!< (秒)CD 冷却时间
    [HideInInspector]
    public bool mayPerformWhileMove = true; //!< 角色是否会因为技能而移动
    [HideInInspector]
    public bool forceStop = true;           //!< 施法并且会移动时，才会呗设置为false，使角色停止其他skill
    [HideInInspector]
    public bool canBeQueued = true;         //!< 是否支持队列施放， 如果true，在使用其他技能的时候，也能施放当前技能，只不过入队列，等待之前的技能施放完毕后施放
    [HideInInspector]
    public bool autoQueue = false;          //!< 只有才canBeQueued = true的时候才有效。设置技能是否自动进入队列
    [HideInInspector]
    public bool actorMustFaceTarget = true; // 施放时，角色是否必须朝向目标



    [HideInInspector]
    public Activation activation = Activation.User;                     //!< 技能是如何激活
    [HideInInspector]
    public DeliveryMethod deliveryMethod = DeliveryMethod.Instant;      //!< 技能的效果是如何实现的
    [HideInInspector]
    public TargetingMethod targetingMethodMask = TargetingMethod.Auto;  //!< 技能如何收集可能的目标
    [HideInInspector]
    public ValidTargets validTargetsMask = 0;                           //!< 技能有效的目标验证
    [HideInInspector]
    public TargetLocation targetLocation = TargetLocation.ActorDirection; //!< 技能方向



    [HideInInspector]
    public int maxEffects = 1;          //!< 最大目标数量，对与弹道技能来说，这决定了可以选择的目标数量，也决定了弹道特效数量
    [HideInInspector]
    public float targetingDistance = 2; //!< (米) 技能施放距离，也决定了弹道失效钱可以在移动的距离
    [HideInInspector]
    public int targetingAngle = 45;     //!< (角度) 施放技能允许的角度范围
    [HideInInspector]
    public int targetingRadius = 5;	    //!< (米)用于检测圆AtClickPosition时使用


    [HideInInspector]
    public LayerMask obstacleCheckMask = 0; //!< 检查施法角色与目标之间是否有遮挡
    [HideInInspector]
    public float obstacbleCheckHeight = 1.5f;	//!< 检测遮挡的射线起点高度


    [HideInInspector]
    public float instantHitDelay = 0f; //!< 设置拥有 DeliveryMethod .延迟多久实例化伤害事件


    [HideInInspector]
    public int secondaryMaxTargets = 0;                     //!< 可用于溅射伤害，0为不使用溅射
    [HideInInspector]
    public int secondaryRadius = 5;                         //!< 溅射伤害半径，单位米
    [HideInInspector]
    public LayerMask secondaryObstacleCheckMask = 0;        //!< 溅射伤害判断特效与目标之间是否有遮挡
    [HideInInspector]
    public float secondaryObstacbleCheckHeight = 1.5f;		//!< 检测遮挡的射线起点高度


    [HideInInspector]
    public GameObject[] projectileFabs = new GameObject[5]; //!< 预制的弹丸。将从这些随机选择。
    [HideInInspector]
    public float projectileCreateDelay = 0f;                //!< 在第一个弹丸使用后多久创建新的弹丸
    [HideInInspector]
    public float projectileCreateDelayBetween = 0f;         //!< 创建炮弹之间的延迟多久
    [HideInInspector]
    public ProjectileMoveMethod projectileMoveMethod = ProjectileMoveMethod.ActorFaceDirection; //!< 弹丸是怎样运动的
    [HideInInspector]
    public bool moveMethod_b_opt = false;					//!< 弹丸是如何朝目标移动的，如，热跟踪？随机角度


    [HideInInspector]
    public bool useVectorOffset = true;                     //!< true:使用弹丸抵消其他弹丸的标记 if true, use projectileCreateOffset else projectileCreateAtTagged
    [HideInInspector]
    public Vector3 projectileCreateOffset = new Vector3(0f, 1f, 0.5f); //!< 弹丸默认施放点
    [HideInInspector]
    public string projectileCreateAtTagged = "";				//!<  弹丸默认创建挂载点名称，如施法者的左手，右手

    [HideInInspector]
    public int hitHeightPercentage = 50;                        //!< 击中目标的部位 0，脚步， 50，中心点 100,头部
    [HideInInspector]
    public int maxFlightDistance = 10;                      //!< (米) 弹丸失效之前的最大移动距离
    [HideInInspector]
    public float maxLiveTime = 0;                               //!< (秒) 技能失效前的最大生存时间
    [HideInInspector]
    public float projectileMoveSpeed = 10f;                 //!< 弹丸的移动速度
    [HideInInspector]
    public bool triggerSecondaryOnFizzle = false;               //!< 在技能失效时，是否出发溅射
    [HideInInspector]
    public bool triggerSecondaryOnFizzleOnlyIfObstacle = true;//!< ？？ Should the secondary only trigger if the fizzle was cause by the projectile colliding with an obstacle?
    [HideInInspector]
    public float collisionRayWidth = 0.0f;                  //!< Used by projectile to check when it hits something. 0 = ray cast is used, else a spherical cast is used
    [HideInInspector]
    public bool destroyProjectileOnHit = true;              //!< 通常设置为true；当弹丸击中目标后消失，当然特殊情况可以设置为false，在命中后有其他处理，如“光束武器”
    [HideInInspector]
    public bool lockProjectileUpDown = true;                // 锁定弹丸上下
    [HideInInspector]
    public bool useGlobalCooldown = false;                  // 使用公共CD
    [HideInInspector]
    public float targetSelectionRayWidth = 0.0f;            // 鼠标悬停选择目标的射线宽度
    [HideInInspector]
    public Vector3 crosshairOffset = Vector3.zero;          // 偏移量

    #endregion



    #region runtime

    public static float GlobalCooldown = 0f; // 技能公共CD时间，依赖“SkillsAsset.globalCooldownValu”以及“PlayerBaseController.Update()”

    /// <summary> 施法者，拥有者 </summary>
    /// <value> The owner. </value>
    public Person owner { get; set; }

    // 技能事件处理器
    protected EventHandler_Skills eventHandler = null;

    protected int projectileFabCount = 0;

    [System.NonSerialized]
    // 执行定时器
    public float executeTimer = 0.0f;
    [System.NonSerialized]
    // 冷却定时器
    public float cooldownTimer = 0.0f;

    // 目标
    public class CollectedTarget
    {
        public Targetable t;
        public float distance;
    }

    // 延迟攻击
    public class DelayedHit
    {
        public Targetable selectedObject;
        public Vector3 selectedPosition;
        public Vector3 mousePosition;
        public float timer = 0f;
    }

    // 延迟攻击列表
    protected List<DelayedHit> delayedHits = new List<DelayedHit>(0);

    #endregion


    #region init/start

    protected void Start()
    {
        executeTimer = 0.0f;
        cooldownTimer = 0.0f;

        // event handlers will only be available after Awake(), so grab it in Start()
        eventHandler = GetComponent<EventHandler_Skills>();

        // order the projectile prefabs so they follow on each other
        // 弹丸预制体跟随顺序
        projectileFabCount = 0;
        if (deliveryMethod == DeliveryMethod.Projectile)
        {
            for (int i = 0; i < projectileFabs.Length; i++)
            {
                if (projectileFabs[i] == null)
                {
                    for (int j = i + 1; j < projectileFabs.Length; j++)
                    {
                        if (projectileFabs[j] != null)
                        {
                            projectileFabs[i] = projectileFabs[j];
                            projectileFabs[j] = null;
                            break;
                        }
                    }
                }

                if (projectileFabs[i] != null) projectileFabCount++;
            }
        }
    }

    #endregion


    #region update

    protected void Update()
    {
        // run down the execute timer
        if (executeTimer > 0.0f)
        {
            executeTimer -= Time.deltaTime;
            if (executeTimer <= 0.0f) executeTimer = 0.0f;
        }

        // then the cool-down timer
        else if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0.0f) cooldownTimer = 0.0f;
        }

        // update delayed hits
        if (delayedHits.Count > 0)
        {
            if (owner == null)
            {
                delayedHits.Clear();
                return;
            }

            if (owner.isDie())
            {
                delayedHits.Clear();
                return;
            }

            for (int i = delayedHits.Count - 1; i >= 0; i--)
            {
                delayedHits[i].timer -= Time.deltaTime;
                if (delayedHits[i].timer <= 0.0f)
                {
                    //DoHitsOn(delayedHits[i].targets);	// do the hits
                    DoDelayedHit(delayedHits[i]);
                    delayedHits.RemoveAt(i);            // done with this delayed hit event
                }
            }
        }
    }

    protected void LateUpdate()
    {
        if (executeTimer <= 0.0f && cooldownTimer <= 0.0f && delayedHits.Count == 0 && gameObject.activeSelf)
        {
            // can go back to sleep, don't need you
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region workers

    // 收集目标点周围半径最大,过滤属于角
    protected List<Targetable> CollectTargets(Targetable selectedObject, int max, Vector3 aroundPoint, Vector3 direction, int angle, float radius)
    {
        if (validTargetsMask == 0) return new List<Targetable>(0); // return nothing when valid targets are set to nothing

        List<Targetable> targets = new List<Targetable>();
        if ((targetingMethodMask & TargetingMethod.Self) == TargetingMethod.Self)
        {
            if (owner.character != null)
            {
                if (IsValidTargetable(owner.character, owner.transform, owner.transform.position, obstacbleCheckHeight, obstacleCheckMask))
                {
                    targets.Add(owner.character);
                    if (targets.Count == max) return targets;
                }
            }
        }

        if ((targetingMethodMask & TargetingMethod.Selected) == TargetingMethod.Selected)
        {
            if (IsValidTargetable(selectedObject, owner.transform, owner.transform.position, obstacbleCheckHeight, obstacleCheckMask))
            {
                if (!targets.Contains(selectedObject)) targets.Add(selectedObject);
                if (targets.Count == max) return targets;
            }
        }

        if ((targetingMethodMask & TargetingMethod.Auto) == TargetingMethod.Auto)
        {
            // First find all valid targets within radius around point
            // Then select those that falls within a certain angle
            // and from those the ones closest to the point will
            // have priority in making the final list

            LayerMask mask = 0;
            if ((validTargetsMask & ValidTargets.Player) == ValidTargets.Player) mask |= 1 << GameGlobal.LayerMapping.Player;
            if ((validTargetsMask & ValidTargets.RPGObject) == ValidTargets.RPGObject) mask |= 1 << GameGlobal.LayerMapping.plyObject;
            if ((validTargetsMask & ValidTargets.FriendlyActor) == ValidTargets.FriendlyActor ||
                (validTargetsMask & ValidTargets.NeutralActor) == ValidTargets.NeutralActor ||
                (validTargetsMask & ValidTargets.HostileActor) == ValidTargets.HostileActor
            //(validTargetsMask & ValidTargets.ReversedFriendlyActor) == ValidTargets.ReversedFriendlyActor ||
            //(validTargetsMask & ValidTargets.ReversedNeutralActor) == ValidTargets.ReversedNeutralActor ||
            //(validTargetsMask & ValidTargets.ReversedHostileActor) == ValidTargets.ReversedHostileActor
            )
            {
                mask |= 1 << GameGlobal.LayerMapping.Player;
                mask |= 1 << GameGlobal.LayerMapping.NPC;
            }

            Collider[] colliderHits = Physics.OverlapSphere(aroundPoint, radius, mask);
            if (colliderHits.Length > 0)
            {
                // create a list of all the hits and how far they are from the target point
                List<CollectedTarget> collectedTargets = new List<CollectedTarget>();
                for (int i = 0; i < colliderHits.Length; i++)
                {
                    Targetable target = colliderHits[i].gameObject.GetComponent<Targetable>();
                    if (target != null)
                    {
                        collectedTargets.Add(new CollectedTarget()
                        {
                            t = target,
                            distance = Vector3.Distance(colliderHits[i].transform.position, aroundPoint)
                        });
                    }
                }

                // sort the objects by distance from the point
                collectedTargets.Sort(delegate (CollectedTarget a, CollectedTarget b) { return a.distance.CompareTo(b.distance); });

                if (angle < 5 || angle > 355)
                {   // assume 360 degrees
                    for (int i = 0; i < collectedTargets.Count; i++)
                    {
                        if (IsValidTargetable(collectedTargets[i].t, owner.transform, owner.transform.position, obstacbleCheckHeight, obstacleCheckMask))
                        {
                            if (!targets.Contains(collectedTargets[i].t)) targets.Add(collectedTargets[i].t);
                            if (targets.Count == max) return targets;
                        }
                    }
                }

                else
                {
                    float maxAngle = angle / 2f;
                    float minAngle = -maxAngle;

                    for (int i = 0; i < collectedTargets.Count; i++)
                    {
                        if (IsValidTargetable(collectedTargets[i].t, owner.transform, owner.transform.position, obstacbleCheckHeight, obstacleCheckMask))
                        {
                            // first check if within the angle before adding
                            float res = plyUtil.AngleSigned(direction, (collectedTargets[i].t.transform.position - aroundPoint), Vector3.up);
                            if (res >= minAngle && res <= maxAngle)
                            {
                                if (!targets.Contains(collectedTargets[i].t)) targets.Add(collectedTargets[i].t);
                                if (targets.Count == max) return targets;
                            }
                        }
                    }
                }
            }
        }

        return targets;
    }

    #endregion
}
