

using System;
using System.Collections;
using UnityEngine.UI;

public enum WeaponType
{
    None = -1,
    W1,
    W2,
}

public class UIWeaponEquitem : UIEquitem
{
    public void setWeapIcon(WeaponType type)
    {
        // setIcon((int)type);

        StartCoroutine(lateInit(() =>
            {
                setIcon((int)type);
            }
        ));
    }

}
