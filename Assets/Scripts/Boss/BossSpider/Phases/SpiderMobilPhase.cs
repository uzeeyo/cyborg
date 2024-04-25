using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BossPhase/MobilePhase")]
public class SpiderMobilPhase : BossPhase
{
    public SpiderPhaseManager spiderPhaseManager {  get; private set; }
    [SerializeField] private SpiderTurret spiderTurret;

    [SerializeField] private BossPhase[] AttackPhases;

    private short CurrentPhase;

    private bool Continue;
    public override void Begin(BossPhaseManager bossPhaseManager)
    {
        Continue = true;
        base.Begin(bossPhaseManager);
        spiderPhaseManager = (SpiderPhaseManager)bossPhaseManager;
        spiderTurret = bossPhaseManager.GetComponentInChildren<SpiderTurret>();
        CurrentPhase = 0;
        GoNextAttackPhase();
    }
    public override void MustEnd()
    {
        Continue = false;
    }

    public override void Tick()
    {
        
    }
    private void PhaseEnded()
    {
        if(Continue)
        {
            GoNextAttackPhase();
        }
        else
        {
            End();
        }
    }

    private void GoNextAttackPhase()
    {
        AttackPhases[CurrentPhase].Begin(bossPhaseManager,PhaseEnded);

        CurrentPhase++;
        if(CurrentPhase == AttackPhases.Length )
            CurrentPhase = 0;
    }

}
