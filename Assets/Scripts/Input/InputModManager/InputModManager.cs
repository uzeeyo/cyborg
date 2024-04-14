
public enum E_InputMod { GamePlay, UI, Menu, NoInput}
public class InputModManager 
{
    private static InputModManager instance;

    public static InputModManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InputModManager();
            return instance;
        }
    }

    public InputModManager()
    {
        InputGameplay.Instance.E_GoMenu = MenuMod;
        InputUI.Instance.E_Esc = GamePlayMod;
        InputMenu.Instance.E_Esc = GamePlayMod;
    }

    public void GamePlayMod()
    {
        DisableInputMenu();
        DisableInputUI();

        EnableInputGameplay();
    }

    public void UIMod()
    {
        DisableInputMenu();
        DisableInputGameplay();

        EnableInputUI();
    }
    public void MenuMod()
    {
        DisableInputGameplay();
        DisableInputUI();

        EnableInputMenu();
    }

    public void NoInputMod()
    {
        DisableInputGameplay();
        DisableInputUI();
        DisableInputMenu();
    }

    #region Enable Disable Functions
    private void EnableInputGameplay()
    {
        InputGameplay.Instance.Activate();
    }

    private void DisableInputGameplay()
    {
        InputGameplay.Instance.Deactivate();
    }

    private void EnableInputUI()
    {
        InputUI.Instance.Activate();
    }

    private void DisableInputUI()
    {
        InputUI.Instance.Deactivate();
    }

    private void EnableInputMenu()
    {
        InputMenu.Instance.Activate();
    }
    private void DisableInputMenu()
    {
        InputMenu.Instance.Deactivate();
    }
    #endregion
}
