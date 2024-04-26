using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    void Start()
    {
        EventHub.Ambiance(E_Ambiance.IntroCutscene);
    }

   
    private void RemoveHealth()
    {
        //LevelManager.OpenBossScene();

        Invoke("RemoveHealth", 2);

    }
}
