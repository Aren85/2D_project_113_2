using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public interface IInputReader
{
    Vector2 Direction { get; }
}

[CreateAssetMenu(fileName = "InputReader", menuName ="Platformer/Input/InputReader")]
public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions, IInputReader
{
    public event UnityAction<Vector2> Move = delegate { };


    PlayerInputActions inputActions;

    public Vector2 Direction => inputActions.Player.Move.ReadValue<Vector2>();

    private void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInputActions();
            inputActions.Player.SetCallbacks(this);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        
    }
    public void OnFire2(InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        
    }


}