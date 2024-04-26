using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BossPhase/EndPhase")]
public class EndBoss : BossPhase
{
    [SerializeField] Sprite sp;
    public override void Begin(BossPhaseManager bossPhaseManager)
    {
        base.Begin(bossPhaseManager);
        bossPhaseManager.GetComponentInChildren<SpiderTurret>().gameObject.SetActive(false);
        bossPhaseManager.GetComponent<SpriteRenderer>().sprite = sp;
        bossPhaseManager.GetComponent<SpiderLegHealth>().EndBoss();
    }
}
