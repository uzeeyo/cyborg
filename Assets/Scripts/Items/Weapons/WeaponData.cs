using UnityEngine;
using UnityEngine.VFX;

namespace Cyborg.Items
{
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "ScriptableObjects/WeaponData")]
    public class WeaponData : ItemData
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _coolDown;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private VisualEffect _hitEffect;

        public float Damage => _damage;
        public float CoolDown => _coolDown;
        public float ProjectileSpeed => _projectileSpeed;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public VisualEffect HitEffect => _hitEffect;
    }
}