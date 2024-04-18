using UnityEngine;

namespace Cyborg.Items
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private string _description;
        [SerializeField] private GridCoordinate _size;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private float _energyCost;

        public string ItemName => _itemName;
        public string Description => _description;
        public GridCoordinate Size => _size;
        public Sprite Sprite => _sprite;
        public ItemType ItemType => _itemType;
        public float EnergyCost => _energyCost;
    }
}