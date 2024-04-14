using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private RobotMovement robotMovement;

    void Start()
    {
        ObserveInput();
    }

    private void OnDestroy()
    {
        StopObserveInput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ObserveInput()
    {
        InputGameplay.Instance.E_Move = SendMove;
        InputGameplay.Instance.E_SprintStart = SendSprintStart;
        InputGameplay.Instance.E_SprintCancel = SendSprintCancel;
    }

    private void StopObserveInput()
    {
        if (InputGameplay.Instance.E_Move == SendMove)
            InputGameplay.Instance.E_Move = null;
        if (InputGameplay.Instance.E_SprintStart == SendSprintStart)
            InputGameplay.Instance.E_SprintStart = null;
        if(InputGameplay.Instance.E_SprintCancel == SendSprintCancel)
            InputGameplay.Instance.E_SprintCancel= null;
    }

    private void SendMove(Vector2 DesiredDirection)
    {
        robotMovement.SetDirection(DesiredDirection);
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
