using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AE_Holder aE_Holder;
    private List<AudioEvent> aE_Events;

    void Start()
    {
        SetaE_Events();

    }

    private void SetaE_Events()
    {
        aE_Events = aE_Holder.events;

        List<AudioEvent> removeList = new List<AudioEvent>();
        
        foreach (AudioEvent aEvent in aE_Events)
        {
            if (!aEvent.TryBegin())
            {
                removeList.Add(aEvent);
            }
        }

        foreach (AudioEvent aEvent in removeList)
        {
            print("Fail Sound Event " +  aEvent.name);
            aE_Events.Remove(aEvent);
        }
    }
   
}
