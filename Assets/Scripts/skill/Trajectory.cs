using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class YZCurve
{
    public AnimationCurve yCurve;
    public AnimationCurve zCurve;

    public YZCurve()
    {
        yCurve = new AnimationCurve(/*new[] { new Keyframe(0, 0), new Keyframe(0.5f, 0.2f), new Keyframe(1, 0) }*/);
        zCurve = new AnimationCurve(/*new[] { new Keyframe(0, 0), new Keyframe(0.5f, 0.2f), new Keyframe(1, 0) }*/);
    }
}


public class Trajectory : MonoBehaviour
{

    public List<YZCurve> yzCurves = new List<YZCurve>();

#if UNITY_EDITOR
    [SerializeField]
#endif
    private YZCurve yzCurve;

#if UNITY_EDITOR
    [SerializeField]
#endif
    private float lastTime;

    private float costTime;

    private float distance;

    private float speed = 10f;

    private bool inited = false;

    //public AnimationCurve yCurve = new AnimationCurve(new[] { new Keyframe(0, 0), new Keyframe(0.5f, 0.2f), new Keyframe(1, 0) });
    //public AnimationCurve zCurve = new AnimationCurve(new[] { new Keyframe(0, 0), new Keyframe(0.5f, 0.2f), new Keyframe(1, 0) });

    public void init(float distance, float lastTime, int index = -1)
    {
        this.distance = distance;

        this.lastTime = lastTime;

        if (index == -1 || index >= yzCurves.Count)
        {
            index = RandomUtil.next(yzCurves.Count);
            Debug.Log("INDEX = " + index);
        }

        this.yzCurve = yzCurves[index];

        AnimationCurve yCurve = yzCurve.yCurve;
        AnimationCurve zCurve = yzCurve.zCurve;

        foreach (Keyframe kf in yCurve.keys)
        {
            Debug.Log(kf.time + " " + kf.value);
        }

        foreach (Keyframe kf in zCurve.keys)
        {
            Debug.Log(kf.time + " " + kf.value);
        }

        this.inited = true;
    }

    public void evaluateYZ(ref float y, ref float z)
    {
        costTime += Time.deltaTime;

        float time = costTime / lastTime;
        y = yzCurve.yCurve.Evaluate(time);
        z = yzCurve.zCurve.Evaluate(time);
    }

    private float y, z;

    private float dy;
    private float dz;

    private int index = 0;

    void Update()
    {
        if (!inited) return;

        this.transform.LookAt(target);

        this.transform.position += this.transform.forward * speed * Time.deltaTime;

        evaluateYZ(ref y, ref z);

        y *= this.distance;
        z *= this.distance;

        this.traj.position = new Vector3(this.transform.position.x, this.transform.position.y + y, this.transform.position.z + z);

#if UNITY_EDITOR
        GameObject ghost = Instantiate(Resources.Load("Sphere")) as GameObject;
        ghost.transform.position = this.traj.position;
        ghost.transform.rotation = this.transform.rotation;
#endif

        if (costTime > lastTime)
        {
            Destroy(this);
        }
    }

    public Transform target;

    public Transform traj;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "测试攻击目标"))
        {

            float distance = Vector3.Distance(transform.position, target.position);
            float lastTime = distance / speed;

            this.init(distance, lastTime, -1);
        }
    }
}
