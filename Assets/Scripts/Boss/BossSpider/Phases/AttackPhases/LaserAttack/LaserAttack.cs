using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BossPhase/LaserAttack")]
public class LaserAttack : BossPhase
{
    private LaserAttackSystem laserAttack;

    public override void Begin(BossPhaseManager bossPhaseManager)
    {
        base.Begin(bossPhaseManager);

        if (laserAttack == null)
        {
            laserAttack = bossPhaseManager.GetComponentInChildren<LaserAttackSystem>();
            laserAttack.AttackEnded = End;
        }
        laserAttack.Begin();
    }
    public override void End()
    {
        PhaseEndEvent?.Invoke();
    }
}
