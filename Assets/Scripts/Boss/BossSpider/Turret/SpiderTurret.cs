using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiderTurret : MonoBehaviour
{
    [Range(-120, 120)]
    [SerializeField] private float TurretRotation;
    [SerializeField] private float LerpSpeed;

    [HideInInspector] public float LaserRate;
    private Vector3 CurrentRot;

    private void Update()
    {
        LookAtPlayer();
    }

    public void SetLaserRate(float rate)
    {
        LaserRate = rate;
        print(LaserRate);
    }

    private void LookAtPlayer()
    {
        CurrentRot = Vector3.Lerp(CurrentRot, (GlobalObjects.Player.transform.position - transform.position).normalized, LerpSpeed * Time.deltaTime);
        transform.up = Quaternion.Euler(0, 0, LaserRate * TurretRotation) * CurrentRot;
    }
}