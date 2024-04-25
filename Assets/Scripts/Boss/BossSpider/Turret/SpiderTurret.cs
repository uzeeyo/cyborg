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
    private Vector3 LaserCenterRot;
    private bool IsLaserTime;

    private void Update()
    {
        if (IsLaserTime)
        {
            LaserRotSet();
        }
        else
        {
            LookAtPlayer();
        }
    }

    public void LaserTime(bool IsTime)
    {
        IsLaserTime = IsTime;
        if(!IsLaserTime)
        {
            return;
        }

        LaserCenterRot = Vector3.Lerp(CurrentRot, (GlobalObjects.Player.transform.position - transform.position).normalized, LerpSpeed * Time.deltaTime);
        

    }
    public void SetLaserRate(float rate)
    {
        LaserRate = rate;
    }

    private void LookAtPlayer()
    {
        //(GlobalObjects.Player.transform.position - transform.position).normalized;
        //Vector3.Lerp(CurrentRot, (GlobalObjects.Player.transform.position - transform.position).normalized, LerpSpeed * Time.deltaTime);
        CurrentRot =  Vector3.Lerp(CurrentRot, (CalculateWherePlayerIsGoing() - (Vector2)transform.position).normalized, LerpSpeed * Time.deltaTime);
        transform.up = Quaternion.Euler(0, 0, LaserRate * TurretRotation) * CurrentRot;
    }
    private void LaserRotSet()
    {
        transform.up = Quaternion.Euler(0, 0, LaserRate * TurretRotation) * LaserCenterRot;
    }

    private Vector2 CalculateWherePlayerIsGoing()
    {
        return RobotMovement.PlayerSpeed * 0.5f + (Vector2)GlobalObjects.Player.transform.position;
    }
}