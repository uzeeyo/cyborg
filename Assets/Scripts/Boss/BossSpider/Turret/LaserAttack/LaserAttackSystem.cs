using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserAttackSystem : MonoBehaviour
{
    public Action AttackEnded;
    private float AnimWaitTime = 1f;
    public void Begin()
    {
        StartCoroutine(Operations());
    }

    IEnumerator Operations()
    {
        //start anim,
        yield return new WaitForSeconds(AnimWaitTime);
        //StartShooting
        //End anim
        yield return new WaitForSeconds(AnimWaitTime);
        End();
    }
    private void End()
    {
        AttackEnded?.Invoke();
    }
   
}
