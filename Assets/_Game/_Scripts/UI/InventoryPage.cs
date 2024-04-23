using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItemUI ItemPrefabs;
    [SerializeField] private RectTransform ContentPanel;
    [SerializeField] private InventoryDescription inventoryDescription;

   public  List<InventoryItemUI> ListItems = new List<InventoryItemUI>();
    [SerializeField] private MouseFollower mouseFollower;

    public new Action<int> OnDescriptionRequested,
        OnItemActionRequested,
        OnStartDragging;

    public new Action<int, int> OnSwapItems;


    private int currentDragItemIndex = -1;
    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        inventoryDescription.ResetDescription();
    }
    public void InitInventoryItemUI(int inventorySize)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            InventoryItemUI item = Instantiate(ItemPrefabs, Vector3.zero, Quaternion.identity); 
            item.transform.SetParent(ContentPanel,false);
            ListItems.Add(item);
            item.OnItemClicked += HandleOnSelect;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnDropItem += HandleOnSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseClick += HandleShowItemAction;
        }
    }

    public void UpdateData(int itemindex, Sprite spriteItem, int quantity)
    {
        if (ListItems.Count > itemindex)
        {
            ListItems[itemindex].SetData(spriteItem, quantity);
        }
    }
    public void HandleOnSelect(InventoryItemUI item)
    {
        int index = ListItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);
    }
    public void HandleBeginDrag(InventoryItemUI item)
    {

        int index = ListItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        currentDragItemIndex = index;
        HandleOnSelect(item);
        OnStartDragging?.Invoke(index);
    }
    public void CreateDragItem(Sprite imageItem, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(imageItem, quantity);
    }
    public void HandleOnSwap(InventoryItemUI item)
    {
        int index = ListItems.IndexOf(item);
        if (currentDragItemIndex == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentDragItemIndex, index);
        HandleOnSelect(item);
    }
    public void HandleEndDrag(InventoryItemUI item)
    {
        ResetDragItem();
    }
    public void HandleShowItemAction(InventoryItemUI item)
    {
        int index = ListItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        inventoryDescription.ResetDescription();
        ResetSelected();
        DeSelectAllItem();

    }
    public void ResetSelected()
    {
        inventoryDescription.ResetDescription();
    }
    public void DeSelectAllItem()
    {
        foreach(InventoryItemUI item in ListItems)
        {
            item.DeSelect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDragItem() ;
    }

    public void ResetDragItem()
    {
        mouseFollower.Toggle(false);
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        inventoryDescription.SetDescription(itemImage, name, description);
        DeSelectAllItem();
        ListItems[itemIndex].Select();
    }

    internal void ResetAllItem()
    {
        foreach(var item in ListItems)
        {
            item.ResetData();
            item.DeSelect();
        }
    }
}
