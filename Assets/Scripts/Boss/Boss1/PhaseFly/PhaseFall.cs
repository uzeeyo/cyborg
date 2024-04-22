using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BossPhase/fall")]
public class PhaseFall : BossPhase
{
    [SerializeField] string str;
    public override void Begin(BossPhaseManager bossPhaseManager)
    {
        base.Begin(bossPhaseManager);
    }
    public override void Tick()
    {
    }
    public override void MustEnd()
    {
        End();
    }
    public override void End()
    {
        Destroy(bossPhaseManager.gameObject);
    }
}
