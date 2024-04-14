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
            ObserveInput();
        }

        private void OnDestroy()
        {
            StopObserveInput();
        }
        private void ObserveInput()
        {
            InputGameplay.Instance.E_LeftClick = LeftClick;
            InputGameplay.Instance.E_LeftClickCancel = LeftClickCancel;
            InputGameplay.Instance.E_Reload = Reload;
            InputGameplay.Instance.E_Interact = Interact;
            InputGameplay.Instance.E_Inventory = Inventory;
        }

        private void StopObserveInput()
        {
            if (InputGameplay.Instance.E_LeftClick == LeftClick)
                InputGameplay.Instance.E_LeftClick = null;
            if (InputGameplay.Instance.E_LeftClickCancel == LeftClickCancel)
                InputGameplay.Instance.E_LeftClickCancel = null;
            if (InputGameplay.Instance.E_Reload == Reload)
                InputGameplay.Instance.E_Reload = null;
            if (InputGameplay.Instance.E_Interact == Interact)
                InputGameplay.Instance.E_Interact = null;
            if (InputGameplay.Instance.E_Inventory == Inventory)
                InputGameplay.Instance.E_Inventory = null;
        }

        private void LeftClick()
        {
            print("leftclick");
            _equipment.Weapon.Shoot();
        }

        private void LeftClickCancel()
        {
            print("leftclickcancel");
        }
        
        private void Reload()
        {
            print("r");
        }

        private void Interact()
        {
            print("e");
        }

        private void Inventory()
        {
            print("openInventory");
            InputModManager.Instance.UIMod();
        }
    }
}