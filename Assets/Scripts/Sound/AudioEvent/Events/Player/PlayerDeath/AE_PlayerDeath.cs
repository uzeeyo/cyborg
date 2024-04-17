using UnityEngine;
using FMODUnity;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/PlayerDeath")]
public class AE_PlayerDeath : AudioEvent
{
    [SerializeField] EventReference eventReference;

    public override bool TryBegin()
    {
        if(eventReference.IsNull)
        return false;

        EventHub.E_PlayerDeath += Play;
        return true;
    }
    private void Play()
    {
        RuntimeManager.PlayOneShot(eventReference);
    }

}
