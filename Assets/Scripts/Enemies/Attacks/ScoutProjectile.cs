using Cyborg.Items;
using System.Collections;
using UnityEngine;

namespace Cyborg.Enemies
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class ScoutProjectile : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private float _damage;
        private PlayerHealth _playerHealth;

        [SerializeField] private HitEffect _hitVfx;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            StartCoroutine(DestroyAfterSeconds());
        }


        public void Init(Vector2 velocity, float damage, PlayerHealth playerHealth)
        {
            _rb.velocity = velocity;
            _damage = damage;
            _playerHealth = playerHealth;
        }

        private IEnumerator DestroyAfterSeconds()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerMovement _))
            {
                _playerHealth.TakeDamage(_damage);
            }

            Instantiate(_hitVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}