using System;
using UnityEngine.InputSystem;

[System.Serializable]
public class InputMenu : Inputs.IMenuActions
{
    #region Instantiation
    private static InputMenu instance;

    public static InputMenu Instance
    {
        get
        {
            if (instance == null)
                instance = new InputMenu();
            return instance;
        }
    }

    public InputMenu()
    {
        inputs = new Inputs();
        inputs.Menu.SetCallbacks(this);
    }

    private Inputs inputs;
    #endregion

    #region Activation
    public void Activate()
    {
        inputs.Menu.Enable();
    }

    public void Deactivate()
    {
        inputs.Menu.Disable();
    }
    #endregion

    #region Events

    public Action E_Esc;

    #endregion
    
    public void OnEsc(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            E_Esc?.Invoke();       
    }
}
