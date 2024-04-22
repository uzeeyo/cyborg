using UnityEngine;

namespace Cyborg.Enemies
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        protected Vector2 _targetPosition;
        public abstract void Attack(Vector2 targetPosition);
    }
}