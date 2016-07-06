using UnityEngine;
using System.Collections;

public class WowMainCamera : MonoBehaviour
{
	public bool HideAndShowCursor = true;

    // 是否锁定镜头旋转（false：开启鼠标右键调整镜头，true：关闭鼠标右键调整镜头）
	public bool LockRotationWhenRightClick = false;

    // 使用模糊效果
	// public bool UseBlurEffect = true;

    // 使用雾效
    // public bool UseFogEffect = true;

    // 镜头目标
    public Transform target;

    // 尝试查找目标的最大次数（1秒一次）
    public int tryFindTarget = 10;

    // 目标高度
    public float targetHeight = 1.0f;
    // 默认距离
    public float distance = 5.0f;

    // 镜头与目标的最小最近距离
    public float minDistance = 2f;
    public float maxDistance = 14;

    // 鼠标右键或中键导致的，镜头x，y旋转速度
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    // y方向能调节的最小与最大值
    public int yMinLimit = -80;
    public int yMaxLimit = 80;

    // 鼠标中间滚动导致的距离改变速度
    public int zoomRate = 40;

    // WASD导致镜头旋转速度
    public float rotationDampening = 3.0f;
    public float zoomDampening = 10.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    // 当前距离
    private float currentDistance;
    // 预期距离
    private float desiredDistance;
    // 修正距离
    private float correctedDistance;
    // 镜头是否在地表或其他物体覆盖下
    // private bool grounded = false;


    // 寻路地表层
    public int walkMask;


    private IEnumerator Start ()
    {
        walkMask = LayerMask.GetMask("Walk");

        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance - 0.2f;

        // Make the rigid body not change rotation
        // 如果相机添加了刚体组件，确保相机刚体组件解锁了旋转
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

	    while (true)
	    {
            yield return new WaitForSeconds(1);

            // Don't do anything if target is not defined
            // 如果未指定目标，自动查找Player对象
            if (!target)
            {
                tryFindTarget--;
                GameObject go = GameObject.Find("Player");
                if (go != null)
                {
                    Debug.LogError("自动查询到目标：Player!!!");
                    target = go.transform;
                    transform.LookAt(target);
                    break;
                }

                PlayerMoveController[] controllers = GameObject.FindObjectsOfType<PlayerMoveController>();
                foreach (PlayerMoveController controller in controllers)
                {
                    //if (controller.isLocalPlayer)
                    //{
                    target = controller.transform;
                    transform.LookAt(target);
                    //}
                }

                if (tryFindTarget < 0)
                {
                    Debug.LogError("无法找到镜头目标！！！");
                }
            }
        }

        Debug.Log("WowMainCamera init success !");
    }

	void LateUpdate ()
    {

#if UNITY_EDITOR && DEBUG_MOVE
        float begin = Time.realtimeSinceStartup;
        // long begin = DateTime.Now.Ticks;
        try
        {
#endif



#if UNITY_EDITOR
            // 在鼠标右键以及中键点击时，隐藏鼠标
            if (HideAndShowCursor)
		{
		    if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
		        Cursor.visible = false; //Screen.lockCursor = true; 
		    else
		        Cursor.visible = true; //Screen.lockCursor = false; 
		}
#endif

	    if (!target)
	    {
	        return;
	    }

	    //TODO 耗性能，放到Start中处理
        //// Don't do anything if target is not defined
        //// 如果未指定目标，自动查找Player对象
        //if (!target){
        //	GameObject go = GameObject.Find("Player");
        //	target = go.transform;
        //	transform.LookAt(target);
        //    return;
        //}

            // If either mouse buttons are down, let the mouse govern camera position
            // 当鼠标右键或中键被按下，调整镜头旋转
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
			if(!LockRotationWhenRightClick){
                // 获得镜头x，y该变量
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			}
        }		
        // otherwise, ease behind the target if any of the directional keys are pressed
        // 当键盘WASD改变了水平或垂直向量时，调整摄像机旋转（以目标角度）
//        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
//        {
//            float targetRotationAngle = target.eulerAngles.y;
//            float currentRotationAngle = transform.eulerAngles.y;
//            x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
//        }

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // set camera rotation
        // 设置镜头旋转，通过Vector3返回欧拉角对应的四元素Quaternion
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        // calculate the desired distance
        // 计算鼠标中间滚动导致的远近修改
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        // 设置修正距离
        correctedDistance = desiredDistance;

        // calculate desired camera position
        // 计算相机移动到的位置
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + new Vector3(0, -targetHeight, 0));

        // check for collision using the true target's desired registration point as set by user using height
        // 通过射线，检测相机移动到的位置是否在地表或其他物体覆盖下

        // 真实目标位置
        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

        // if there was a collision, correct the camera position and calculate the corrected distance
        bool isCorrected = false;
        RaycastHit collisionHit;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, walkMask))
        {
            position = collisionHit.point;
            // 设置修正距离,保证摄像机不能处于地表或其他物体覆盖下
            correctedDistance = Vector3.Distance(trueTargetPosition, position);
            isCorrected = true;
        }

        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
        // 平滑获得镜头移动范围
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance;

        // recalculate position based on the new currentDistance
            position = target.position - (rotation*Vector3.forward*currentDistance + new Vector3(0, -targetHeight - 0.2f, 0));

        transform.rotation = rotation;
        transform.position = position;


#if UNITY_EDITOR && DEBUG_MOVE
        }
        finally
        {
            //long cost = DateTime.Now.Ticks - begin;
            float cost = (Time.realtimeSinceStartup - begin) * 1000000; // 转换成为纳秒
            if (cost > 200) // 超过200纳秒就代表有问题了，需要优化性能
            {
                Debug.Log("WoWMainCamera Update cost ::: " + cost);
            }
        }
#endif

    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        // 返回angle范围限定值
        return Mathf.Clamp(angle, min, max);
    }
}