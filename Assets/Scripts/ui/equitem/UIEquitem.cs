
using System;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Images))]
public abstract class UIEquitem : MonoBehaviour
{

    protected Images images;

    protected void Awake()
    {
        this.images = GetComponent<Images>();
        Debug.Log("UIEquitem Start");
    }

    protected void setIcon(int type)
    {
        this.images.changeImage(type);
    }

    protected IEnumerator lateInit(Action init)
    {
        yield return 1;

        init();
    }
}
