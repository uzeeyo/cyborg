using UnityEngine;

namespace Cyborg.Enemies
{
    public class ShockAttack : EnemyAttack
    {
        private bool _playerInRange;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInParent<Animator>();
        }

        public override void Attack(Transform targetTransform)
        {
            _animator.Play("Attack");
            if (_playerInRange)
            {
                EnergyManager.Instance.RemoveEnergy(20);
            }
            RaiseAttackComplete();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerMovement _))
            {
                _playerInRange = true;
                Debug.Log("Player hit.");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out PlayerMovement _))
            {
                _playerInRange = false;
            }
        }
    }
}