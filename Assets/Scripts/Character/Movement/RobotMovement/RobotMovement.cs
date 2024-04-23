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
        if (model.IsDashActive)
        {
            Dash();
        }
        else
        {
            Move();
        }
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

    #region Dash
    private void StartDash()
    {
        model.IsDashActive = true;
        model.DashReady = false;

        model.DashDirection = model.DesiredDirection;

        Invoke("DashEnd", model.DashTime);

        EventHub.PlayerDash();

    }
    private void Dash()
    {
        model.rb.MovePosition(model.rb.position + model.DashDirection * model.SpeedDash * Time.fixedDeltaTime);
    }

    private void DashEnd()
    {
        model.IsDashActive = false;
        Invoke("DashReady", model.DashCoolDown);

    }

    private void DashReady()
    {
        model.DashReady = true;
    }
    #endregion

    #region RecieveController
    public void SetDirection(Vector2 desiredDirection)
    {
        model.DesiredDirection = desiredDirection;
    }

    public void SetRotateDirection(Vector2 mousePosition)
    {
        Vector3 mousePos = GlobalObjects.MainCamera.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        model.RotateDirection = direction;
    }

    public void Sprint()
    {
        if (model.IsDashActive || !model.DashReady)
            return;
        if (model.DesiredDirection == Vector2.zero)
            return;
        if (EnergyManager.Instance.TryToRemoveEnergy(model.DashEnergy))
            StartDash();
    }


    public void SprintCancel()
    {

    }

    #endregion
}
