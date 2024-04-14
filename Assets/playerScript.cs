using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Rigidbody rb;
    float speed = 5;

    Vector2 inputdirection;

    void Start()
    {
        //InputGamePlay.Instance.Move = Move;
        rb = GetComponent<Rigidbody>();
        InputGameplay.Instance.E_Move = Move2;
        InputGameplay.Instance.E_SprintStart = Sprint;
        InputGameplay.Instance.E_SprintCancel = SprintStop;
        InputModManager.Instance.UIMod();

    }

    private void Update()
    {
        
    }

    private void Sprint()
    {
        speed = 10;
    }

    private void SprintStop()
    {
        speed = 5;
    }
    private void Move2(Vector2 direction)
    {
        inputdirection = direction;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(new Vector3(inputdirection.x, 0, inputdirection.y) * speed * Time.fixedDeltaTime + transform.position);
    }


}
