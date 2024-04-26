using System.Collections;
using UnityEngine;

namespace Cyborg.Items
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        protected void SetVelocity(Vector2 velocity)
        {
            GetComponent<Rigidbody2D>().velocity = velocity;
        }

        public abstract void SetDirection(Vector2 direction);

        protected IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
        protected void TryToGiveDamage(Collision2D collision, float damage)
        {
            I_TakeDamage takeDamage = collision.gameObject.GetComponent<I_TakeDamage>();
            if (takeDamage == null)
                return;
            takeDamage.TakeDamage(damage);
        }
    }
}