using System;
using UnityEngine;

namespace Cyborg.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private DialogBox _dialogBox;
        [SerializeField] private Dialog _openingDialog;

        private Action _postDialogAction;

        private static DialogManager s_instance;

        public static DialogManager Instance => s_instance;
        [field: SerializeField] public float TimeBetweenChars = 0.5f;

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _dialogBox.gameObject.SetActive(false);
        }

        private void Start()
        {
            Show(_openingDialog);
        }

        public void Show(Dialog dialog, Action action = null)
        {
            InputModManager.Instance.UIMod();

            _dialogBox.gameObject.SetActive(true);
            _dialogBox.LoadDialog(dialog);

            _postDialogAction = action;
        }

        public void Hide()
        {
            InputModManager.Instance.GamePlayMod();

            _dialogBox.gameObject.SetActive(false);
            _postDialogAction?.Invoke();
        }

    }
}