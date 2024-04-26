using System.Collections;
using UnityEngine;

namespace Cyborg.Enemies
{
    public class ProjectileAttack : EnemyAttack
    {
        private PlayerHealth _playerHealth;

        [SerializeField] private ScoutProjectile _projectilePrefab;
        [SerializeField] private float _projectileSpeed;

        private void Awake()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
        }

        public override void Attack(Transform targetTransform)
        {
            StartCoroutine(SpawnOverTime(targetTransform.position));
        }

        private IEnumerator SpawnOverTime(Vector2 targetPosition)
        {
            for (int i = 0; i < 3; i++)
            {
                var projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
                var direction = (targetPosition - (Vector2)transform.position).normalized;
                projectile.Init(direction * _projectileSpeed, _damage, _playerHealth);
                projectile.transform.up = direction;
                yield return new WaitForSeconds(0.25f);
            }
            RaiseAttackComplete();
        }
    }
}