using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AE_Holder[] aE_Holders;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
    }
    void Start()
    {
        SetaE_Events();
    }

    private void SetaE_Events()
    {
        foreach(AE_Holder holder in aE_Holders)
        {
            foreach(AudioEvent audioEvent in holder.events)
            {
                audioEvent.TryBegin();
            }
        }

    }

}
