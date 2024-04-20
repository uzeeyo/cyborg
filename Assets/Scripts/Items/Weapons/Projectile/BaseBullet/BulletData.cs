using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Cyborg.Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapon/Projectile/BulletData")]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public float Damage { get; private set; }

        [field: SerializeField] public VisualEffect hitEffect { get; private set; }
    }
}
