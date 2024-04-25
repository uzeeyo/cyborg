using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserAttackSystem : MonoBehaviour
{
    public Action AttackEnded;
    private float AnimWaitTime = 1.2f;
    private float ActionTime = 1.5f;
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
        StartCoroutine(Operations());
    }

    IEnumerator Operations()
    {
        animator.SetTrigger("Laser");
        yield return new WaitForSeconds(AnimWaitTime);
        Laser.SetActive(true);

        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > ActionTime)
            {
                spiderTurret.LaserRate(0.5f);
                break;
            }
            float rate = timer / ActionTime;
            spiderTurret.LaserRate(rate - 0.5f);
            yield return null;
        }
 
        animator.SetTrigger("End");
        Laser.SetActive(false);
        yield return new WaitForSeconds(AnimWaitTime);
        End();
    }
    private void End()
    {
        AttackEnded?.Invoke();
    }
   
}
