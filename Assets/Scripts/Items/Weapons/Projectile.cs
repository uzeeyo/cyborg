using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Cyborg.Items
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private VisualEffect _hitEffect;

        private void Start()
        {
            StartCoroutine(DestroyAfterTime());
        }

        public void Init(Vector2 velocity, VisualEffect hitEffect)
        {
            GetComponent<Rigidbody2D>().velocity = velocity;
            _hitEffect = hitEffect;
        }

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}