using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, I_TakeDamage
{
    private bool alive = true;
    float DashInvincibleTime = 0.5f;
    private short DashCount;
    void Start()
    {
        EventHub.E_PlayerDash += PlayerDashed;
        EventHub.E_EnergyEnded += Die;
    }
    private void OnDestroy()
    {
        EventHub.E_PlayerDash -= PlayerDashed;
        EventHub.E_EnergyEnded -= Die;
    }

    private void PlayerDashed()
    {
        StartCoroutine(DashCounter());
    }
    IEnumerator DashCounter()
    {
        DashCount++;
        yield return new WaitForSeconds(DashInvincibleTime);
        DashCount--;
    }

    public void ShotBySpiderLaser(float damage)
    {
        if (DashCount != 0)
            return;
        TakeDamage(damage);
    }
    private void Die()
    {
        alive = false;
    }
    public void TakeDamage(float damage)
    {
        if (alive)
        {
            EnergyManager.Instance.RemoveEnergy(damage);
            EventHub.PlayerDamage();
        }
    }
}
