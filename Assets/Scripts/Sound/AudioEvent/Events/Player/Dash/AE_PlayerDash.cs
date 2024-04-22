using UnityEngine;
using FMODUnity;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/Player/PlayerDash")]

public class AE_PlayerDash : AudioEvent
{
    [SerializeField] EventReference eventReference;

    public override bool TryBegin()
    {
        if (eventReference.IsNull)
            return false;

        EventHub.E_PlayerDash += Play;
        return true;
    }
    private void Play()
    {
        RuntimeManager.PlayOneShot(eventReference);
    }

}
