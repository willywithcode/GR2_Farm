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
        public Sprite Icon;

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

        public void Add(Collectable item)
        {
            this.type = item.type;
            this.Icon = item.Icon;
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

    public void AddItem(Collectable newItem)
    {
        foreach (Item item in Items)
        {

            if (item.type == newItem.type && item.CanAdd())
            {
                item.Add(newItem);
                return;
            }
        }
        foreach (Item item in Items)
        {
            if (item.type == ItemType.none)
            {
                item.Add(newItem);
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
