using UnityEngine;

namespace Cyborg.Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapon/Projectile/LaunchProjectileData")]
    public class LnchProctData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public GameObject throwObject { get; private set; }
    }
}
