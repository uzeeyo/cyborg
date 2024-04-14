using UnityEngine;

namespace Cyborg.Items
{
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "ScriptableObjects/WeaponData")]
    public class WeaponData : ItemData
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _coolDown;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private GameObject _projectilePrefab;

        public float Damage => _damage;
        public float CoolDown => _coolDown;
        public float ProjectileSpeed => _projectileSpeed;
        public GameObject ProjectilePrefab => _projectilePrefab;
    }
}