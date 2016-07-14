
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> effectList = new List<ParticleSystem> ();

    void Start()
    {
        ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
        Debug.Log("particles: " + particleSystems.Length);
        effectList.AddRange(particleSystems);
    }
}
