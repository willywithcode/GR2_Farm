using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "ScriptableObjects/InventorySO", order = 1)]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;
    public new Action<Dictionary<int, InventoryItem>> OnInventoryUpdate;
    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for(int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public int AddItem(ItemSO item, int quantity)
    {
        if(item.IsStackable == false)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                while(quantity > 0 && IsInventoryFull() == false) 
                {
                    quantity -= AddItemToFirstFreeSlot(item, 1);
                }
                ChangeInfor();
                return quantity;
            }
        }
        quantity = AddSatckableItem(item, quantity);
        ChangeInfor();
        return quantity;
    }

    private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            quantity = quantity,
        };
        for(int i = 0;i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = newItem;
                return quantity;
            }
        }
        return 0;
    }

    private int AddSatckableItem(ItemSO item, int quantity)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            if (inventoryItems[i].item.ID == item.ID)
            {
                int amountPossibleToTake =
                    inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                if (quantity > amountPossibleToTake)
                {

                    inventoryItems[i] = inventoryItems[i]
                        .ChangeQuantityItem(inventoryItems[i].item.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else
                {
                    inventoryItems[i] = inventoryItems[i]
                        .ChangeQuantityItem(inventoryItems[i].quantity + quantity);
                    ChangeInfor();
                    return 0;
                }
            }
        }
        while (quantity > 0 && IsInventoryFull() == false)
        {
            int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity);
        }
        return quantity;
    }



    private bool IsInventoryFull()
    => inventoryItems.Where(item => item.IsEmpty).Any() == false;

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue =
            new Dictionary<int, InventoryItem>();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    internal InventoryItem GetItemAtIndex(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    internal void SwapItem(int index1, int index2)
    {
        //Debug.Log(index1 + "" + index2);
        InventoryItem item1 = inventoryItems[index1];
        inventoryItems[index1] = inventoryItems[index2];
        inventoryItems[index2] = item1;
        ChangeInfor();
    }

    private void ChangeInfor()
    {
        OnInventoryUpdate?.Invoke(GetCurrentInventoryState());
    }

    internal void RemoveItem(int itemIndex, int amount)
    {
        if (inventoryItems.Count > itemIndex)
        {
            if (inventoryItems[itemIndex].IsEmpty)
                return;
            int reminder = inventoryItems[itemIndex].quantity - amount;
            if (reminder <= 0)
                inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
            else
                inventoryItems[itemIndex] = inventoryItems[itemIndex]
                    .ChangeQuantityItem(reminder);

            ChangeInfor();
        }
    }
}


[Serializable]
public struct InventoryItem
{
    public int quantity;
   public ItemSO item;

    public bool IsEmpty => item == null;

    public InventoryItem ChangeQuantityItem(int newQuantity)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuantity,
        };
    }
    public static InventoryItem GetEmptyItem()
        => new InventoryItem
        {
            item = null,
            quantity = 0,
        };


}