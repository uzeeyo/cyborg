using UnityEngine;

[System.Serializable]
public struct S_BossPhase 
{
    [SerializeField] public BossPhase bossPhase;
    [Range(0f, 1f)]
    [SerializeField] private float endHealthRate;

     public float EndHealthRate => endHealthRate;
}
