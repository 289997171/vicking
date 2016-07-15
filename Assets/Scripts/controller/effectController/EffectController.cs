using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Person))]
public class EffectController : MonoBehaviour
{

#if UNITY_EDITOR
    public List<EffectGroup> effectGroupList = new List<EffectGroup>();
#endif

    public Dictionary<string, EffectGroup> effectGroupMap = new Dictionary<string, EffectGroup>();

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

        EffectGroup[] groups = this.transform.GetComponentsInChildren<EffectGroup>();

#if UNITY_EDITOR
        effectGroupList.AddRange(groups);
#endif
        foreach (EffectGroup group in groups)
        {
            effectGroupMap.Add(group.name, group);

            //group.gameObject.SetActive(false);
        }

    }

    public void playEffect(string effectInfo)
    {
        string[] splits = effectInfo.Split(',');
        string effectName = splits[0];

        EffectGroup effectGroup;
        if (effectGroupMap.TryGetValue(effectName, out effectGroup))
        {
            //effectGroup.gameObject.SetActive(true);
            effectGroup.play(priority);
        }
    }

    private EffectPriority priority = EffectPriority.PERFECT;

#if UNITY_EDITOR
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, Screen.height - 40 * 1, 100, 30), "完美特效"))
        {
            this.priority = EffectPriority.PERFECT;
        }

        if (GUI.Button(new Rect(10, Screen.height - 40 * 2, 100, 30), "优秀特效"))
        {
            this.priority = EffectPriority.EXCELLENT;
        }

        if (GUI.Button(new Rect(10, Screen.height - 40 * 3, 100, 30), "一般特效"))
        {
            this.priority = EffectPriority.NORMAL;
        }

        if (GUI.Button(new Rect(10, Screen.height - 40 * 4, 100, 30), "低特效"))
        {
            this.priority = EffectPriority.LOW;
        }
    }
#endif
}
