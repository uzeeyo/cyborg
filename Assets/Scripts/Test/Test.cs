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
        //Invoke("RemoveHealth", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

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
