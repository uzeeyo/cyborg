using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/PlayerMovement")]
public class AE_PlayerMovement : AudioEvent
{
    [SerializeField] EventReference eventReference;

    private EventInstance SoundInstance;

    public override bool TryBegin()
    {
        if (eventReference.IsNull)
            return false;

        SoundInstance = RuntimeManager.CreateInstance(eventReference);
        SoundInstance.start();
        SendSpeed(Vector2.zero);

        EventHub.E_PlayerMoveSpeed += SendSpeed;
        return true;
    }

    public void SendSpeed(Vector2 speed)
    {
        SoundInstance.setParameterByName("PlayerSpeed", speed.magnitude);
    }
    public override void Destroy()
    {
        SoundInstance.stop((FMOD.Studio.STOP_MODE.IMMEDIATE));
        SoundInstance.release();

        EventHub.E_PlayerMoveSpeed -= SendSpeed;
    }

}
