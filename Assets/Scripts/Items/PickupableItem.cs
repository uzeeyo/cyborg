using Cyborg.Player;
using Cyborg.UI;
using UnityEngine;

namespace Cyborg.Items
{
    public class PickupableItem : MonoBehaviour
    {
        private ItemWindow _itemWindow;
        private InventoryItem _iconObj;

        public Item Item { get; private set; }

        private void Awake()
        {
            Item = GetComponent<Item>();
            _itemWindow = FindObjectOfType<ItemWindow>();
        }

        public void PickUp()
        {
            _itemWindow.Open();
            _iconObj = _itemWindow.CreateInventoryItem(Item);

            Inventory.Instance.ToggleItemRaycasts();

            InputUI.Instance.E_Cancel += CancelPickup;
        }

        private void CancelPickup()
        {
            InputUI.Instance.E_Cancel -= CancelPickup;
            if (_iconObj != null)
            {
                Destroy(_iconObj.gameObject);
            }

            _itemWindow.Close();
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            InputUI.Instance.E_Cancel -= CancelPickup;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            InputGameplay.Instance.E_Interact += PickUp;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            InputGameplay.Instance.E_Interact -= PickUp;
        }
    }
}