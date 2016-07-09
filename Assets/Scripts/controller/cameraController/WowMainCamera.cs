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

    // ��ͷ��ɫ
    public Person targetPerson;

    // ���Բ���Ŀ�����������1��һ�Σ�
    public int tryFindTarget = 10;

    // Ŀ��߶�
    public float targetHeight = 1.0f;
    // Ĭ�Ͼ���
    public float distance = 8.0f;

    // ��ͷ��Ŀ�����С�������
    public float minDistance = 2f;
    public float maxDistance = 16;

    // ����Ҽ����м����µģ���ͷx��y��ת�ٶ�
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    // y�����ܵ��ڵ���С�����ֵ
    public float yMinLimit = -80;
    public float yMaxLimit = 80;

    // ����м�������µľ���ı��ٶ�
    public float zoomRate = 40;

    // WASD���¾�ͷ��ת�ٶ�
    public float rotationDampening = 3.0f;
    public float zoomDampening = 10.0f;

    // ��Ϊ��ɫ�ƶ����¾�ͷ��ת
    public float personRunningRotationDampening = 0.5f;

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

    // �����ƶ���ʱ�����ﳯ��ı��Ƿ�Ӱ���������ת (�ƶ���WoWMainCamera)
    public bool mainCameraRotate = true;


    #region 2D-3D change
    // ʹ��2D�����
    public bool use2D = false;

    // ʹ��2D�����Y�̶��Ƕ�ֵ
    public float use2DY = 45f;

    public float use3DY = 20f;

    private float use3DX = 0f;

    public float use2Ddistance = 12f;

    public float use3Ddistance = 6f;

    // ����ı�ģʽ�У����2Dת3D��
    private bool changeing = false;
    #endregion


    private IEnumerator Start()
    {
        yield return 0;

        walkMask = LayerMask.GetMask("Walk");

        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        // y = angles.y;
        if (use2D)
            y = use3DY;
        else
            y = use3DY;


        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance - 0.2f;

        // Make the rigid body not change rotation
        // ����������˸��������ȷ��������������������ת
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        //	    while (true)
        //	    {
        //            yield return new WaitForSeconds(1);
        //
        //            // Don't do anything if target is not defined
        //            // ���δָ��Ŀ�꣬�Զ�����Player����
        //            if (!target)
        //            {
        //                tryFindTarget--;
        //                GameObject go = GameObject.Find("Player");
        //                if (go != null)
        //                {
        //                    Debug.LogError("�Զ���ѯ��Ŀ�꣺Player!!!");
        //                    target = go.transform;
        //                    transform.LookAt(target);
        //                    break;
        //                }
        //
        //                PlayerMoveController[] controllers = GameObject.FindObjectsOfType<PlayerMoveController>();
        //                foreach (PlayerMoveController controller in controllers)
        //                {
        //                    //if (controller.isLocalPlayer)
        //                    //{
        //                    target = controller.transform;
        //                    transform.LookAt(target);
        //                    //}
        //                }
        //
        //                if (tryFindTarget < 0)
        //                {
        //                    Debug.LogError("�޷��ҵ���ͷĿ�꣡����");
        //                }
        //            }
        //        }

        Debug.Log("WowMainCamera init success !");
    }

    private float h;
    private float v;

    void LateUpdate()
    {

#if UNITY_EDITOR && DEBUG_CAMERA
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
            if (changeing || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                if (!LockRotationWhenRightClick)
                {

                    if (use2D)
                    {
                        if (changeing)
                        {
                            if (Mathf.Abs(DistanceAngle(y, use2DY)) < 1f)
                            {
                                changeing = false;
                                y = use2DY;
                            }
                            else
                            {
                                y = Mathf.LerpAngle(y, use2DY, Time.deltaTime * 2f);
                            }
                        }
                        else
                        {
                            h = Input.GetAxis("Mouse X");
                            x += h * xSpeed * 0.02f;
                        }
                    }
                    else
                    {
                        // ��þ�ͷx��y�ñ���
                        h = Input.GetAxis("Mouse X");
                        v = Input.GetAxis("Mouse Y");

                        if (changeing)
                        {
                            if (h != 0f || v != 0f) changeing = false;
                            else
                            {
                                if (Mathf.Abs(DistanceAngle(y, use3DY)) < 1f && Mathf.Abs(DistanceAngle(x, use3DX)) < 1f)
                                {
                                    changeing = false;
                                    x = use3DX;
                                    y = use3DY;
                                }
                                else
                                {
                                    x = Mathf.LerpAngle(x, use3DX, Time.deltaTime * 2f);
                                    y = Mathf.LerpAngle(y, use3DY, Time.deltaTime * 2f);
                                }
                            }
                        }
                        else
                        {
                            x += h * xSpeed * 0.02f;
                            y -= v * ySpeed * 0.02f;
                        }
                    }

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
            // ��ɫ�ƶ�����������������ﳯ����ת
            else if (targetPerson != null && targetPerson.Moveable.isMoving())
            {
                float targetRotationAngle = target.eulerAngles.y;
                float currentRotationAngle = transform.eulerAngles.y;
                x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, personRunningRotationDampening * Time.deltaTime);
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            // set camera rotation
            // ���þ�ͷ��ת��ͨ��Vector3����ŷ���Ƕ�Ӧ����Ԫ��Quaternion
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // calculate the desired distance
            // ��������м�������µ�Զ���޸�
            if (changeing)
            {
                desiredDistance = Mathf.Lerp(desiredDistance, distance, Time.deltaTime * 4f);
            }
            else
            {
                desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
                desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
            }

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
            position = target.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -targetHeight - 0.2f, 0));

            transform.rotation = rotation;
            transform.position = position;


#if UNITY_EDITOR && DEBUG_CAMERA
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

    private static float DistanceAngle(float a1, float a2)
    {
        while (a1 < 0)
        {
            a1 += 360;
        }
        while (a1 > 360)
        {
            a1 -= 360;
        }
        while (a2 < 0)
        {
            a2 += 360;
        }
        while (a2 > 360)
        {
            a2 -= 360;
        }
        return a1 - a2;
    }

#if UNITY_EDITOR

    private string buttonContext = "2.5D";

    void OnGUI()
    {

        if (use2D)
        {
            if (GUI.Button(new Rect(Screen.width - 110, Screen.height / 2, 100, 30), "3D Camera"))
            {
                use2D = false;
                //Vector3 dir = transform.TransformDirection(new Vector3(0, target.eulerAngles.y, 0));
                //dir.Normalize();
                use3DX = target.eulerAngles.y;
                distance = use3Ddistance < maxDistance ? use3Ddistance : maxDistance;
                changeing = true;
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width - 110, Screen.height / 2, 100, 30), "2.5D Camera"))
            {
                use2D = true;
                distance = use2Ddistance < maxDistance ? use2Ddistance : maxDistance;
                changeing = true;
            }
        }

    }

#endif
}