using Cyborg.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Cyborg.Items
{
    public enum SlotType
    {
        Head,
        Arm,
        Leg,
    }

    public class EquipmentManager : MonoBehaviour
    {
        public enum Slot
        {
            None,
            Head,
            LeftArm,
            RightArm,
            LeftLeg,
            RightLeg,
        }

        private Weapon _weapon;
        private EquippableItem _head;
        private EquippableItem _leftArm;
        private EquippableItem _rightArm;
        private EquippableItem _leftLeg;
        private EquippableItem _rightLeg;
        private Dictionary<Slot, EquippableItem> _equipmentMap;

        public Weapon Weapon => _weapon;

        private void Awake()
        {
            _weapon = GetComponentInChildren<Weapon>();
            _equipmentMap = new Dictionary<Slot, EquippableItem>
            {
                { Slot.Head, _head },
                { Slot.LeftArm, _leftArm },
                { Slot.RightArm, _rightArm },
                { Slot.LeftLeg, _leftLeg },
                { Slot.RightLeg, _rightLeg },
            };

            //DontDestroyOnLoad(gameObject);
        }

        public void EquipWeapon(WeaponData data)
        {
            _weapon.Data = data;
            //Change the sprite
        }

        public bool TryUnequipItem(EquippableItem item)
        {
            var inventoryItem = item.GetComponent<InventoryItem>();
            Inventory.Instance.TryAddItem(inventoryItem);
            _equipmentMap[item.CurrentSlot] = null;
            return true;
        }

        public bool TryEquipItem(EquippableItem item, Slot slot)
        {
            if (!CheckFit(item.SlotType, slot)) return false;

            Inventory.Instance.RemoveItem(item.Id);
            item.CurrentSlot = slot;
            _equipmentMap[slot] = item;

            return true;
        }

        public bool TryMoveItem(EquippableItem item, Slot slot)
        {
            if (!CheckFit(item.SlotType, slot)) return false;
            _equipmentMap[item.CurrentSlot] = null;
            _equipmentMap[slot] = item;
            item.CurrentSlot = slot;

            return true;
        }

        private bool CheckFit(SlotType slotType, Slot slot)
        {
            return slotType switch
            {
                SlotType.Head => slot == Slot.Head,
                SlotType.Arm => slot == Slot.LeftArm || slot == Slot.RightArm,
                SlotType.Leg => slot == Slot.LeftLeg || slot == Slot.RightLeg,
                _ => false,
            };
        }
    }
}