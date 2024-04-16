using Cyborg.Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cyborg.Items
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ItemData _data;
        private Guid _id;
        private GridCoordinate _inventoryPosition;
        private GridCoordinate _size;
        private bool _rotated;
        private bool _inInventory;
        private Image _image;
        private Coroutine _listenForRotateCoroutine;
        private Coroutine _listenForMoveCouroutine;
        private Vector2 _originalPosition;

        public Guid Id => _id;
        public ItemData Data => _data;
        public GridCoordinate Size => _size;
        public GridCoordinate InventoryPosition { get; set; }
        public Item WorldItem { get; set; }
        public bool InInventory => _inInventory;
        public bool Moved { get; set; }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _id = Guid.NewGuid();
        }

        public void StartPlacement(ItemData data)
        {
            _data = data;
            _size = _data.Size;
            GetComponent<Image>().sprite = _data.Sprite;

            Move();
        }

        private void Move()
        {
            _listenForMoveCouroutine = StartCoroutine(MoveWithMouse());
            _listenForRotateCoroutine = StartCoroutine(ListenForRotate());
        }

        public void EndPlacement()
        {
            GetComponent<Image>().raycastTarget = true;
            _inInventory = true;
            StopAllCoroutines();
        }

        private void Rotate()
        {
            _size = new GridCoordinate(_size.y, _size.x);
        }

        private IEnumerator ListenForRotate()
        {
            while (true)
            {
                //TODO: Replace with input system
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _rotated = !_rotated;
                    var rotationZ = _rotated ? -90 : 90;
                    (transform as RectTransform).Rotate(new Vector3(0, 0, rotationZ));
                    Rotate();
                }

                yield return null;
            }
        }

        private IEnumerator MoveWithMouse()
        {
            var rectTransform = transform as RectTransform;
            while (true)
            {
                rectTransform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, rectTransform.position.z);
                yield return null;
            }
        }

        public void OnBeginDrag(PointerEventData _)
        {
            if (!_inInventory) return;

            _originalPosition = (transform as RectTransform).anchoredPosition;
            _listenForRotateCoroutine = StartCoroutine(ListenForRotate());

            Inventory.Instance.ToggleItemRaycasts();
            Inventory.Instance.TempRemoveItem(_id);
            Inventory.Instance.ItemToDrop = this;
            _image.raycastTarget = false;
            Moved = false;
        }

        public void OnEndDrag(PointerEventData _)
        {
            if (!_inInventory) return;
            Debug.Log("End drag.");
            StopCoroutine(_listenForRotateCoroutine);
            Inventory.Instance.ToggleItemRaycasts();
            _image.raycastTarget = true;
            if (!Moved)
            {
                ResetPosition();
                Moved = false;
            }
        }



        public void ResetPosition()
        {
            Debug.Log("Reset position.");
            Inventory.Instance.UndoTempRemoveItem();
            (transform as RectTransform).anchoredPosition = _originalPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            (transform as RectTransform).anchoredPosition += eventData.delta;
        }
    }
}