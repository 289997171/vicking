using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Person))]
public class EffectController : MonoBehaviour
{

#if UNITY_EDITOR
    public List<EffectItem> effectList = new List<EffectItem>();
#endif

    public Dictionary<string, EffectItem> effectItemMap = new Dictionary<string, EffectItem>();

    //    public Person person;
    //
    //    public Transform cacheTransform;
    //
    //    public string mounts = "Up;Medium;Down;R_Weapon;L_Weapon;Arms01;Bip01 Prop1";
    //
    //    public Dictionary<string, Transform> mountsDictionary = new Dictionary<string, Transform>();
    //
    //#if UNITY_EDITOR
    //    public List<Transform> mountsList = new List<Transform>(); 
    //#endif

    //    public string Up = "Up"; // 血条
    //    public string Medium = "Medium"; // 被击点
    //    public string Down = "Down";// 阴影挂点
    //
    //    public string R_Weapon = "R_Weapon";// 右手
    //    public string L_Weapon = "L_Weapon";// 左手
    //    
    //    public string Arms01 = "Arms01"; // 背部武器挂点
    //    public string Bip01_Prop1 = "Bip01 Prop1"; // 武器骨骼,由于是双手武器，挂点要挂在武器骨骼上。


    void Start()
    {
        //        {
        //            this.person = this.GetComponent<Person>();
        //            this.cacheTransform = this.transform;
        //            string[] splits = mounts.Split(';');
        //            foreach (string split in splits)
        //            {
        //                Transform tra = this.transform.findInChildrens(split);
        //                if (tag == null)
        //                {
        //                    mountsDictionary.Add(split, cacheTransform); // 找不到节点，默认以
        //                }
        //                else
        //                {
        //                    mountsDictionary.Add(split, tra);
        //#if UNITY_EDITOR
        //                    mountsList.Add(tra);
        //#endif
        //                }
        //            }
        //        }
        //        

        EffectItem[] items = this.transform.GetComponentsInChildren<EffectItem>();

#if UNITY_EDITOR
        effectList.AddRange(items);
#endif
        foreach (EffectItem item in items)
        {
            effectItemMap.Add(item.name, item);

            //item.gameObject.SetActive(false);
        }

    }





    public void playParticle(string effectInfo)
    {
        string[] splits = effectInfo.Split(',');
        string effectName = splits[0];

        EffectItem effectItem;
        if (effectItemMap.TryGetValue(effectName, out effectItem))
        {
            //effectItem.gameObject.SetActive(true);
            effectItem.play();
        }
    }
}
