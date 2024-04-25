using Cyborg.Items;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpiderPlasmaAttack : MonoBehaviour
{
    [SerializeField] private Projectile PlasmaPrefab;
    [SerializeField] private Transform FireStartTransform;
    [SerializeField] private float FireCoolDown;

    private Transform playerTransform;
    private bool Continue;
    private short ammo;
    public Action AttackEnded;
    [SerializeField] EventReference WeaponSound;
    public void Begin()
    {
        playerTransform = GlobalObjects.Player.transform;
        Continue = true;
        ammo = 40;
        StartCoroutine(FireMod());
    }

    private IEnumerator FireMod()
    {
        Fire();
        CheckAmmo();
        yield return new WaitForSeconds(FireCoolDown);
        if(Continue)
            StartCoroutine(FireMod());
        else
        {
            AttackEnded?.Invoke();
        }
    }

    private void Fire()
    {
        Projectile bullet = Instantiate(PlasmaPrefab, FireStartTransform.position, Quaternion.LookRotation(Vector3.forward, transform.up));
        bullet.SetDirection(SelectRotation());
        EventHub.PlayOneShotSound(WeaponSound);
        //bullet.SetDirection(transform.up);
    }

    private Vector3 SelectRotation()
    {
        float Randomize = 30;
        Quaternion randomRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(-Randomize, Randomize), Vector3.forward);
        Vector3 randomUp = randomRotation * transform.up;
        return randomUp;
    }

    private void CheckAmmo()
    {
        ammo--;
        if(ammo < 0)
            Continue = false;
    }

    public void MustEnd()
    {
        Continue = false;
    }
}
