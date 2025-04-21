using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorkflowEmbeddedActions : MonoBehaviour
{
    public InputAction moveAction;

    public float moveSpeed = 5f;
    public float time = 60;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }

    void Update()
    {
        Move();
    }
    void FixedUpdate()
    {

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        //Debug.Log(Time.deltaTime);
    }
    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }
    void Move()
    {
        Vector2 moveAmount = moveAction.ReadValue<Vector2>();
        transform.Translate(moveAmount * moveSpeed * Time.deltaTime);
    }
}
