
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class JoysticksController : WASDController
{


#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    private float h;
    private float v;
    // 编辑环境下允许使用WASD
    public override void getInputHV(ref float h, ref float v)
    {

        float _h = CrossPlatformInputManager.GetAxis(Joystick.horizontalAxisName);
        float _v = CrossPlatformInputManager.GetAxis(Joystick.verticalAxisName);
        // CrossPlatformInputManager.GetButton("Jump");
        if (_h != 0 || _v != 0)
        {
            h = _h;
            v = _v;
        }
        else
        {
            h = this.h;
            v = this.v;
        }
    }

    
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }
#else
    public override void getInputHV(ref float h, ref float v)
    {

        h = CrossPlatformInputManager.GetAxis(Joystick.horizontalAxisName);
        v = CrossPlatformInputManager.GetAxis(Joystick.verticalAxisName);
        // CrossPlatformInputManager.GetButton("Jump");
    }
#endif

}
