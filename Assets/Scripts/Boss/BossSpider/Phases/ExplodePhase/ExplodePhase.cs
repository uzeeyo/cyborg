using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BossPhase/ExplodePhase")]
public class ExplodePhase : BossPhase
{
    [SerializeField] private ExplodeEffect effect;
    [SerializeField] float Strength;
    [SerializeField] private float MaxAmmo;
    private float Ammo;
    private float sayac;
    private float TargetTime;
    Vector3 spiderpos;
    public override void Begin(BossPhaseManager bossPhaseManager)
    {
        Debug.Log("Begin");
        base.Begin(bossPhaseManager);

        Ammo = MaxAmmo;
        sayac = 0;
        TargetTime = Random.Range(0.2f, 0.5f);
        if (spiderpos == Vector3.zero) 
        {
            spiderpos = GameObject.FindWithTag("Spider").transform.position;
        }
    }

    public override void Tick()
    {
        sayac += Time.deltaTime;
        if (sayac > TargetTime) 
        {     
            if(Ammo==0)
            {
                End();
            }
            TargetTime = Random.Range(0.2f, 0.5f);
            sayac = 0;
            Ammo--;
            Explode();
        }
    }

    private void Explode()
    {
        ExplodeEffect ef = Instantiate(effect, GetPosition(), Quaternion.identity);
        ef.SetRadius(Strength);
        EventHub.Explosion();
    }

    private Vector3 GetPosition()
    {
 
        return spiderpos + new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.2f, 2.6f), 0);
    }
    
}
