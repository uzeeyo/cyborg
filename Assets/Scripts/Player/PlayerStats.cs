using System;
using UnityEngine;

namespace Cyborg.Player
{
    public class PlayerStats : MonoBehaviour
    {
        private float _health;
        private float _movementSpeed = 10f;

        [SerializeField] private float _maxHealth = 100f;

        public event Action<float> HealthChanged;
        public event Action<float> MaxHealthChanged;

        private void Awake()
        {
            _health = _maxHealth;
        }

        public void Heal(float amount)
        {
            _health += amount;
            HealthChanged?.Invoke(_health);
        }

        public void IncreaseMaxHealth(float amount)
        {
            _maxHealth = amount;
            MaxHealthChanged?.Invoke(_maxHealth);
        }

        public void TakeDamage(float amount)
        {
            _health -= amount;
            HealthChanged?.Invoke(_health);
            if (_health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Debug.Log("Player died");
        }
    }
}