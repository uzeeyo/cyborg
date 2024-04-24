using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private bool Stop;
    [SerializeField] private bool Environment;
    [SerializeField] private bool War;
    [SerializeField] private bool Stop2;
    [SerializeField] private bool LevelMusic;
    [SerializeField] BossHealth damage;
    void Start()
    {
        Invoke("RemoveHealth", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Stop)
        {
            Stop = false;
            EventHub.Ambiance(E_Ambiance.stop);
        }
        if (Environment)
        {
            Environment = false;
            EventHub.Ambiance(E_Ambiance.EnvironmentalNoise);
        }
        if (War)
        {
            War = false;
            EventHub.Ambiance(E_Ambiance.BackgroundWarZone);
        }
        if (Stop2)
        {
            Stop2 = false;
            EventHub.Music(E_Music.stop);
        }
        if (LevelMusic)
        {
            LevelMusic = false;
            EventHub.Music(E_Music.LVL1);
        }
    }

    private void RemoveHealth()
    {
        //EventHub.Ambiance(E_Ambiance.EnvironmentalNoise);
        //print("Fire ambiance");
        //Invoke("RemoveHealth", 1);

        damage.TakeDamage(90);
    }
}
