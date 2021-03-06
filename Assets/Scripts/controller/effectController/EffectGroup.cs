﻿using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
[RequireComponent(typeof(EffectPriority))]
public class EffectGroup : MonoBehaviour
{

    //#if UNITY_EDITOR
    //    [SerializeField]
    //#endif
    //    private List<ParticleSystem> particleSystemList = new List<ParticleSystem>();
    //
    //#if UNITY_EDITOR
    //    [SerializeField]
    //#endif
    //    private List<MeshReadererEffect> meshReadererEffectList = new List<MeshReadererEffect>();

#if UNITY_EDITOR
    [SerializeField]
#endif
    private List<EffectItem> effectList_LOW = new List<EffectItem>();
#if UNITY_EDITOR
    [SerializeField]
#endif
    private List<EffectItem> effectList_NORMAL = new List<EffectItem>();
#if UNITY_EDITOR
    [SerializeField]
#endif
    private List<EffectItem> effectList_EXCELLENT = new List<EffectItem>();
#if UNITY_EDITOR
    [SerializeField]
#endif
    private List<EffectItem> effectList_PERFECT = new List<EffectItem>();

    void Start()
    {
        Debug.LogError("EffectGroup Start()");

        EffectItem[] effects = this.transform.GetComponentsInChildren<EffectItem>();

        foreach (EffectItem effectItem in effects)
        {
            if (!effectItem.follow && effectItem.isRootEffect)
            {
                effectItem.gameObject.SetActive(false);
                EffectItem[] effectItems = effectItem.transform.GetComponentsInChildren<EffectItem>();
                foreach (EffectItem item in effectItems)
                {
                    item.initNotFollow();
                    if (effectItem.lastTime < item.lastTime)
                    {
                        effectItem.lastTime = item.lastTime;
                    }
                }
            }

            if (effectItem.priority == EffectPriority.PERFECT)
            {
                effectList_PERFECT.Add(effectItem);
            }
            else if (effectItem.priority == EffectPriority.EXCELLENT)
            {
                effectList_PERFECT.Add(effectItem);
                effectList_EXCELLENT.Add(effectItem);
            }
            else if (effectItem.priority == EffectPriority.NORMAL)
            {
                effectList_PERFECT.Add(effectItem);
                effectList_EXCELLENT.Add(effectItem);
                effectList_NORMAL.Add(effectItem);
            }
            else if (effectItem.priority == EffectPriority.LOW)
            {
                effectList_PERFECT.Add(effectItem);
                effectList_EXCELLENT.Add(effectItem);
                effectList_NORMAL.Add(effectItem);
                effectList_LOW.Add(effectItem);
            }
        }

        //        ParticleSystem[] particleSystems = this.transform.GetComponentsInChildren<ParticleSystem>();
        //        particleSystemList.AddRange(particleSystems);
        //
        //        MeshReadererEffect[] meshReadererEffects = this.transform.GetComponentsInChildren<MeshReadererEffect>();
        //        meshReadererEffectList.AddRange(meshReadererEffects);

        // if (!this.name.EndsWith("(Clone)")) StartCoroutine(init());
    }

    //#region 将特效直接挂载到对应的节点上
    //    public string sp_mounts = "Root";
    //
    //    private Dictionary<string, Transform> sp_mountsDictionary = new Dictionary<string, Transform>();
    //
    //#if UNITY_EDITOR
    //    public List<Transform> mountsList = new List<Transform>();
    //#endif
    //
    //    public Person person;
    //
    //    public Transform personTransform;
    //
    //    public ParticleSystemController particleSystemController;
    //
    //
    //    private int maxTryCount = 10;
    //    private int i = 10;
    //
    //    private IEnumerator init()
    //    {
    //        yield return 1;
    //
    //        Transform parentTra = this.transform.parent;
    //
    //        for (;;)
    //        {
    //            yield return 1;
    //
    //            this.person = parentTra.GetComponent<Person>();
    //
    //            if (this.person != null)
    //            {
    //                this.personTransform = this.person.transform;
    //                this.particleSystemController = this.person.GetComponent<ParticleSystemController>();
    //                break;
    //            }
    //
    //            parentTra = parentTra.parent;
    //
    //            if (parentTra == null || i-- < 0)
    //            {
    //                
    //                if (maxTryCount-- < 0)
    //                {
    //                    Debug.LogError("无法找到Person节点！！！" + this.name);
    //                    break;
    //                }
    //
    //                parentTra = this.transform.parent;
    //            }
    //
    //        }
    //
    //        if (this.person == null || this.personTransform == null)
    //        {
    //            Debug.LogError("无法找到Person节点！！！" + this.name);
    //            yield break;
    //        }
    //
    //        {
    //            string[] splits = sp_mounts.Split(';');
    //            bool addInRoot = splits.Contains("Root");
    //
    //            foreach (string split in splits)
    //            {
    //                if (string.IsNullOrEmpty(split) || split.Equals("Root"))
    //                {
    //                    continue;
    //                }
    //
    //                Transform tra = this.personTransform.findInChildrens(split);
    //                if (tag == null)
    //                {
    //                    Debug.LogError("Person中无法找到：" + split + "节点" + this.name);
    //                }
    //                else
    //                {
    //                    sp_mountsDictionary.Add(split, tra);
    //#if UNITY_EDITOR
    //                    mountsList.Add(tra);
    //#endif
    //                    // TODO 需要将自己clone到对应的节点，并设置自己的disable
    //                    GameObject clone = Instantiate(this.gameObject) as GameObject;
    //                    Vector3 localPosition = clone.transform.localPosition;
    //                    Quaternion localRotation = clone.transform.localRotation;
    //                    Vector3 localScale = clone.transform.localScale;
    //                    clone.transform.parent = tra;
    //                    clone.transform.localPosition = localPosition;
    //                    clone.transform.localRotation = localRotation;
    //                    clone.transform.localScale = localScale;
    //
    //                    this.particleSystemController.particleList.Add(clone.gameObject.GetComponent<ParticleItem>());
    //                    this.particleSystemController.particleItemMap.Add(clone.name, clone.gameObject.GetComponent<ParticleItem>());
    //                }
    //            }
    //
    //            if (!addInRoot && !this.name.Contains("(Clone)"))
    //            {
    //                Destroy(this.gameObject);
    //            }
    //        }
    //    }
    //
    //#endregion


    public void play(EffectPriority priority)
    {
        //        foreach (MeshReadererEffect meshReadererEffect in meshReadererEffectList)
        //        {
        //            meshReadererEffect.play();
        //        }
        //
        //        foreach (ParticleSystem item in particleSystemList)
        //        {
        //            item.Play(false);
        //        }

        if (priority == EffectPriority.PERFECT)
        {
            foreach (EffectItem effectItem in effectList_PERFECT)
            {
                playEffect(effectItem);
            }
        }
        else if (priority == EffectPriority.EXCELLENT)
        {
            foreach (EffectItem effectItem in effectList_EXCELLENT)
            {
                playEffect(effectItem);
            }
        }
        else if (priority == EffectPriority.NORMAL)
        {
            foreach (EffectItem effectItem in effectList_NORMAL)
            {
                playEffect(effectItem);
            }
        }
        else if (priority == EffectPriority.LOW)
        {
            foreach (EffectItem effectItem in effectList_LOW)
            {
                playEffect(effectItem);
            }
        }


    }

    private void playEffect(EffectItem effectItem)
    {
        if (effectItem.follow)
        {
            effectItem.play();
        }
        else if (effectItem.isRootEffect)
        {
            GameObject clone = Instantiate(effectItem.gameObject);
            clone.SetActive(true);

            clone.transform.position = effectItem.transform.position;
            clone.transform.rotation = effectItem.transform.rotation;

            Destroy(clone, effectItem.lastTime /*+ 0.1f*/);
        }
    }
}
