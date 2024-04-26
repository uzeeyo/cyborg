using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPhaseManager : MonoBehaviour
{
    [field:SerializeField] public BossHealth bossHealth { get; private set; }

    [field: SerializeField] public NavMeshAgent navMeshAgent { get; private set; }

    [SerializeField] private S_BossPhase[] Phases;

    private short indexPhase;

    private void Start()
    {
        bossHealth.E_HealtRateChanged += HealtDecreased;
        StartBossFight();
    }
    public void StartBossFight()
    {
        StartCoroutine(BossFight());
    }

    IEnumerator BossFight()
    {
        yield return null;
        GoNextPhase();
        while(true)
        {
            yield return null;
            Phases[indexPhase].bossPhase.Tick();
        }
    }
    public void PhaseEnded()
    {
        indexPhase++;
        if (indexPhase < Phases.Length)
        {
            GoNextPhase();
        }
    }
    private void GoNextPhase()
    {
        Phases[indexPhase].bossPhase.Begin(this);
    }

    private void HealtDecreased(float rate)
    {
        if (Phases[indexPhase].EndHealthRate > rate)
        {
            Phases[indexPhase].bossPhase.MustEnd();
        }
    }

    public virtual void EndBossFight()
    {
        StopAllCoroutines();
    }

}
