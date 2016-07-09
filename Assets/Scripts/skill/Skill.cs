
using UnityEngine;


public class Skill : MonoBehaviour
{
    #region properties

    //技能模板Id
    private int skillModelId;
    //技能等级
    private int skillLevel;


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


    #endregion
}
