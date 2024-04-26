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

        [SerializeField] private HitEffect _hitVfx;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            StartCoroutine(DestroyAfterSeconds());
        }


        public void Init(Vector2 velocity, float damage)
        {
            _rb.velocity = velocity;
            _damage = damage;
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
                EnergyManager.Instance.RemoveEnergy(_damage);
            }

            Instantiate(_hitVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}