using UnityEngine;

public abstract class BossPhase : ScriptableObject
{
    protected BossPhaseManager bossPhaseManager;
    public virtual void Begin(BossPhaseManager bossPhaseManager) 
    { 
        this.bossPhaseManager = bossPhaseManager;
    }
    public abstract void Tick();
    public abstract void MustEnd();
    public virtual void End()
    {
        bossPhaseManager.PhaseEnded();
    }
}
