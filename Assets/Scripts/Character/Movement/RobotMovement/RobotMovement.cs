using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        model.rb.MovePosition(model.rb.position + model.DesiredDirection * model.SpeedCurrent * Time.fixedDeltaTime);

        MovedWithSpeedOf?.Invoke(model.DesiredDirection * model.SpeedCurrent);
    }

    #region RecieveController
    public void SetDirection(Vector2 desiredDirection)
    {
        model.DesiredDirection = desiredDirection;
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
