using UnityEngine;

public class RobotMovementModel : MonoBehaviour
{
    public Rigidbody2D rb;

    public float SpeedNormal;

    public float SpeedDash;

    public float DashTime;

    public float DashCoolDown;

    public float DashEnergy;

    [HideInInspector] public bool DashReady = true;

    [HideInInspector] public Vector2 DashDirection;

    [HideInInspector] public float SpeedCurrent;

    [HideInInspector] public Vector2 DesiredDirection = Vector2.zero;
    [HideInInspector] public Vector2 RotateDirection = Vector2.zero;

    [HideInInspector] public bool IsDashActive;


}
