using Cyborg.Items;
using Cyborg.UI;
using UnityEngine;

namespace Cyborg.Player
{
    public class ActionController : MonoBehaviour
    {
        private EquipmentManager _equipment;
        private ItemWindow _itemWindow;

        private void Awake()
        {
            _equipment = GetComponent<EquipmentManager>();
            _itemWindow = FindObjectOfType<ItemWindow>();
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

        }

        private void Reload()
        {

        }


        private void OpenInventory()
        {
            _itemWindow.Open();
        }
    }
}