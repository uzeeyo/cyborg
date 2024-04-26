using UnityEngine;

public class LevelStartInput : MonoBehaviour
{
    [SerializeField] E_InputMod StartMod;
    [SerializeField] E_Music music = E_Music.stop;
    [SerializeField] E_Ambiance ambiance = E_Ambiance.stop;
    void Start()
    {
        int a = 3;
        EventHub.Ambiance(ambiance);
        EventHub.Music(music);
        EnergyManager.Instance.EnergyIsMax();
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
