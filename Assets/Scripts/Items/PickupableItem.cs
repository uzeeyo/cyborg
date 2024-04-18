using Cyborg.Player;
using UnityEngine;

namespace Cyborg.Items
{
    public class PickupableItem : MonoBehaviour
    {
        private InventoryItem _iconObj;

        public Item Item { get; private set; }
        public InventoryItem Icon => _iconObj;

        private void Awake()
        {
            Item = GetComponent<Item>();
        }

        public void PickUp()
        {
            Inventory.Instance.Open();
            _iconObj = Inventory.Instance.CreateInventoryItem(Item);

            Inventory.Instance.ToggleItemRaycasts();
            Inventory.Instance.ItemToDrop = _iconObj;

            InputUI.Instance.E_Cancel += CancelPickup;
        }

        private void CancelPickup()
        {
            InputUI.Instance.E_Cancel -= CancelPickup;
            if (_iconObj != null)
            {
                Destroy(_iconObj.gameObject);
            }

            Inventory.Instance.Close();
            StopAllCoroutines();
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