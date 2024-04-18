using UnityEngine;

namespace Cyborg.Items
{
    [CreateAssetMenu(fileName = "New EquipmentData", menuName = "ScriptableObjects/EquipmentData")]
    public class EquipmentData : ItemData
    {
        [SerializeField] private SlotType _slotType;

        public SlotType SlotType => _slotType;
    }
}