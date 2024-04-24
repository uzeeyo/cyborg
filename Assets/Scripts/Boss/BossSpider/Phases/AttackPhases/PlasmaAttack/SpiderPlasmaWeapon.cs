using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BossPhase/PlasmaAttack")]
public class SpiderPlasmaWeapon : BossPhase
{
    private SpiderPlasmaAttack plasmaAttack;

    public override void Begin(BossPhaseManager bossPhaseManager)
    {
        base.Begin(bossPhaseManager);

        if(plasmaAttack == null)
        {
            plasmaAttack = bossPhaseManager.GetComponentInChildren<SpiderPlasmaAttack>();
            plasmaAttack.AttackEnded = End;
        }
        plasmaAttack.Begin();
    }

    public override void MustEnd()
    {
        plasmaAttack.MustEnd();
    }
    public override void End()
    {
        PhaseEndEvent?.Invoke();
    }
}
