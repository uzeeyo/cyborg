using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum E_SpiderTurretPhases { none, plasma, laser, missile}
public class SpiderTurret : MonoBehaviour
{
    private E_SpiderTurretPhases currentPhase = E_SpiderTurretPhases.none;

    private float LerpSpeed = 1;

    public void SetTargetRotation(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, LerpSpeed);
    }
    public void ChangePhase(E_SpiderTurretPhases phase)
    {
        if (phase == currentPhase)
            return;

        currentPhase = phase;

        switch (currentPhase)
        {
            case E_SpiderTurretPhases.plasma:
                StartCoroutine(PlasmaPhase());
                break;
            case E_SpiderTurretPhases.laser:
                StartCoroutine(LaserPhase());
                break;
            case E_SpiderTurretPhases.missile:
                StartCoroutine(MissilePhase());
                break;
            default:
                break;

        }
    }

    private IEnumerator PlasmaPhase()
    {
        yield return null;
    }
    private IEnumerator LaserPhase()
    {
        yield return null;
    }
    private IEnumerator MissilePhase()
    {
        yield return null;
    }
}