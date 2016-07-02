using UnityEngine;

[DisallowMultipleComponent]
public abstract class Clazz : MonoBehaviour
{
    public abstract void sayHello();

    protected void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    protected void OnDisable()
    {
        Debug.Log("OnDisable");
    }
}
