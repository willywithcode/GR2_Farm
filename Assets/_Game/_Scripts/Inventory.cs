using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    [System.Serializable]
    public class Item
    {
        public ItemType type;
        public int count;
        public int maxCount;

        public Item()
        {
            type = ItemType.none;
            count = 0;
            maxCount = 3;
        }

        public bool CanAdd()
        {
            if (count < maxCount)
            {
                return true;
            }
            return false;
        }

        public void Add(ItemType type)
        {
            this.type = type;
            count++;
        }
    }

    public List<Item> Items = new List<Item>();
    public Inventory(int CountSlot)
    {
        for (int i = 0; i < CountSlot; i++)
        {
            Item item = new Item();
            Items.Add(item);
        }
    }

    public void AddItem(ItemType itemType)
    {
        foreach (Item item in Items)
        {

            if (item.type == itemType && item.CanAdd())
            {
                item.Add(itemType);
                return;
            }
        }
        foreach (Item item1 in Items)
        {
            if (item1.type == ItemType.none)
            {
                item1.Add(itemType);
                return;
            }
        }
    }

}
public enum ItemType
{
    none = 0,
    seed = 1
}
