using System.Collections;
using UnityEngine;

namespace Cyborg.Enemies
{
    public class Scanner : MonoBehaviour
    {
        private Color _originalColor;
        private SpriteRenderer _sprite;
        private float _maxRange;

        [SerializeField] private AnimationCurve _resizeCurve;
        [SerializeField] private float _scanDuration = 1;

        public float CurrentRange { get; private set; }

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _maxRange = GetComponentInParent<Enemy>().DetectionRange;
        }

        public void Scan()
        {
            _originalColor = _sprite.color;
            StartCoroutine(IncreaseRange());
        }

        private IEnumerator IncreaseRange()
        {
            float startTime = Time.time;
            while (Time.time - startTime < _scanDuration)
            {
                var timeNormalized = (Time.time - startTime) / _scanDuration;
                CurrentRange = _resizeCurve.Evaluate(timeNormalized) * _maxRange;
                transform.localScale = Vector3.one * CurrentRange;
                //_sprite.color = _originalColor * timeNormalized;
                yield return null;
            }
            transform.localScale = Vector3.zero;
        }
    }
}