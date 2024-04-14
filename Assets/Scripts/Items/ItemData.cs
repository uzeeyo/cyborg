using UnityEngine;

namespace Cyborg.Items
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private GridCoordinate _size;
        [SerializeField] private Sprite _sprite;

        public string ItemName => _itemName;
        public GridCoordinate Size => _size;
        public Sprite Sprite => _sprite;

    }
}