using Cyborg.Items;
using System;

namespace Cyborg.Player
{
    public struct InventoryItem
    {
        public InventoryItem(ItemData data)
        {
            Id = Guid.NewGuid();
            Data = data;
        }

        public Guid Id;
        public ItemData Data;
    }
}