using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMissile : MonoBehaviour
{
    [SerializeField] GameObject missileImage;
    [SerializeField] GameObject ShadowImage;
    [SerializeField] float GoUpSpeed;

    [SerializeField] float Scaler;

    [SerializeField] float GoUpTime;
    [SerializeField] float GoDownratio ;

    void Start()
    {
        StartCoroutine(GoUp());
    }

    // Update is called once per frame
  

    IEnumerator GoUp()
    {
        float timer = GoUpTime;
        while (timer > 0)
        {
            missileImage.transform.position += new Vector3(0, 1, 0) * GoUpSpeed * Time.deltaTime;
            missileImage.transform.localScale += new Vector3(Scaler, Scaler, Scaler) * Time.deltaTime;
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(GoDown());
    }

    IEnumerator GoDown()
    {
        missileImage.transform.rotation = Quaternion.Euler(0, 0, 180);
        float timer = GoUpTime;
        while (timer > 0)
        {
            missileImage.transform.position -= new Vector3(0, 1, 0) * GoUpSpeed * GoDownratio * Time.deltaTime;
            missileImage.transform.localScale -= new Vector3(Scaler, Scaler, Scaler) * GoDownratio * Time.deltaTime;
            timer -= Time.deltaTime * GoDownratio ;
            yield return null;
        }
        Explode();
    }
    private void Explode()
    {
        print("Expled");
        Destroy(gameObject);
    }
    
}
