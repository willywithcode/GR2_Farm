using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
    IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] private Image Border;
    [SerializeField] private Image imageItem;
    [SerializeField] private TextMeshProUGUI textQuantity;

    public event Action<InventoryItemUI> OnItemClicked,
        OnDropItem, OnItemBeginDrag, OnItemEndDrag, OnRightMouseClick;

    private bool empty = true;

    private void Awake()
    {
        ResetData();
        DeSelect();
    }
    public void ResetData()
    {
        if(imageItem!= null)
        {
            this.imageItem.gameObject.SetActive(false);

        }
        empty = true;
    }

    public void DeSelect()
    {
        if (Border != null)
        {
            Border.enabled = false;
        }
    }

    public void Select()
    {
        if(Border != null)
        {
            Border.enabled = true;
        }
    }

    public void SetData(Sprite sprite, int quantity)
    {
        //Debug.Log("SetData");
        imageItem.gameObject.SetActive(true);
        imageItem.sprite = sprite;
        textQuantity.text = quantity.ToString();
        empty = false;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropItem?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
