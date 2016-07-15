using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class EffectItem : MonoBehaviour
{

#if UNITY_EDITOR
    [SerializeField]
#endif
    private List<ParticleSystem> particleSystemList = new List<ParticleSystem>();

#if UNITY_EDITOR
    [SerializeField]
#endif
    private List<MeshReadererLifeTimer> mesgReadererLifeTimerList = new List<MeshReadererLifeTimer>();



    void Start()
    {
        Debug.LogError("ParticleItem Start()");

        ParticleSystem[] particleSystems = this.transform.GetComponentsInChildren<ParticleSystem>();
        particleSystemList.AddRange(particleSystems);

        MeshReadererLifeTimer[] mesgReadererLifeTimers = this.transform.GetComponentsInChildren<MeshReadererLifeTimer>();
        mesgReadererLifeTimerList.AddRange(mesgReadererLifeTimers);

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


    public void play()
    {
        foreach (MeshReadererLifeTimer meshReadererLifeTimer in mesgReadererLifeTimerList)
        {
            meshReadererLifeTimer.play();
        }

        foreach (ParticleSystem item in particleSystemList)
        {
            item.Play(false);
        }
    }



}
