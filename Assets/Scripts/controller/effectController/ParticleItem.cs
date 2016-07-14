using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleItem : MonoBehaviour {

    [SerializeField]
    private List<ParticleSystem> particleSystemsList = new List<ParticleSystem>();

    void Start()
    {
        ParticleSystem[] particleSystems = this.transform.GetComponentsInChildren<ParticleSystem>();
        particleSystemsList.AddRange(particleSystems);

        foreach (ParticleSystem item in particleSystems)
        {
            item.startDelay = 0f;
        }
    }

    public void play()
    {
        foreach (ParticleSystem item in particleSystemsList)
        {
            item.Play(false);
            //Debug.LogError("ps::::::" + item.name);
        }
    }


}
