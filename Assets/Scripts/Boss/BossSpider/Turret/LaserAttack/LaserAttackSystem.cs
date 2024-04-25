using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;

public class LaserAttackSystem : MonoBehaviour
{
    [SerializeField] EventReference eventReference;
    public Action AttackEnded;
    private float AnimWaitTime = 1.2f;
    private float ActionTime = 0.9f;
    [SerializeField] private GameObject Laser;
    private Animator animator;
    private SpiderTurret spiderTurret;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spiderTurret = GetComponent<SpiderTurret>();
    }
    public void Begin()
    {
        EventHub.PlayOneShotSound(eventReference);
        StartCoroutine(Operations());
    }

    IEnumerator Operations()
    {
        animator.SetTrigger("Laser");
        StartCoroutine(BeginingTurretMove());
        yield return new WaitForSeconds(AnimWaitTime);
        spiderTurret.LaserTime(true);
        Laser.SetActive(true);

        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > ActionTime)
            {
                spiderTurret.SetLaserRate(0.5f);
                break;
            }
            float rate = timer / ActionTime;
            spiderTurret.SetLaserRate(rate - 0.5f);
            yield return null;
        }
 
        animator.SetTrigger("End");
        Laser.SetActive(false);
        spiderTurret.SetLaserRate(0);
        spiderTurret.LaserTime(false);
        yield return new WaitForSeconds(AnimWaitTime);
        End();
    }

    IEnumerator BeginingTurretMove()
    {
        yield return new WaitForSeconds(AnimWaitTime * 0.9f);
        float timer = 0f;
        while (timer < AnimWaitTime) 
        {
            timer += Time.deltaTime * 10f;
            timer = Mathf.Min(timer, AnimWaitTime);
            spiderTurret.SetLaserRate(-0.5f * (timer / AnimWaitTime));
            yield return null;
        }
    }
    
    private void End()
    {
        AttackEnded?.Invoke();
    }
   
}
