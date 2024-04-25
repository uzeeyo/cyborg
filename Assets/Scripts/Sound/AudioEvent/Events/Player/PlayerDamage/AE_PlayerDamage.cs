using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/Player/PlayerDamage")]
public class AE_PlayerDamage : AudioEvent
{
    [SerializeField] EventReference eventReference;

    public override bool TryBegin()
    {
        if (eventReference.IsNull)
            return false;

        EventHub.E_PlayerDamage += Play;
        return true;
    }
    private void Play()
    {
        RuntimeManager.PlayOneShot(eventReference);
    }
}
