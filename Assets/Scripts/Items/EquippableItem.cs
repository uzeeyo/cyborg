using System;
using UnityEngine;

namespace Cyborg.Items
{
    public abstract class EquippableItem : MonoBehaviour
    {
        public Guid Id => GetComponent<InventoryItem>().Id;
        public EquipmentManager.Slot CurrentSlot { get; set; }
        public abstract SlotType SlotType { get; }

        public abstract void Init(EquipmentData data);
    }
}