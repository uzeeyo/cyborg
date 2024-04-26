using System;
using UnityEngine.InputSystem;

[System.Serializable]
public class InputUI : Inputs.IUIActions
{
    #region Instantiation
    private static InputUI instance;

    public static InputUI Instance
    {
        get
        {
            if (instance == null)
                instance = new InputUI();
            return instance;
        }
    }

    public InputUI()
    {
        inputs = new Inputs();
        inputs.UI.SetCallbacks(this);
    }

    private Inputs inputs;
    #endregion

    #region Activation
    public void Activate()
    {
        inputs.UI.Enable();
    }

    public void Deactivate()
    {
        inputs.UI.Disable();
    }
    #endregion

    #region Events

    public Action E_Esc;
    public Action E_Cancel;
    public Action E_Continue;

    #endregion
    void Inputs.IUIActions.OnEsc(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_Esc?.Invoke();
    }

    void Inputs.IUIActions.OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_Cancel?.Invoke();
    }

    public void OnContinue(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_Continue?.Invoke();
    }
}
