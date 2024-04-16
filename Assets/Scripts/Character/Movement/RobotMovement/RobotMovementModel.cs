using UnityEngine;

public class RobotMovementModel : MonoBehaviour
{
    public Rigidbody2D rb;

    public float SpeedNormal;

    public float SpeedSprint;

    [HideInInspector] public float SpeedCurrent;

    [HideInInspector] public Vector2 DesiredDirection = Vector2.zero;
    [HideInInspector] public Vector2 RotateDirection = Vector2.zero;
}
