using Cyborg.Items;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cyborg.Player
{
    [RequireComponent(typeof(Image))]
    public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        const float HIGHLIGHT_AMOUNT = 0.7f;

        private Image _background;
        private Inventory _inventory;
        private GridCoordinate _position;
        private Item _item;
        private Item _itemToClear;
        private Guid _idToClear;
        private Color _originalColor;
        private Guid _itemIdInSlot;

        public bool Empty
        {
            get
            {
                return _item == null || _itemIdInSlot == default;
            }
        }
        public Item Item => _item;
        public Guid IdToClear => _idToClear;

        private void Awake()
        {
            _background = GetComponent<Image>();
            _originalColor = _background.color;
        }

        public void Init(Inventory inventory, GridCoordinate position)
        {
            _inventory = inventory;
            _position = position;
        }

        public void PlaceItem(Item item)
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

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out TestDroppableItem item))
            {
                _inventory.Unhiglight();

                if (item.Item.InInventory && _inventory.TryMoveItem(item.Item, _position))
                {
                    Debug.Log("Item moved.");
                    item.transform.SetParent(transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    item.transform.SetParent(_inventory.transform, true);
                    return;
                }

                if (_inventory.TryAddItem(item.Item, _position))
                {
                    Debug.Log("Item placed in inventory slot.");
                    item.transform.SetParent(transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    item.transform.SetParent(_inventory.transform, true);
                    return;
                }

                Debug.Log("Item could not be placed in inventory slot.");
                item.ResetPosition();
                return;
            }
            Debug.Log("Non droppable item attempted to be dropped in inventory slot.");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent(out TestDroppableItem item) && Empty)
            {
                _inventory.Unhiglight();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Empty)
            {
                Debug.Log("Slot is not empty.");
            }

            if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent(out TestDroppableItem item) && Empty)
            {
                _inventory.Highlight(_position, item.Item.Size);
            }
        }
    }
}