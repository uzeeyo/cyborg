using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private RobotMovement robotMovement;

    void Start()
    {
        ObserveInput();
        ConnectionMovement_EventHub(true);
    }

    private void OnDestroy()
    {
        StopObserveInput();
        ConnectionMovement_EventHub(false);
    }

    private void ConnectionMovement_EventHub(bool Connect)
    {
        if (Connect)
        {
            robotMovement.MovedWithSpeedOf += EventHub.PlayerMoveSpeed;
        }
        else
        {
            robotMovement.MovedWithSpeedOf -= EventHub.PlayerMoveSpeed;
        }
    }

    private void ObserveInput()
    {
        InputGameplay.Instance.E_Move = SendMove;
        InputGameplay.Instance.E_SprintStart = SendSprintStart;
        InputGameplay.Instance.E_SprintCancel = SendSprintCancel;
        InputGameplay.Instance.E_Rotate = SendRotate;
    }

    private void StopObserveInput()
    {
        if (InputGameplay.Instance.E_Move == SendMove)
            InputGameplay.Instance.E_Move = null;
        if (InputGameplay.Instance.E_SprintStart == SendSprintStart)
            InputGameplay.Instance.E_SprintStart = null;
        if (InputGameplay.Instance.E_SprintCancel == SendSprintCancel)
            InputGameplay.Instance.E_SprintCancel = null;
    }

    private void SendMove(Vector2 DesiredDirection)
    {
        robotMovement.SetDirection(DesiredDirection);
    }

    private void SendRotate(Vector2 DesiredDirection)
    {
        robotMovement.SetRotateDirection(DesiredDirection);
    }

    private void SendSprintStart()
    {
        robotMovement.Sprint();
    }

    private void SendSprintCancel()
    {
        robotMovement.SprintCancel();
    }

}
