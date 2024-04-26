using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float LaserDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        I_TakeDamage damagable = collision.GetComponent<I_TakeDamage>();

        if (damagable == null)
            return;

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().ShotBySpiderLaser(LaserDamage);
        }
        else
        {
            damagable.TakeDamage(LaserDamage);
        }

    }
}
