using System;
using UnityEngine;

namespace Cyborg.Items
{
    public class Item : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private GridCoordinate _size;
        private bool _inInventory;

        [SerializeField] private ItemData _data;

        public bool InInventory => _inInventory;
        public ItemData Data => _data;
        public GridCoordinate Size => _size;
        public Guid IdInInvetory { get; set; }

        private void Awake()
        {
            _size = _data.Size;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            //_spriteRenderer.sprite = _data.Sprite;
        }

        public void Rotate()
        {
            _size = new GridCoordinate(_size.y, _size.x);
        }

        public void PlaceInInventory()
        {
            _inInventory = true;
        }
    }
}