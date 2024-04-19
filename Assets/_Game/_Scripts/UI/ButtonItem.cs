using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItem : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI quantity;

    public void SetItem(Inventory.Item item)
    {
        if(item != null)
        {
            Icon.sprite = item.Icon;
            quantity.text = item.count.ToString();
        }
    }

    public void setEmpty()
    {
        Icon.sprite = null;
        quantity.text = string.Empty;
    }
}
