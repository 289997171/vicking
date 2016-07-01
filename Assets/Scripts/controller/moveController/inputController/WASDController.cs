using UnityEngine;

[DisallowMultipleComponent]
public abstract class WASDController : MonoBehaviour
{
    public abstract void getInputHV(ref float h, ref float v);
}
