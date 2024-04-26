using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, I_TakeDamage
{
    private bool alive = true;
    float DashInvincibleTime = 0.5f;
    private short DashCount;
    [SerializeField] GameObject EnergyGiver;
    void Start()
    {
        EventHub.E_PlayerDash += PlayerDashed;
        EventHub.E_EnergyEnded += Die;
        StartCoroutine(spawn());
    }
    private void OnDestroy()
    {
        EventHub.E_PlayerDash -= PlayerDashed;
        EventHub.E_EnergyEnded -= Die;
    }

    private IEnumerator spawn()
    {
        Instantiate(EnergyGiver, transform.position+GetLoc(),Quaternion.identity);
        yield return new WaitForSeconds(5);
        StartCoroutine(spawn());
    }

    private Vector3 GetLoc()
    {
        return new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0).normalized * 5;
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
