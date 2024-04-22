using Cyborg.Items;
using Cyborg.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cyborg.Player
{
    [RequireComponent(typeof(Image))]
    public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDropHandler
    {
        const float HIGHLIGHT_AMOUNT = 0.4f;

        private Image _background;
        private Inventory _inventory;
        private GridCoordinate _position;
        private InventoryItem _item;
        private InventoryItem _itemToClear;
        private Guid _idToClear;
        private Color _originalColor;
        private Guid _itemIdInSlot;
        private ItemWindow _itemWindow;

        public bool Empty
        {
            get
            {
                return _item == null || _itemIdInSlot == default;
            }
        }

        public InventoryItem Item => _item;
        public Guid IdToClear => _idToClear;

        private void Awake()
        {
            _background = GetComponent<Image>();
            _originalColor = _background.color;
            _itemWindow = GetComponentInParent<ItemWindow>();
        }

        public void Init(Inventory inventory, GridCoordinate position)
        {
            _inventory = inventory;
            _position = position;
        }

        public void PlaceItem(InventoryItem item)
        {
            _item = item;
            _itemIdInSlot = item.Id;
        }

        public void Clear()
        {
            _idToClear = _item.Id;
            _itemToClear = _item;
            _item = null;
            _itemIdInSlot = default;
        }

        public void Unclear()
        {
            _itemIdInSlot = _idToClear;
            _item = _itemToClear;
            _itemToClear = null;
            _idToClear = default;
        }

        public void ToggleHighlight()
        {
            if (_background.color == _originalColor)
            {
                _background.color = new Color(_originalColor.r * HIGHLIGHT_AMOUNT, _originalColor.g * HIGHLIGHT_AMOUNT, _originalColor.b * HIGHLIGHT_AMOUNT);
                return;
            }
            _background.color = _originalColor;
        }

        public bool Drop()
        {
            _inventory.Unhiglight();
            var item = _itemWindow.ItemToDrop;
            if (item.InInventory && _inventory.TryMoveItem(item, _position))
            {
                Debug.Log("Item moved.");
                item.Moved = true;
                item.transform.SetParent(transform);
                _itemWindow.PlaceItemInWindow(item);
                return true;
            }

            if (_inventory.TryAddItem(item, _position))
            {
                Debug.Log("Item placed in inventory slot.");

                item.EndPlacement();
                item.transform.SetParent(transform);
                _itemWindow.PlaceItemInWindow(item);
                Destroy(item.WorldItem.gameObject);
                item.WorldItem = null;
                return true;
            }

            Debug.Log("Item could not be placed in inventory slot.");
            //item.ResetPosition();
            return false;

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_itemWindow.ItemToDrop != null && Empty)
            {
                _inventory.Unhiglight();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_itemWindow.ItemToDrop != null && Empty)
            {
                _inventory.Highlight(_position, _itemWindow.ItemToDrop.Size);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_itemWindow.ItemToDrop != null && Empty)
            {
                _inventory.Unhiglight();
                Drop();
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out InventoryItem item))
            {
                Drop();
            }
        }
    }
}