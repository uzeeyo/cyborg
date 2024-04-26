using UnityEngine;

namespace Cyborg.Items
{
    public class Bullet : Projectile
    {
        [SerializeField] private BulletData bulletData;

        private void Start()
        {
            StartCoroutine(DestroyAfterTime());
        }
        public override void SetDirection(Vector2 direction)
        {
            SetVelocity(direction * bulletData.Speed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryToGiveDamage(collision, bulletData.Damage);

            PlayVisualHitEffect();
            Destroy(gameObject);
        }
        private void PlayVisualHitEffect()
        {
            Instantiate(bulletData.hitEffect, transform.position, transform.rotation);
        }

    }
}
