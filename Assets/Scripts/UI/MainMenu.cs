using UnityEngine;
using UnityEngine.UI;

namespace Cyborg.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(LoadGame);
        }

        private void LoadGame()
        {
            LevelManager.OpenMainScene();
        }
    }
}