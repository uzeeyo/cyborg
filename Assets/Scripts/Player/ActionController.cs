using Cyborg.Items;
using UnityEngine;

namespace Cyborg.Player
{
    public class ActionController : MonoBehaviour
    {
        private EquipmentManager _equipment;

        [SerializeField] private Inventory _inventory;

        private void Awake()
        {
            _equipment = GetComponent<EquipmentManager>();
            ObserveInput();
        }

        private void OnDestroy()
        {
            StopObserveInput();
        }
        private void ObserveInput()
        {
            InputGameplay.Instance.E_LeftClick += LeftClick;
            InputGameplay.Instance.E_LeftClickCancel += LeftClickCancel;
            InputGameplay.Instance.E_Reload += Reload;
            InputGameplay.Instance.E_Inventory += OpenInventory;
        }

        private void StopObserveInput()
        {
            InputGameplay.Instance.E_LeftClick -= LeftClick;
            InputGameplay.Instance.E_LeftClickCancel -= LeftClickCancel;
            InputGameplay.Instance.E_Reload -= Reload;
            InputGameplay.Instance.E_Inventory -= OpenInventory;
        }

        private void LeftClick()
        {
            _equipment.Weapon.Shoot();
        }

        private void LeftClickCancel()
        {
            _equipment.Weapon.ShootCancel();
        }

        private void Reload()
        {

        }


        private void OpenInventory()
        {
            _inventory.Open();
        }
    }
}