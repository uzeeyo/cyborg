using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegHealth : MonoBehaviour,I_TakeDamage
{
    private float Health = 100;
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health < 0)
            Brake();
    }

    private void Brake()
    {
        
    }
}
