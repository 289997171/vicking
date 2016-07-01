using UnityEngine;

[RequireComponent(typeof(Clazz))]
public class ClazzTest : MonoBehaviour
{

    void Start()
    {
        Clazz clazz = this.GetComponent<Clazz>();
        clazz.sayHello();
    }
}
