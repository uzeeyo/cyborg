using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegHealth : MonoBehaviour
{
    [SerializeField] GameObject SpiderBrokenLegs;
    public void EndBoss()
    {
        SpiderBrokenLegs.SetActive(true);
        StartCoroutine(SceneChange());
    }
    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(7);
        LevelManager.OpenStartMenu();
    }
}
