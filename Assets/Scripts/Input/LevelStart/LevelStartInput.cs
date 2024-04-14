using UnityEngine;

public class LevelStartInput : MonoBehaviour
{
    [SerializeField] E_InputMod StartMod;
    void Start()
    {
        switch (StartMod)
        {
            case E_InputMod.GamePlay:
                InputModManager.Instance.GamePlayMod();
                break;
            case E_InputMod.UI:
                InputModManager.Instance.UIMod();
                break;
            case E_InputMod.Menu:
                InputModManager.Instance.MenuMod();
                break;
            case
                E_InputMod.NoInput:
                InputModManager.Instance.NoInputMod();
                break;
            default:
                break;
        }
    }

}
