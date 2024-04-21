using UnityEngine;
using UnityEngine.VFX;
using FMODUnity;

namespace Cyborg.Items
{
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "ScriptableObjects/Weapon/WeaponData")]
    public class WeaponData : ItemData
    {
        [field: SerializeField] public float CoolDown { get; private set; }

        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }

        [SerializeField] public VisualEffect HitEffecddt;

        [SerializeField] public EventReference _FmodEvent;
    }
}