using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorkflowActionsAsset : MonoBehaviour
{
    public PlayerInputActions actions;


    public float moveSpeed = 5f;
    void Awake()
    {
        actions = new PlayerInputActions();
    }

    void Update()
    {
        Move();
        Debug.Log(actions.Player.Fire.IsPressed());
    }

    void OnEnable()
    {
        actions.Player.Enable();
    }

    void OnDisable()
    {
        actions.Player.Disable();
    }

    void Move()
    {
        Vector2 moveAmount = actions.Player.Move.ReadValue<Vector2>().normalized;
        transform.Translate(moveAmount * moveSpeed * Time.deltaTime);
    }
}
