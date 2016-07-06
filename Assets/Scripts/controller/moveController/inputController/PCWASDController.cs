
using UnityEngine;

public class PCWASDController : WASDController
{
    private float h;
    private float v;
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
//#if UNITY_EDITOR
//        if (h != 0 || v != 0)
//        {
//            Debug.Log("h = " + h + "  v = " + v);
//        }
//#endif
    }

    public override void getInputHV(ref float h, ref float v)
    {
        h = this.h;
        v = this.v;
    }
}
