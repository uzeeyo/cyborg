using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/Weapon/WeaponShot")]
public class AE_WeaponShot : AudioEvent
{
    public override bool TryBegin()
    {
        EventHub.E_PlayOneShotSound += Play;
        return true;
    }

    private void Play(EventReference eventReference)
    {
        RuntimeManager.PlayOneShot(eventReference);
    }
}
