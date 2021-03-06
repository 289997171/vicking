﻿using System;
using UnityEngine;

/// <summary>
/// 控制角色的移动，包含了WASD移动和寻路移动
/// </summary>
[RequireComponent(typeof(WASDController))]
[RequireComponent(typeof(AreaController))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMoveController : Moveable, IPersonController
{
    public void updated()
    {
        stopMove();

        Start();
    }

    private Person person;

    private CharacterController characterController;

    private WASDController wasdController;

    private MoveAnimationController moveAnimationController;

    private AreaController areaController;

    // 重力加速度
    private float gravity = 10;

    // 是否在地面上
    // private bool onGround = false;
    private CollisionFlags flags;

    // 移动朝向
    private Vector3 moveDirection = Vector3.zero;

    // 场景资源动态加载
    private Vector3 oldPos = Vector3.zero;

    private bool canMove = true;

    // private Camera mainCamera;

    // 人物移动的时候，人物朝向改变是否影响摄像机旋转 (移动到WoWMainCamera)
    // [SerializeField] private bool mainCameraRotate = true;

    void Start()
    {
        // this.mainCamera = Camera.main;

        this.person = GetComponent<Person>();

        this.characterController = GetComponent<CharacterController>();

        this.wasdController = GetComponent<WASDController>();

        this.moveAnimationController = GetComponent<MoveAnimationController>();

        this.areaController = GetComponent<AreaController>();

        this.navMeshPath = new NavMeshPath();
    }


    private float h = 0;
    private float v = 0;
    private bool wasd = false;



    // 是否在旋转中
    private bool turning = false;

    // 强制朝向改变
    public bool turningForce = false;

    public bool turningForceKeep = false;

    public float maxAngle;

    // 强制朝向的目标
    private Targetable target;

    // 保持朝向时间
    private float keepLookAtTime;

    //    public float turningForceLast = 0.006f;
    //
    //    public float turningForceCost = 0f;



    // 目前移动只考虑水平朝向
    private float rotY;

    // 转向到
    private Quaternion newQuaternion = Quaternion.identity;



    void Update()
    {
#if UNITY_EDITOR && DEBUG_MOVE
        float begin = Time.realtimeSinceStartup;
        try
        {
#endif
            bool runing = false;

            float angle = 0f;

            // 未处于地面
            if (!onGround && !fly)
            {
                moveDirection.x = 0f;
                moveDirection.z = 0f;
                moveDirection.y -= gravity * Time.deltaTime;
                flags = characterController.Move(moveDirection);
                onGround = (flags & CollisionFlags.Below) != 0;
            }


            // 技能控制角色在一定时间内不能移动
            if (!canMove)
            {
                moveAnimationController.OnIdle();
                return;
            }

            // h = Input.GetAxis("Horizontal");
            // v = Input.GetAxis("Vertical");

            if (isMoveBy)
            {
                wasd = false;
            }
            else
            {
                this.wasdController.getInputHV(ref h, ref v);

                wasd = h != 0 || v != 0;

                if (wasd && navMoveing)
                {
                    stopMove();
                }
                else if (!wasd && !navMoveing && !turning)
                {
                    moveAnimationController.OnIdle();
                    return;
                }

                runing = true;
                // moveAnimationController.OnRun(h, v);
            }

            if (wasd)
            {
                moveDirection.x = h;
                moveDirection.y = 0;
                moveDirection.z = v;
                moveDirection.Normalize();

                // 1.以角色自身坐标朝向
                // moveDirection = transform.TransformDirection(moveDirection);

                // 2.以镜头坐标朝向 TODO 有问题
                //            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
                //            Debug.Log("moveDirection 1 = " + moveDirection.x + " " + moveDirection.y + " " + moveDirection.z);
                //            moveDirection.y = 0;
                //            moveDirection.Normalize();
                //            Debug.Log("moveDirection 2 = " + moveDirection.x + " " + moveDirection.y + " " + moveDirection.z);
                //            moveDirection *= this.person.finalAbility.speed;

                // 3.以镜头坐标朝向,只考虑y坐标朝向
                rotY = Camera.main.transform.rotation.eulerAngles.y;
                moveDirection = Quaternion.Euler(0, rotY, 0) * moveDirection;

                // moveDirection *= person.finalAbility.speed;
            }
            else if (navMoveing)
            {
                float minDistance = 0.3f;
                if (isMoveBy)
                {
                    minDistance = 0.6f;
                }
                //                if (isMoveBy && moveBySpeed > 20f)
                //                {
                //                    minDistance = 1f;
                //                }

                if (Vector3.Distance(transform.position, curAutoPos) < minDistance)
                {
                    if (!nextPoint() || !canNav/*冲锋等位移技能可能不会转点移动*/)
                    {
                        stopMove();
                    }
                    return;
                }

                // 目标点朝向
                moveDirection = curAutoPos - transform.position;
                moveDirection.y = 0f;
                moveDirection.Normalize();

                //                if (isMoveBy)
                //                {
                //                    moveDirection *= moveBySpeed;
                //                    //moveDirection = Vector3.Cross(moveDirection, moveBySpeed);
                //                    //moveDirection = new Vector3(moveDirection.x * -moveBySpeed.x, moveDirection.y * -moveBySpeed.y, moveDirection.z * -moveBySpeed.z);
                //                }
                //                else
                //                {
                //                    moveDirection *= this.person.finalAbility.speed;
                //                }

            }
            else if (isMoveBy) // 强制移动
            {
                isMoveBy = false;
                canNav = false;
            }
            else if (turning && !turningForce && !turningForceKeep) // 原地转向
            {
                angle = Quaternion.Angle(transform.rotation, newQuaternion);
                // TODO 判断角度范围
                if (angle < 5f)
                {
                    turning = false;
                    turningForce = false;
                }

                //rotY = Camera.main.transform.rotation.eulerAngles.y;
                //moveDirection = Quaternion.Euler(0, rotY, 0) * moveDirection;

                transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion,
                    this.person.rotSpeed * Time.deltaTime /*0.2f*/);

                moveAnimationController.OnTurn(newQuaternion);

                return;
            }

            

            if (turningForce)
            {
                //                turningForceCost -= Time.deltaTime;
                //
                //                if (turningForceCost < 0f)
                //                {
                //                    turning = false;
                //                    turningForce = false;
                //                }
                Vector3 dir = target.transform.position - this.transform.position;
                dir.y = 0f;
                dir.Normalize();

                newQuaternion = Quaternion.LookRotation(dir);
                angle = Quaternion.Angle(transform.rotation, newQuaternion);

                // TODO 判断角度范围
                if (angle < maxAngle)
                {
                    turning = false;
                    turningForce = false;

                    if (keepLookAtTime > 0f) turningForceKeep = true;
                }

                //rotY = Camera.main.transform.rotation.eulerAngles.y;
                //moveDirection = Quaternion.Euler(0, rotY, 0) * moveDirection;

                transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion,
                    this.person.rotSpeed * Time.deltaTime /*0.2f*/);

                angle = Vector3.Angle(moveDirection, dir);

            }
            else if (turningForceKeep)
            {

                keepLookAtTime -= Time.deltaTime;
                if (keepLookAtTime < 0f)
                {
                    turning = false;
                    turningForceKeep = false;
                }

                Vector3 dir = target.transform.position - this.transform.position;
                dir.y = 0f;
                dir.Normalize();

                newQuaternion = Quaternion.LookRotation(dir);

                // angle = Quaternion.Angle(transform.rotation, newQuaternion);

                angle = Vector3.Angle(moveDirection, dir);

                transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion,
                        this.person.rotSpeed * Time.deltaTime /*0.2f*/);
            }

            if (runing)
            {
                // Debug.LogError("angle ::: " + angle);
                moveAnimationController.OnRun(h, v, angle);
            }

            // Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.
            // 按下右键后，角色朝向始终与镜头朝向相同
            //        if (Input.GetMouseButton(1))
            //        {
            //            if (IsInWater)
            //            {
            //                transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0);
            //                
            //            }
            //            else
            //            {
            //                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            //            }
            //        }
            //        else
            //        {
            //            transform.Rotate(0, h * rotateSpeed * Time.deltaTime, 0);
            //            transform.Rotate(0, h * rotateSpeed * Time.deltaTime, 0);
            //        }

            // if (moveDirection == Vector3.zero) return;


            if (isMoveBy)
            {
                moveDirection *= moveBySpeed;
                //moveDirection = Vector3.Cross(moveDirection, moveBySpeed);
                //moveDirection = new Vector3(moveDirection.x * -moveBySpeed.x, moveDirection.y * -moveBySpeed.y, moveDirection.z * -moveBySpeed.z);
            }
            else
            {
                moveDirection *= this.person.finalAbility.speed;
            }


            // 处理坐标改变

            // 1.如果不使用CharacterController
            // transform.position += (moveDirection * Time.deltaTime);

            // 2.使用CharacterController
            flags = characterController.Move(moveDirection * Time.deltaTime);
            onGround = (flags & CollisionFlags.Below) != 0;


            // 处理朝向改变
#if UNITY_EDITOR
            if (moveDirection.y != 0)
            {
                Debug.LogError("moveDirection.y = " + moveDirection.y);
                moveDirection.y = 0f;
            }
#endif

            if (!turningForce && !turningForceKeep && moveDirection != Vector3.zero)
            {
                // 技能导致的移动，如果！canNav，不能改变朝向，如，LR的向后跳
                if (!isMoveBy || canNav)
                {
                    newQuaternion = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion,
                        this.person.rotSpeed * Time.deltaTime /*0.2f*/);
                }

                // 因为WOWMainCamra的原因，这里无效，所以需要添加到WoWMainCamra中
                // // 摄像机朝移动朝向旋转
                // if (mainCameraRotate)
                // {
                //     mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, newQuaternion, 0.001f);
                // }
            }

            // 出发区域改变判断
            {
                float x_changed = oldPos.x - transform.position.x;
                if (x_changed > 2f || x_changed < -2f)
                {
                    areaController.OnMove(oldPos, transform.position);
                    oldPos = transform.position;
                }

                float z_changed = oldPos.z - transform.position.z;
                if (z_changed > 2f || x_changed < -2f)
                {
                    areaController.OnMove(oldPos, transform.position);
                    oldPos = transform.position;
                }
            }

#if UNITY_EDITOR && DEBUG_MOVE
        }
        finally
        {
            float cost = (Time.realtimeSinceStartup - begin) * 1000000f; // 转换成为纳秒
            if (cost > 200.0f)
            {
                // Debug.Log("PlayerMoveController Update cost ::: " + cost);
            }
        }
#endif


    }



    #region 点击移动/寻路
    /// <summary>
    /// 是否在导航寻路中
    /// </summary>
    private bool navMoveing = false;
    /// <summary>
    /// 自动寻路信息
    /// </summary>
    private NavMeshPath navMeshPath;
    /// <summary>
    /// The end point.
    /// 最终目的地
    /// </summary>
    private Vector3 endPoint = Vector3.zero;
    /// <summary>
    /// The current auto point.
    /// 当前自动寻路的下个目标点
    /// </summary>
    private Vector3 curAutoPos = Vector3.zero;
    /// <summary>
    /// 下一个点
    /// </summary>
    private int nextPointIndex = 0;


    #region 技能或其他原因导致的移动
    //相对移动，如技能等控制的坐标改变
    private bool isMoveBy = false;

    //是否允许多点移动
    private bool canNav = false;

    //相对移动的速度
    private float moveBySpeed = 0f;
    #endregion

    /// <summary>
    /// 自动寻路
    /// </summary>
    /// <param name="endPos"></param>
    public override bool move(Vector3 endPos)
    {
        if (navMoveing) stopMove();

        // endPos.y = transform.position.y;
        if (Vector3.Distance(transform.position, endPos) < 1f)
        {
            Debug.Log("移动距离不超过0.5f");
            return false;
        }

        bool canNavMove = NavMesh.CalculatePath(transform.position, endPos, 255, navMeshPath);
        if (canNavMove)
        {
            navMoveing = true;

            nextPointIndex = 0;
            endPoint = endPos;

            nextPoint();
        }
        else
        {
            Debug.LogError("无法向指定点寻路！");
        }
        return canNavMove;
    }

    /// <summary>
    /// 停止自动导航寻路
    /// </summary>
    public override bool stopMove()
    {
        if (navMoveing)
        {
            navMoveing = false;

            if (isMoveBy) isMoveBy = false;
            if (canNav) canNav = false;

            nextPointIndex = 0;
            endPoint = Vector3.zero;
            curAutoPos = Vector3.zero;
            moveDirection = Vector3.zero;

            navMeshPath.ClearCorners();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 是否移动中
    /// </summary>
    /// <returns></returns>
    public override bool isMoving()
    {
        return navMoveing;
    }

    public override bool turn(Vector3 direction)
    {
        newQuaternion = Quaternion.LookRotation(direction);
        float angle = Quaternion.Angle(transform.rotation, newQuaternion);
        Debug.LogError("angle XXXXXX : " + angle);

        // TODO 判断角度范围
        if (angle > 5f)
        {
            turning = true;
        }

        return turning;
    }

    public bool turnForce(SingleSkill singleSkill/*Vector3 direction, float angle, float lookAtTime*/)
    {
        turning = true;
        turningForce = true;

        maxAngle = singleSkill.maxAngle;
        target = singleSkill.target;
        keepLookAtTime = singleSkill.keepLookAtTime;

        return turning;


//        // TODO 以下是老版本
//
//        newQuaternion = Quaternion.LookRotation(direction);
//        //        float angle = Quaternion.Angle(transform.rotation, newQuaternion);
//        //        Debug.LogError("angle XXXXXX : " + angle);
//        //
//        //        // TODO 判断角度范围
//        //        if (angle > 5f)
//        //        {
//        turning = true;
//        turningForce = true;
//        turningForceCost = lookAtTime;
//
//        //        if (angle < 30)
//        //        {
//        //            turningForceCost = 0.2f;
//        //        }
//        //        else if (angle > 90)
//        //        {
//        //            turningForceCost = 0.6f;
//        //        }
//        //        else
//        //        {
//        //            turningForceCost = 0.4f;
//        //        }
//
//        // turningForceCost = turningForceLast * angle;
//        // turningForceCost = turningForceCost < 0.2f ? 0.2f : (turningForceCost > 0.6f ? 0.6f : turningForceCost);
//        //        }
//
//        return turning;
    }

    public override bool stopTrun()
    {
        if (turning)
        {
            turning = false;
            return true;
        }
        return false;
    }

    public override bool isTurning()
    {
        return turning;
    }

    /// <summary>
    /// 是否还有未走完的点
    /// </summary>
    /// <returns></returns>
    private bool nextPoint()
    {
        nextPointIndex++;

        bool flag = true;
        if (navMeshPath.corners.Length > nextPointIndex)
        {
            curAutoPos = navMeshPath.corners[nextPointIndex];
        }
        else
        {
            curAutoPos = endPoint;
            flag = false;
        }

        return flag;
    }


    public override void setCanMove(bool canMove)
    {
        // TODO 
        this.canMove = canMove;
    }


    public override bool moveBy(Vector3 value, float speed, bool canNav)
    {
        this.isMoveBy = true;
        this.canNav = canNav;
        this.moveBySpeed = speed;

        return move(this.transform.position + value);
    }

    #endregion
}