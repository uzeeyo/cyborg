using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGiver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnergyManager.Instance.AddEnergy(40);
        }
        Destroy(gameObject);
    }
}
