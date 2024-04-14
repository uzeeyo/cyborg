using Cyborg.Items;
using System;
using System.Collections.Generic;
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

        [SerializeField] private InventorySlot _slotPrefab;

        private void Awake()
        {
            _gridGroup = GetComponentInChildren<GridLayoutGroup>();
        }

        private void Start()
        {
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

        public bool TryAddItem(Item item, GridCoordinate position)
        {

            if (!VerifySpaceEmpty(position, item.Size)) return false;

            item.PlaceInInventory();
            var invetoryItem = new InventoryItem(item.Data);
            item.IdInInvetory = invetoryItem.Id;
            _items.Add(invetoryItem);
            FillGrid(invetoryItem, position, item.Size);

            return true;
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
                if (slot.ItemData.Id == id)
                {
                    slot.Clear();
                }
            }
        }

        public void DeleteItem(Item item)
        {
            //_items.Remove(item.Data);
        }
    }
}