using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderExplossion : Explosive
{
 
    void Start()
    {
        Boom();
    }

    private void Boom()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, data.Radius);
        List<I_TakeDamage> damagables = new List<I_TakeDamage>();

        foreach (Collider2D collider in colliders)
        {
            I_TakeDamage IsDamagable = collider.GetComponent<I_TakeDamage>();
            if (IsDamagable != null)
            {
                if ("SpiderLayer" != LayerMask.LayerToName(collider.gameObject.layer))
                    IsDamagable.TakeDamage(data.Damage);
            }
        }
        PlaySound();
        Destroy(gameObject);
    }
    
}
