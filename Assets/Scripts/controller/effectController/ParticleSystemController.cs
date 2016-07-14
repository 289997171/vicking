
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParticleSystemController : MonoBehaviour
{
    public List<ParticleItem> particleList = new List<ParticleItem>();

    public Dictionary<string, ParticleItem> particleItemMap = new Dictionary<string, ParticleItem>();

    void Start()
    {
        ParticleItem[] items = this.transform.GetComponentsInChildren<ParticleItem>();
        particleList.AddRange(items);

        foreach (ParticleItem item in items)
        {
            particleItemMap.Add(item.name, item);

            //item.gameObject.SetActive(false);
        }

//        StartCoroutine(test());
    }

//    private IEnumerator test()
//    {
//        yield return new WaitForSeconds(5);
//        playParticle("skill_00_01");
//        yield return new WaitForSeconds(5);
//        playParticle("skill_00_01");
//        yield return new WaitForSeconds(5);
//        playParticle("skill_00_01");
//        yield return new WaitForSeconds(5);
//        playParticle("skill_00_01");
//    }

    public void playParticle(string particleInfo)
    {
        //Debug.LogError("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        string[] splits = particleInfo.Split(';');

        string particleName = splits[0];


        ParticleItem particleItem;
        if (particleItemMap.TryGetValue(particleName, out particleItem))
        {
            //particleItem.gameObject.SetActive(true);
            particleItem.play();
        }

    }
}
