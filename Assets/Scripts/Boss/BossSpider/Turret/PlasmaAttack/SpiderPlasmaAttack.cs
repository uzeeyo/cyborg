using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPlasmaAttack : MonoBehaviour
{
    [SerializeField] private GameObject PlasmaPrefab;
    [SerializeField] private Transform FireStartTransform;
    [SerializeField] private float FireCoolDown;

    private Transform playerTransform;
    private bool Continue;
    private short ammo;
    public Action AttackEnded;
    public void Begin()
    {
        playerTransform = GlobalObjects.Player.transform;
        Continue = true;
        ammo = 3;
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
