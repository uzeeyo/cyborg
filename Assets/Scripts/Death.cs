using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject image;
    void Start()
    {
        EventHub.E_EnergyEnded += Die;
    }
    private void OnDestroy()
    {
        EventHub.E_EnergyEnded -= Die;
    }

    private void Die()
    {
        image.SetActive(true);
        EventHub.Music(E_Music.GameOver);
        InputModManager.Instance.NoInputMod();
        StartCoroutine(dieee());
    }
    IEnumerator dieee()
    {
        yield return new WaitForSeconds(5);
        LevelManager.ReloadScene();
    }
    void Update()
    {
        
    }
}
