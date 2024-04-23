using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField] private Image ImageItem;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Description;

    
    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.ImageItem.enabled = false;
        this.Title.text = "";
        this.Description.text = "";
    }

    public void SetDescription(Sprite ImageItem, string Title, string Description)
    {
        this.ImageItem.enabled = true;
        this.ImageItem.sprite = ImageItem;      
        this.Title.text = Title;
        this.Description.text = Description;
    }
}
