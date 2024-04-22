using System.Collections;
using UnityEngine;

namespace Cyborg.Enemies
{
    public class BeamAttack : EnemyAttack
    {
        private const float MAX_LENGTH = 15;
        private float _startLength;
        private float length;

        [SerializeField] private float _shootTime;
        [SerializeField] private LineRenderer[] _beams;
        [SerializeField] private AnimationCurve _beamCurve;

        public override void Attack(Vector2 position)
        {
            _targetPosition = position;
            _startLength = _beams[0].GetPosition(0).y;

            StartCoroutine(LaunchBeam());
        }

        private IEnumerator LaunchBeam()
        {
            float startTime = Time.time;
            while (Time.time - startTime < _shootTime)
            {
                length = _beamCurve.Evaluate((Time.time - startTime) / _shootTime) * MAX_LENGTH + _startLength;
                foreach (var beam in _beams)
                {
                    beam.SetPosition(1, new Vector3(beam.GetPosition(0).x, length, 0));
                }
                yield return null;
            }

            startTime = Time.time;
            float dissolveDuration = 1f;
            foreach (var beam in _beams)
            {
                beam.transform.SetParent(transform.root, true);
            }
            while (Time.time - startTime < dissolveDuration)
            {
                foreach (var beam in _beams)
                {
                    beam.material.SetFloat("_DissolvePercent", Time.time - startTime / dissolveDuration);
                }
                yield return null;
            }

        }
    }
}