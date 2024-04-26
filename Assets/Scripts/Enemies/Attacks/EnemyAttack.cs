using System;
using UnityEngine;

namespace Cyborg.Enemies
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [SerializeField] protected float _damage;

        public event Action AttackComplete;

        public abstract void Attack(Transform targetTransform);

        protected void RaiseAttackComplete()
        {
            AttackComplete?.Invoke();
        }
    }
}