using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpiderMissile : MonoBehaviour
{
    [SerializeField] GameObject missileImage;
    [SerializeField] GameObject ShadowImage;
    [SerializeField] float GoUpSpeed;

    [SerializeField] float Scaler;
    private float ShadowScaler = 0.22f;

    [SerializeField] float GoUpTime;
    [SerializeField] float GoDownratio ;

    [SerializeField] float MissileSpeed;
    [SerializeField] Explosive explosive;

    public static short Count = 0;

    void Start()
    {
        StartCoroutine(GoUp());
    }
    private void TeleportMissile()
    {
        Vector2 pos = (Vector2)GlobalObjects.Player.transform.position;
        pos -= RobotMovement.PlayerSpeed * GetFloatValue() * 0.2f;
        transform.position = pos; 
    }

    private float GetFloatValue()
    {
        if (Count == 0)
            return 1.5f;

        return 0.5f;
    }
    IEnumerator FollowPlayer()
    {
        while (true)
        {
            Vector3 direction = GlobalObjects.Player.transform.position - transform.position;
            direction = direction.normalized * MissileSpeed * Time.deltaTime;
            transform.Translate(direction);
            yield return null;
        }
    }
  

    IEnumerator GoUp()
    {
        ShadowImage.SetActive(false);
        float timer = GoUpTime;
        while (timer > 0)
        {
            missileImage.transform.position += new Vector3(0, 1, 0) * GoUpSpeed * Time.deltaTime;
            missileImage.transform.localScale += new Vector3(Scaler, Scaler, Scaler) * Time.deltaTime;
            ShadowImage.transform.localScale -= new Vector3(Scaler, Scaler, Scaler) * ShadowScaler * Time.deltaTime;
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(GoDown());
        TeleportMissile();
        StartCoroutine(FollowPlayer());
    }

    IEnumerator GoDown()
    {
        Invoke("ShowShadew", 0.3f);
        missileImage.transform.rotation = Quaternion.Euler(0, 0, 180);
        float timer = GoUpTime;
        while (timer > 0)
        {
            missileImage.transform.position -= new Vector3(0, 1, 0) * GoUpSpeed * GoDownratio * Time.deltaTime;
            missileImage.transform.localScale -= new Vector3(Scaler, Scaler, Scaler) * GoDownratio * Time.deltaTime;
            ShadowImage.transform.localScale += new Vector3(Scaler, Scaler, Scaler) * ShadowScaler * GoDownratio * Time.deltaTime;
            timer -= Time.deltaTime * GoDownratio ;
            yield return null;
        }
        Explode();
    }
    private void ShowShadew()
    {
        ShadowImage.SetActive(true);
    }
    private void Explode()
    {
        Instantiate(explosive, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}
