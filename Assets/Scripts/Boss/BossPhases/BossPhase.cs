using UnityEngine;
using System;

public abstract class BossPhase : ScriptableObject
{
    protected Action PhaseEndEvent;
    protected BossPhaseManager bossPhaseManager;
    public virtual void Begin(BossPhaseManager bossPhaseManager) 
    { 
        this.bossPhaseManager = bossPhaseManager;
    }
    public virtual void Begin(BossPhaseManager bossPhaseManager, Action phaseEndEvent)
    {
        Begin(bossPhaseManager);
        PhaseEndEvent = phaseEndEvent;
    }
    public virtual void Tick() { }
    public virtual void AttackEnded() { }
    public virtual void MustEnd() { }
    public virtual void End()
    {
        bossPhaseManager.PhaseEnded();
    }
}
