using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class InventoryController : Singleton<InventoryController>
    {
    [SerializeField]
    private InventoryPage inventoryUIPage;

        [SerializeField] 
        private RectTransform ContentUIItem;

    [SerializeField]
    private InventorySO inventoryData;
    //public InventorySO inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdate += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item.item, item.quantity);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUIPage.ResetAllItem();
            foreach (var item in inventoryState)
            {
                inventoryUIPage.UpdateData(item.Key, item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUIPage.InitInventoryItemUI(inventoryData.Size);
            inventoryUIPage.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUIPage.OnSwapItems += HandleSwapItems;
            inventoryUIPage.OnStartDragging += HandleDragging;
            inventoryUIPage.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAtIndex(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject);
            }
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAtIndex(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUIPage.CreateDragItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItem(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAtIndex(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUIPage.ResetSelected();
                return;
            }
            ItemSO item = inventoryItem.item;
            inventoryUIPage.UpdateDescription(itemIndex, item.ItemImage,
                item.name, item.Description);
        }

        public void Update()
        {
        
        if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (inventoryUIPage.isActiveAndEnabled == false)
                {
                    inventoryUIPage.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUIPage.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUIPage.Hide();
                }

            }
        }
    }
