using Cyborg.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Cyborg.Player
{
    public class Inventory : MonoBehaviour
    {
        private List<InventoryItem> _items = new();
        private List<InventorySlot> _highlightedSlots;
        private InventorySlot[,] _grid = new InventorySlot[4, 3]; //TODO: get from player stats
        private GridLayoutGroup _gridGroup;
        private Guid _tempIdToClear;

        [SerializeField] private GameObject _window;
        [SerializeField] private InventorySlot _slotPrefab;
        [SerializeField] private InventoryItem _itemPrefab;

        private static Inventory _instance;

        public static Inventory Instance => _instance;
        public InventoryItem ItemPrefab => _itemPrefab;
        public InventoryItem ItemToDrop { get; set; }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            _gridGroup = GetComponentInChildren<GridLayoutGroup>(true);
        }

        private GridCoordinate FindEmptySpace(GridCoordinate size)
        {
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    if (VerifySpaceEmpty(new GridCoordinate(x, y), size))
                    {
                        return new GridCoordinate(x, y);
                    }

                    //Check if the item fits rotated
                    if (VerifySpaceEmpty(new GridCoordinate(x, y), new GridCoordinate(y, x)))
                    {
                        return new GridCoordinate(y, x);
                    }
                }
            }
            return new GridCoordinate(-1, -1);
        }

        private bool VerifySpaceEmpty(GridCoordinate position, GridCoordinate size)
        {
            if (!_grid[position.x, position.y].Empty) return false;

            if (position.x + size.x > _grid.GetLength(0) || position.y + size.y > _grid.GetLength(1)) return false;

            for (int x = position.x; x < position.x + size.x; x++)
            {
                for (int y = position.y; y < position.y + size.y; y++)
                {
                    if (!_grid[x, y].Empty) return false;
                }
            }

            return true;
        }

        public void ToggleItemRaycasts()
        {
            foreach (var item in _items)
            {
                var image = item.GetComponent<Image>();
                image.raycastTarget = !image.raycastTarget;
            }
        }

        public InventoryItem CreateInventoryItem(Item item)
        {
            var inventoryItem = Instantiate(_itemPrefab, _window.transform);
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

            return inventoryItem;
        }

        public void PlaceItemInWindow(InventoryItem item)
        {
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            item.transform.SetParent(_window.transform, true);
            ItemToDrop = null;
        }

        private void Init()
        {
            foreach (var slot in _grid)
            {
                if (slot != null)
                {
                    Destroy(slot.gameObject);
                }
            }

            _gridGroup.constraintCount = _grid.GetLength(0);

            for (int y = 0; y < _grid.GetLength(1); y++)
            {
                for (int x = 0; x < _grid.GetLength(0); x++)
                {
                    var slot = Instantiate(_slotPrefab, _gridGroup.transform);
                    slot.Init(this, new GridCoordinate(x, y));
                    _grid[x, y] = slot;
                }
            }
        }

        public void Open()
        {
            InputModManager.Instance.UIMod();
            _window.gameObject.SetActive(true);
            Init();
            InputUI.Instance.E_Esc += Close;
        }

        public void Close()
        {
            InputUI.Instance.E_Esc -= Close;
            _window.gameObject.SetActive(false);
            InputModManager.Instance.GamePlayMod();
        }

        public void Unhiglight()
        {
            if (_highlightedSlots == null) return;

            foreach (var slot in _highlightedSlots)
            {
                slot.ToggleHighlight();
            }
            _highlightedSlots = null;
        }


        public void Highlight(GridCoordinate position, GridCoordinate size)
        {
            _highlightedSlots = GetSlots(position, size);
            if (_highlightedSlots == null) return;
            foreach (var slot in _highlightedSlots)
            {
                slot.ToggleHighlight();
            }
        }

        public List<InventorySlot> GetSlots(GridCoordinate position, GridCoordinate size)
        {
            if (position.x + size.x > _grid.GetLength(0) || position.y + size.y > _grid.GetLength(1))
            {
                return null;
            }

            var slots = new List<InventorySlot>();

            for (int x = position.x; x < position.x + size.x; x++)
            {
                for (int y = position.y; y < position.y + size.y; y++)
                {
                    if (!_grid[x, y].Empty) return null;
                    slots.Add(_grid[x, y]);
                }
            }

            return slots;
        }

        public bool TryMoveItem(InventoryItem item, GridCoordinate newPosition)
        {
            if (!VerifySpaceEmpty(newPosition, item.Size))
            {
                //item.GetComponent<DroppableItem>().ResetPosition();
                return false;
            }

            item.InventoryPosition = newPosition;
            foreach (var slot in _grid)
            {
                if (slot.Item != null && slot.Item.Id == item.Id)
                {
                    slot.Clear();
                }
            }

            FillGrid(item, newPosition, item.Size);

            return true;
        }

        public bool TryAddItem(InventoryItem inventoryItem)
        {
            var position = FindEmptySpace(inventoryItem.Size);

            //No space found
            if (position.x == -1) return false;

            inventoryItem.Rotate();

            _items.Add(inventoryItem);
            FillGrid(inventoryItem, position, inventoryItem.Size);

            return true;
        }

        public bool TryAddItem(InventoryItem inventoryItem, GridCoordinate position)
        {
            if (!VerifySpaceEmpty(position, inventoryItem.Size)) return false;

            _items.Add(inventoryItem);
            FillGrid(inventoryItem, position, inventoryItem.Size);

            return true;
        }

        public GridCoordinate GetItemPosition(Guid id)
        {
            return _items.Where(item => item.Id == id).Select(item => item.InventoryPosition).First();
        }

        private void FillGrid(InventoryItem item, GridCoordinate position, GridCoordinate size)
        {
            for (int x = position.x; x < position.x + size.x; x++)
            {
                for (int y = position.y; y < position.y + size.y; y++)
                {
                    _grid[x, y].PlaceItem(item);
                }
            }
        }

        public void UndoTempRemoveItem()
        {
            foreach (var slot in _grid)
            {
                if (slot.IdToClear == _tempIdToClear)
                {
                    slot.Unclear();
                }
            }
        }

        public void TempRemoveItem(Guid id)
        {
            _tempIdToClear = id;
            foreach (var slot in _grid)
            {
                if (!slot.Empty && slot.Item.Id == id)
                {
                    slot.Clear();
                }
            }
        }

        public void RemoveItem(Guid id)
        {
            var itemToRemove = _items.Where(item => item.Id == id);
            if (itemToRemove.Count() != 0)
            {
                _items.Remove(itemToRemove.First());
            }
        }

        public void DeleteItem(Item item)
        {
            //_items.Remove(item.Data);
        }
    }
}