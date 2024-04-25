using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : MonoBehaviour
{
    public Action AttackEnded;
    public void Begin()
    {
        StartCoroutine(Operations());
    }
    IEnumerator Operations()
    {
        print("MissileBegin");
        yield return null;
        print("MissileEnd");
        End();
    }

    private void End()
    {
        AttackEnded?.Invoke();
    }
}
