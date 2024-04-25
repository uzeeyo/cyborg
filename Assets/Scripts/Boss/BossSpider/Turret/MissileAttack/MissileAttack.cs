using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : MonoBehaviour
{
    [SerializeField] EventReference eventReference;
    [SerializeField] private SpiderMissile prefabMissile;
    public Action AttackEnded;
    private Animator animator;
    private float AnimationTime = 1;
    private float CoolDown = 0.45f;

    private short Ammo;
    private short MaxAmmo = 5;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Begin()
    {
        EventHub.PlayOneShotSound(eventReference);
        StartCoroutine(Operations());
        Ammo = MaxAmmo;
        SpiderMissile.Count = 0;
    }
    IEnumerator Operations()
    {
        animator.SetTrigger("Missile");
        yield return new WaitForSeconds(AnimationTime);
        while(Ammo > 0)
        {
            Ammo--;
            Instantiate(prefabMissile,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(CoolDown);
        }

        animator.SetTrigger("End");
        yield return new WaitForSeconds(AnimationTime * 2);
        End();
    }

    private void End()
    {
        AttackEnded?.Invoke();
    }
}
