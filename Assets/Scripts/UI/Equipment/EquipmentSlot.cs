using Cyborg.Items;
using Cyborg.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cyborg.UI
{
    public class EquipmentSlot : MonoBehaviour, IDropHandler
    {
        private EquipmentManager _equipmentManager;

        [SerializeField] private EquipmentManager.Slot _slotType;

        public bool Empty => transform.childCount == 0;

        private void Awake()
        {
            _equipmentManager = GetComponentInParent<EquipmentManager>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Dropped on equipment slot");


            if (Inventory.Instance.ItemToDrop != null && Empty)
            {
                var item = Inventory.Instance.ItemToDrop.GetComponent<EquippableItem>();
                if (_equipmentManager.TryEquipItem(item, _slotType))
                {
                    var itemTransform = Inventory.Instance.ItemToDrop.transform;
                    Inventory.Instance.ItemToDrop.Moved = true;
                    itemTransform.SetParent(transform);
                    (itemTransform as RectTransform).anchoredPosition = Vector2.zero;
                    transform.SetAsLastSibling();
                }
            }
        }

    }
}