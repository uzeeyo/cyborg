using Cyborg.Items;
using UnityEngine;

namespace Cyborg.UI
{
    public class ItemWindow : MonoBehaviour
    {

        [SerializeField] private GameObject _itemWindow;
        [SerializeField] private InventoryItem _itemPrefab;
        public InventoryItem ItemToDrop { get; set; }

        public void Open()
        {
            InputModManager.Instance.UIMod();
            _itemWindow.gameObject.SetActive(true);
            InputUI.Instance.E_Esc += Close;
            EventHub.InventoryOpened();
        }

        public void Close()
        {
            InputUI.Instance.E_Esc -= Close;
            _itemWindow.gameObject.SetActive(false);
            InputModManager.Instance.GamePlayMod();
            EventHub.InventoryClosed();
        }

        public void PlaceItemInWindow(InventoryItem item)
        {
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            item.transform.SetParent(_itemWindow.transform, true);
            ItemToDrop = null;
        }

        public InventoryItem CreateInventoryItem(Item item)
        {
            var inventoryItem = Instantiate(_itemPrefab, _itemWindow.transform);
            inventoryItem.WorldItem = item;
            inventoryItem.StartPlacement(item.Data);
            if (item.Data is EquipmentData)
            {
                var data = item.Data as EquipmentData;
                EquippableItem component = data.SlotType switch
                {
                    SlotType.Head => inventoryItem.gameObject.AddComponent<HeadEquippableItem>(),
                    SlotType.Arm => inventoryItem.gameObject.AddComponent<ArmEquippableItem>(),
                    SlotType.Leg => inventoryItem.gameObject.AddComponent<LegEquippableItem>(),
                    _ => null
                };
                component.Init(item.Data as EquipmentData);
            }
            ItemToDrop = inventoryItem;
            return inventoryItem;
        }
    }
}