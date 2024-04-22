using System;
using Unity.Mathematics;
using UnityEngine;

public class BossHealth : MonoBehaviour, I_TakeDamage
{
    public event Action<float> E_HealtRateChanged;
    [field:SerializeField] protected short MaxHealt { get; private set; }

    protected float Health;

    [SerializeField] protected bool IsDamagable;

    private void Start()
    {
        Health = (float)MaxHealt;
    }
    public virtual void TakeDamage(float damage)
    {
        if(IsDamagable)
            RecieveDamage(damage);
    }

    protected virtual void RecieveDamage(float damage)
    {
        Health -= damage;
        Health = math.max(Health, 0);
        FireHealtChanged();
    }

    protected void FireHealtChanged()
    {
        if (E_HealtRateChanged == null)
            return;
        E_HealtRateChanged(Health / MaxHealt);
    }

    public void SetDamagable(bool isDamagable)
    {
        IsDamagable = isDamagable;
    }
}
