using System;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class InputGameplay : Inputs.IGamePlayActions
{
    #region Instantiation
    private static InputGameplay instance;

    public static InputGameplay Instance
    {
        get
        {
            if (instance == null)
                instance = new InputGameplay();
            return instance;
        }
    }

    public InputGameplay()
    {
        inputs = new Inputs();
        inputs.GamePlay.SetCallbacks(this);
    }

    private Inputs inputs;
    #endregion

    #region Activation
    public void Activate()
    {
        inputs.GamePlay.Enable();
    }

    public void Deactivate()
    {
        inputs.GamePlay.Disable();
    }
    #endregion

    #region Events

    public Action<Vector2> E_Move;

    public Action E_SprintStart;

    public Action E_SprintCancel;

    public Action E_LeftClick;

    public Action E_LeftClickCancel;

    public Action E_Interact;

    public Action E_Reload;

    public Action E_Inventory;

    public Action<Vector2> E_Rotate;

    public Action E_GoMenu;

    #endregion

    void Inputs.IGamePlayActions.OnMove(InputAction.CallbackContext context)
    {
        E_Move?.Invoke(context.ReadValue<Vector2>());
    }

    void Inputs.IGamePlayActions.OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_SprintStart?.Invoke();

        if (context.phase == InputActionPhase.Canceled)
            E_SprintCancel?.Invoke();
    }

    void Inputs.IGamePlayActions.OnLeftClickMouse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_LeftClick?.Invoke();

        if (context.phase == InputActionPhase.Canceled)
            E_LeftClickCancel?.Invoke();
    }
    void Inputs.IGamePlayActions.OnGoMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_GoMenu?.Invoke();
    }
    void Inputs.IGamePlayActions.OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_Interact?.Invoke();
    }
    void Inputs.IGamePlayActions.OnReload(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_Reload?.Invoke();
    }
    void Inputs.IGamePlayActions.OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_Inventory?.Invoke();
    }

    void Inputs.IGamePlayActions.OnRotate(InputAction.CallbackContext context)
    {
        E_Rotate?.Invoke(context.action.ReadValue<Vector2>());
    }
}
