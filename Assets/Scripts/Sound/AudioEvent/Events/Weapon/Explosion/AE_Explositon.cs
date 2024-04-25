using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/Weapon/Explosion")]
public class AE_Explositon : AudioEvent
{
    [SerializeField] EventReference refe;
    public override bool TryBegin()
    {
        EventHub.E_Explosion += Play;
        return true;
    }

    private void Play()
    {
        RuntimeManager.PlayOneShot(refe);
    }
}
