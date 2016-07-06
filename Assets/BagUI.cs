using UnityEngine;
using System.Collections;

public class BagUI : MonoBehaviour
{

    private UIWeaponEquitem baseWeaponItem;

    void Start()
    {
        Object weaponModel = Resources.Load("ui/equitem/WeaponEquitem");
        GameObject o = Instantiate(weaponModel) as GameObject;
        baseWeaponItem = o.GetComponent<UIWeaponEquitem>();
        o.transform.parent = transform;
        o.transform.position = Vector3.zero;
        o.transform.rotation = Quaternion.identity;
        
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 220, 100, 30), "测试创建Item"))
        {
            GameObject w1 = Instantiate(baseWeaponItem.gameObject);
            w1.transform.parent = transform;
            w1.transform.position = Vector3.zero;
            w1.transform.rotation = Quaternion.identity;
            w1.SetActive(true);

            w1.GetComponent<UIWeaponEquitem>().setWeapIcon(WeaponType.W1);
        }
    }
}
