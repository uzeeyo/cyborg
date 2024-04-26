using UnityEngine;

namespace Cyborg.Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapon/Projectile/BulletData")]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public float Damage { get; private set; }

        [field: SerializeField] public ParticleSystem hitEffect { get; private set; }
    }
}
