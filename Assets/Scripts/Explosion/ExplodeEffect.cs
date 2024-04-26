using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{
    float rate = 1;
    public void SetRadius(float radius)
    {
        transform.localScale = new Vector3(radius, radius, radius) * rate;
    }
    void Start()
    {
        StartCoroutine(Disapear());
    }
    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}   
