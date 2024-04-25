using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : MonoBehaviour
{
    public Action AttackEnded;
    private Animator animator;
    private float AnimationTime = 1;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Begin()
    {
        StartCoroutine(Operations());
    }
    IEnumerator Operations()
    {
        animator.SetTrigger("Missile");
        yield return new WaitForSeconds(AnimationTime);

        animator.SetTrigger("End");
        yield return new WaitForSeconds(AnimationTime);
        End();
    }

    private void End()
    {
        AttackEnded?.Invoke();
    }
}
