using Cyborg.Items;
using UnityEngine;

namespace Cyborg.Player
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private WeaponData _testWeapon;

        private Weapon _weapon;

        public Weapon Weapon => _weapon;

        private void Awake()
        {
            _weapon = GetComponentInChildren<Weapon>();

        }

        public void EquipWeapon(WeaponData data)
        {
            _weapon.Data = data;
            //Change the sprite
        }
    }
}