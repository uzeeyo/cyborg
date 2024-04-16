using System;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] RobotMovementModel model;

    public event Action<Vector2> MovedWithSpeedOf;

    private void Start()
    {
        model.SpeedCurrent = model.SpeedNormal;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        model.rb.MovePosition(model.rb.position + model.DesiredDirection * model.SpeedCurrent * Time.fixedDeltaTime);

        MovedWithSpeedOf?.Invoke(model.DesiredDirection * model.SpeedCurrent);
    }

    private void Rotate()
    {
        transform.up = model.RotateDirection;
    }

    #region RecieveController
    public void SetDirection(Vector2 desiredDirection)
    {
        model.DesiredDirection = desiredDirection;
    }

    public void SetRotateDirection(Vector2 mousePosition)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        model.RotateDirection = direction;
    }

    public void Sprint()
    {
        model.SpeedCurrent = model.SpeedSprint;
    }

    public void SprintCancel()
    {
        model.SpeedCurrent = model.SpeedNormal;
    }

    #endregion
}
