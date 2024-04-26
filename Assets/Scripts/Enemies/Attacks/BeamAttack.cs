using System.Collections;
using UnityEngine;

namespace Cyborg.Enemies
{
    public class BeamAttack : EnemyAttack
    {
        private ParticleSystem _hitEffect;
        private PlayerMovement _player;
        private PlayerHealth _playerHealth;
        private Vector2 _targetPosition;

        [SerializeField] private float _shootTime;
        [SerializeField] private LineRenderer[] _beams;
        [SerializeField] private AnimationCurve _beamCurve;
        [SerializeField] private float _hitRadius;

        private void Awake()
        {
            _hitEffect = GetComponentInChildren<ParticleSystem>();
            _player = FindObjectOfType<PlayerMovement>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
        }

        public override void Attack(Transform playerTranform)
        {
            _targetPosition = playerTranform.position;
            ResetBeam();
            StartCoroutine(LaunchBeam());
        }

        private IEnumerator LaunchBeam()
        {
            ResetBeam();
            float timeElapsed = 0;

            while (timeElapsed < _shootTime)
            {
                var t = timeElapsed / _shootTime;
                foreach (var beam in _beams)
                {
                    var pathToTarget = (_targetPosition - (Vector2)beam.transform.position) * _beamCurve.Evaluate(t) + (Vector2)beam.transform.position;

                    beam.SetPosition(1, pathToTarget);
                }
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _hitEffect.transform.position = _targetPosition;
            _hitEffect.Play();
            if (Vector2.Distance(_player.transform.position, _targetPosition) < _hitRadius)
            {
                _playerHealth.TakeDamage(_damage);
            }


            timeElapsed = 0;
            float dissolveDuration = 1f;
            while (timeElapsed < dissolveDuration)
            {
                foreach (var beam in _beams)
                {
                    beam.material.SetFloat("_DissolvePercent", timeElapsed / dissolveDuration);
                }
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            RaiseAttackComplete();
        }

        private void ResetBeam()
        {
            _beams[0].SetPosition(0, _beams[0].transform.position);
            _beams[1].SetPosition(0, _beams[1].transform.position);
            foreach (var beam in _beams)
            {
                beam.SetPosition(1, beam.transform.position);
                beam.material.SetFloat("_DissolvePercent", 0);
            }
        }
    }
}