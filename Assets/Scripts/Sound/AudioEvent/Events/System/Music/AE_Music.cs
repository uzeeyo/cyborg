using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Event/Music")]
public class AE_Music : AudioEvent
{
    [SerializeField] private S_Music[] musics;

    private EventInstance SoundInstance;

    private E_Music currentMusic;

    public override bool TryBegin()
    {
        EventHub.E_Music += Play;
        currentMusic = E_Music.stop;
        return true;
    }
    private void Play(E_Music music)
    {
        if (music == currentMusic)
            return;

        if (music == E_Music.stop)
        {
            ForgetCurrentSound();
            currentMusic = music;
        }
        else
        {
            foreach (S_Music mus in musics)
            {
                if (mus.e_Music == music)
                {
                    ForgetCurrentSound();
                    currentMusic = music;
                    PlayNewSound(mus.reference);
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