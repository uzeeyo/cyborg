using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cyborg.Enemies
{
    [RequireComponent(typeof(Image))]
    public class StatusIcon : MonoBehaviour
    {
        private Dictionary<IconType, Sprite> _iconMap;
        private Image _image;
        private bool _isShowing;

        [SerializeField] private AnimationCurve _resizeCurve;
        [SerializeField] private float _displayTime;

        [Header("Icons")]
        [SerializeField] private Sprite _detectIcon;
        [SerializeField] private Sprite _dieIcon;

        public enum IconType
        {
            Detect,
            Die
        }

        private void Awake()
        {
            transform.localScale = Vector3.zero;
            _image = GetComponent<Image>();
            _iconMap = new()
            {
                { IconType.Detect, _detectIcon },
                { IconType.Die, _dieIcon }
            };
        }

        private void Update()
        {
            var newPosition = new Vector2(transform.parent.position.x, transform.parent.position.y + 1);
            transform.position = newPosition;
            transform.up = Vector3.up;
        }

        public void Show(IconType iconType)
        {
            _image.sprite = _iconMap[iconType];
            StartCoroutine(Resize());
        }

        private IEnumerator Resize()
        {
            if (_isShowing) yield break;

            var timer = 0f;
            var startScale = Vector3.zero;
            var endScale = Vector3.one;

            _isShowing = true;
            while (timer < _displayTime)
            {
                timer += Time.deltaTime;
                var t = _resizeCurve.Evaluate(timer / _displayTime);
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
                yield return null;
            }
            _isShowing = false;
        }
    }
}