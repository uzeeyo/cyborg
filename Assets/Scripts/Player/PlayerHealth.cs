using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, I_TakeDamage
{
    [SerializeField] float DashInvincibleTime;
    private short DashCount;
    void Start()
    {
        EventHub.E_PlayerDash += PlayerDashed;
    }
    private void OnDestroy()
    {
        EventHub.E_PlayerDash -= PlayerDashed;
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

    public void TakeDamage(float damage)
    {
        EnergyManager.Instance.RemoveEnergy(damage);
    }
}
