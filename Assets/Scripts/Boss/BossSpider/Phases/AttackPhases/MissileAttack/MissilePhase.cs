using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BossPhase/MissileAttack")]
public class MissilePhase : BossPhase
{
    private MissileAttack missileAttack;

    public override void Begin(BossPhaseManager bossPhaseManager)
    {
        base.Begin(bossPhaseManager);

        if (missileAttack == null)
        {
            missileAttack = bossPhaseManager.GetComponentInChildren<MissileAttack>();
            missileAttack.AttackEnded = End;
        }
        missileAttack.Begin();
    }
    public override void End()
    {
        PhaseEndEvent?.Invoke();
    }
}
