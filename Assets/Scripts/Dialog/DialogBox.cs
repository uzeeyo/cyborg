using System.Collections;
using TMPro;
using UnityEngine;

namespace Cyborg.Dialog
{
    public class DialogBox : MonoBehaviour
    {
        private Dialog _dialog;
        private int _currentLine;
        private bool _skipRequested;
        private bool _animatingText;

        [SerializeField] private TextMeshProUGUI _dialogText;

        private void OnDestroy()
        {
            InputUI.Instance.E_Continue -= Continue;
        }

        public void LoadDialog(Dialog text)
        {
            _dialog = text;
            _currentLine = 0;
            StartCoroutine(MoveToNext());
            StartCoroutine(WaitForNext());
            InputUI.Instance.E_Continue += Continue;
        }

        public void Hide()
        {
            _dialog = null;
            DialogManager.Instance.Hide();
            InputUI.Instance.E_Continue -= Continue;
        }

        private void Continue()
        {
            _skipRequested = true;
        }

        private IEnumerator WaitForNext()
        {
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                if (_skipRequested && !_animatingText)
                {
                    if (_currentLine >= _dialog.Lines.Count - 1)
                    {
                        Hide();
                        yield break;
                    }
                    _skipRequested = false;
                    _currentLine++;
                    StartCoroutine(MoveToNext());
                }
                yield return null;
            }
        }

        private IEnumerator MoveToNext()
        {
            var text = _dialog.Lines[_currentLine];
            var displayedText = "";
            _animatingText = true;

            for (var i = 0; i < text.Length; i++)
            {
                if (_skipRequested)
                {
                    _dialogText.text = text;
                    _skipRequested = false;
                    break;
                }

                displayedText += text[i];
                _dialogText.text = displayedText;
                yield return new WaitForSeconds(DialogManager.Instance.TimeBetweenChars);
            }
            _animatingText = false;
        }
    }
}