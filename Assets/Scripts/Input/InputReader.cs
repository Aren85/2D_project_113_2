using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName ="Platformer/Input/InputReader")]
public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate { };



    PlayerInputActions inputActions;

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