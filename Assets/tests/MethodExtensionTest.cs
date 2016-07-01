using UnityEngine;

public class MethodExtensionTest : MonoBehaviour
{
    public Transform GetTransformTest;
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "GetTransformTest"))
        {
            Transform find;

            find = GetTransformTest.Find("XXXX");
            Debug.Log("find = " + find);

            find = GetTransformTest.FindChild("XXXX");
            Debug.Log("find = " + find);

            find = GetTransformTest.FindInChildrens("XXXX");
            Debug.Log("find = " + find);
        }
    }
}
