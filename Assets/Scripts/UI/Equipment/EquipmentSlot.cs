using Cyborg.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cyborg.UI
{
    public class EquipmentSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private ItemWindow _itemWindow;
        private EquipmentManager _equipmentManager;
        private Image _highlighter;
        private Color _originalColor;

        [SerializeField] private EquipmentManager.Slot _slotType;

        public bool Empty => transform.childCount == 0;

        private void Awake()
        {
            _equipmentManager = GetComponentInParent<EquipmentManager>();
            _itemWindow = GetComponentInParent<ItemWindow>();
            _highlighter = GetComponent<Image>();
            _originalColor = _highlighter.color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_itemWindow.ItemToDrop != null && Empty)
            {
                Drop();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_itemWindow.ItemToDrop != null && Empty)
            {
                _highlighter.color = Color.black * 0.5f;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _highlighter.color = _originalColor;
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Dropped on equipment slot");

            if (_itemWindow.ItemToDrop != null && Empty)
            {
                Drop();
            }
        }

        private void Drop()
        {
            var equippableItem = _itemWindow.ItemToDrop.GetComponent<EquippableItem>();
            if (_equipmentManager.TryEquipItem(equippableItem, _slotType))
            {
                var itemTransform = _itemWindow.ItemToDrop.transform;
                _itemWindow.ItemToDrop.Moved = true;
                itemTransform.SetParent(transform);
                (itemTransform as RectTransform).anchoredPosition = Vector2.zero;
                transform.SetAsLastSibling();
            }
        }

    }
}