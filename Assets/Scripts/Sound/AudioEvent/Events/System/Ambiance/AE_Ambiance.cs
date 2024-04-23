using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/Ambiance")]
public class AE_Ambiance : AudioEvent
{
    [SerializeField] private S_Ambiance[] ambiances;

    private EventInstance SoundInstance;

    private E_Ambiance currentAmbiance;

    public override bool TryBegin()
    {
        EventHub.E_Ambiance += Play;
        currentAmbiance = E_Ambiance.stop;
        return true;
    }
    private void Play(E_Ambiance ambiance)
    {
        if(ambiance == currentAmbiance) 
            return;
        
        if (ambiance == E_Ambiance.stop)
        {
            ForgetCurrentSound();
        }
        else
        {
            foreach(S_Ambiance am in ambiances)
            {
                if (am.e_Ambiance == ambiance)
                {
                    ForgetCurrentSound();
                    currentAmbiance = ambiance;
                    PlayNewSound(am.reference);
                    return;
                }
            }
        }
    }

    private void StopCurrentSound()
    {
        PLAYBACK_STATE pLAYBACK_STATE;
        SoundInstance.getPlaybackState(out pLAYBACK_STATE);
        if (!pLAYBACK_STATE.Equals(PLAYBACK_STATE.STOPPED))
            SoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    private void ForgetCurrentSound()
    {
        StopCurrentSound();
        SoundInstance.release();
    }
    private void PlayNewSound(EventReference Reference)
    {
        SoundInstance = RuntimeManager.CreateInstance(Reference);
        SoundInstance.start();
    }
}

