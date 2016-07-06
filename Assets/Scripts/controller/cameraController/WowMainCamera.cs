using UnityEngine;
using System.Collections;

public class WowMainCamera : MonoBehaviour
{
	public bool HideAndShowCursor = true;

    // �Ƿ�������ͷ��ת��false����������Ҽ�������ͷ��true���ر�����Ҽ�������ͷ��
	public bool LockRotationWhenRightClick = false;

    // ʹ��ģ��Ч��
	// public bool UseBlurEffect = true;

    // ʹ����Ч
    // public bool UseFogEffect = true;

    // ��ͷĿ��
    public Transform target;

    // ���Բ���Ŀ�����������1��һ�Σ�
    public int tryFindTarget = 10;

    // Ŀ��߶�
    public float targetHeight = 1.0f;
    // Ĭ�Ͼ���
    public float distance = 5.0f;

    // ��ͷ��Ŀ�����С�������
    public float minDistance = 2f;
    public float maxDistance = 14;

    // ����Ҽ����м����µģ���ͷx��y��ת�ٶ�
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    // y�����ܵ��ڵ���С�����ֵ
    public int yMinLimit = -80;
    public int yMaxLimit = 80;

    // ����м�������µľ���ı��ٶ�
    public int zoomRate = 40;

    // WASD���¾�ͷ��ת�ٶ�
    public float rotationDampening = 3.0f;
    public float zoomDampening = 10.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    // ��ǰ����
    private float currentDistance;
    // Ԥ�ھ���
    private float desiredDistance;
    // ��������
    private float correctedDistance;
    // ��ͷ�Ƿ��ڵر���������帲����
    // private bool grounded = false;


    // Ѱ·�ر��
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
        // ����������˸��������ȷ��������������������ת
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

	    while (true)
	    {
            yield return new WaitForSeconds(1);

            // Don't do anything if target is not defined
            // ���δָ��Ŀ�꣬�Զ�����Player����
            if (!target)
            {
                tryFindTarget--;
                GameObject go = GameObject.Find("Player");
                if (go != null)
                {
                    Debug.LogError("�Զ���ѯ��Ŀ�꣺Player!!!");
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
                    Debug.LogError("�޷��ҵ���ͷĿ�꣡����");
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
            // ������Ҽ��Լ��м����ʱ���������
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

	    //TODO �����ܣ��ŵ�Start�д���
        //// Don't do anything if target is not defined
        //// ���δָ��Ŀ�꣬�Զ�����Player����
        //if (!target){
        //	GameObject go = GameObject.Find("Player");
        //	target = go.transform;
        //	transform.LookAt(target);
        //    return;
        //}

            // If either mouse buttons are down, let the mouse govern camera position
            // ������Ҽ����м������£�������ͷ��ת
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
			if(!LockRotationWhenRightClick){
                // ��þ�ͷx��y�ñ���
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			}
        }		
        // otherwise, ease behind the target if any of the directional keys are pressed
        // ������WASD�ı���ˮƽ��ֱ����ʱ�������������ת����Ŀ��Ƕȣ�
//        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
//        {
//            float targetRotationAngle = target.eulerAngles.y;
//            float currentRotationAngle = transform.eulerAngles.y;
//            x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
//        }

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // set camera rotation
        // ���þ�ͷ��ת��ͨ��Vector3����ŷ���Ƕ�Ӧ����Ԫ��Quaternion
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        // calculate the desired distance
        // ��������м�������µ�Զ���޸�
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        // ������������
        correctedDistance = desiredDistance;

        // calculate desired camera position
        // ��������ƶ�����λ��
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + new Vector3(0, -targetHeight, 0));

        // check for collision using the true target's desired registration point as set by user using height
        // ͨ�����ߣ��������ƶ�����λ���Ƿ��ڵر���������帲����

        // ��ʵĿ��λ��
        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

        // if there was a collision, correct the camera position and calculate the corrected distance
        bool isCorrected = false;
        RaycastHit collisionHit;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, walkMask))
        {
            position = collisionHit.point;
            // ������������,��֤��������ܴ��ڵر���������帲����
            correctedDistance = Vector3.Distance(trueTargetPosition, position);
            isCorrected = true;
        }

        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
        // ƽ����þ�ͷ�ƶ���Χ
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
            float cost = (Time.realtimeSinceStartup - begin) * 1000000; // ת����Ϊ����
            if (cost > 200) // ����200����ʹ����������ˣ���Ҫ�Ż�����
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
        // ����angle��Χ�޶�ֵ
        return Mathf.Clamp(angle, min, max);
    }
}