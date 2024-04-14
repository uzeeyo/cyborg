using Cyborg.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cyborg.Items
{
    public class TestDroppableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        private Image _image;
        private Vector2 _originalPosition;
        private bool _rotated;
        private Coroutine _listenForRotateCoroutine;

        public Item Item { get; private set; }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            Item = GetComponent<Item>();
        }

        public void OnBeginDrag(PointerEventData _)
        {
            _originalPosition = _rectTransform.anchoredPosition;
            _listenForRotateCoroutine = StartCoroutine(ListenForRotate());
            var inventory = FindObjectOfType<Inventory>();
            inventory.ToggleItemRaycasts();
            _image.raycastTarget = false;

            if (Item.InInventory)
            {
                inventory.TempRemoveItem(Item.Id);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData _)
        {
            StopCoroutine(_listenForRotateCoroutine);
            FindObjectOfType<Inventory>().ToggleItemRaycasts();
            _image.raycastTarget = true;
        }

        private IEnumerator ListenForRotate()
        {
            while (true)
            {
                //TODO: Get input key elsewhere. Should be customizable
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _rotated = !_rotated;
                    var rotationZ = _rotated ? -90 : 90;
                    _rectTransform.Rotate(new Vector3(0, 0, rotationZ));
                    Item.Rotate();
                }

                yield return null;
            }
        }

        public void ResetPosition()
        {
            FindObjectOfType<Inventory>().UndoTempRemoveItem();
            _rectTransform.anchoredPosition = _originalPosition;
        }
    }
}