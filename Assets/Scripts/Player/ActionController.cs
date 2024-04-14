using UnityEngine;
using UnityEngine.InputSystem;

namespace Cyborg.Player
{
    public class ActionController : MonoBehaviour
    {
        private Inventory _inventory;
        private Equipment _equipment;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _equipment = GetComponent<Equipment>();
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _equipment.Weapon.Shoot();
            }
        }

        public void OnShoot(InputAction.CallbackContext ctx)
        {
            _equipment.Weapon.Shoot();
        }

        public void OnReload(InputAction.CallbackContext ctx)
        {

        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {

        }
    }
}