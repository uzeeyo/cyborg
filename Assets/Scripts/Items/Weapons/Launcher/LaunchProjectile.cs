using UnityEngine;

namespace Cyborg.Items
{
    public class LaunchProjectile : Projectile
    {
        [SerializeField] private LnchProctData data;

        private Vector2 TargetPos;
        private Vector2 Velocity;
        public void SetTargerPosition(Vector2 position)
        {
            TargetPos = position;
            Velocity = TargetPos - (Vector2)transform.position;
            Velocity.Normalize();
            GetComponent<Rigidbody2D>().velocity = Velocity * data.Speed;
            
        }
        private void Update()
        {
            if(HasReachedTarget())
            {
                transform.position = TargetPos;
                SpawnThrownObject();
                Destroy(gameObject);
            }
        }
        private bool HasReachedTarget()
        {
            Vector2 targetDirection = TargetPos- (Vector2)transform.position;
            float dotProduct = Vector2.Dot(targetDirection, Velocity);
            if (dotProduct <= 0)
                return true;
            return false;
        }
        private void SpawnThrownObject()
        {
            Instantiate(data.throwObject, transform.position, Quaternion.identity);
        }
    }
}
