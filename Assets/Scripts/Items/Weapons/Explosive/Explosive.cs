using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] protected ExplosiveData data;

    private void Start()
    {
        if(data.IsExplodeWithTime)
        {
            Invoke("Explode", data.ExplodeTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!data.IsExplodeWithCollision)
            return;

        if (collision.GetComponent<I_TakeDamage>() != null)
            Explode();
    }
    public void Explode()
    {
        foreach(I_TakeDamage damagable in GetAllDamagable())
        {
            damagable.TakeDamage(data.Damage);
        }
        PlaySound();
        Destroy(gameObject);
    }

    protected I_TakeDamage[] GetAllDamagable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, data.Radius);
        List<I_TakeDamage> damagables = new List<I_TakeDamage>();

        foreach (Collider2D collider in colliders)
        {
            I_TakeDamage IsDamagable = collider.GetComponent<I_TakeDamage>();
            if(IsDamagable != null)
            {
                damagables.Add(IsDamagable);
            }
        }
        return damagables.ToArray();
    }

    protected void PlaySound()
    {
        EventHub.Explosion();
    }
}
