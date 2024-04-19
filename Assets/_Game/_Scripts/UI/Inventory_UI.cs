using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{

    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject buttonItem;
    [SerializeField] Transform Content;
    [SerializeField] Player player;
     // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetInventory();
        }
    }

    public void SetInventory()
    {
        if(!Inventory.activeSelf)
        {
            Inventory.SetActive(true);
            ShowItem();
        }
        else
        {
            Inventory.SetActive(false);
            ClearItem();
        }
    }

    public void ShowItem()
    {
        //Player player = GameManager.Instance.player;
        for(int i =0; i< player.inventory.Items.Count; i++) 
        {
            ButtonItem button = Instantiate(buttonItem, Content).GetComponent<ButtonItem>();
            if (player.inventory.Items[i].type!= ItemType.none)
            {
                button.SetItem(player.inventory.Items[i]);
            }
            else
            {
                button.setEmpty();
            }
        }
    }
    public void ClearItem()
    {
        foreach(Transform button in Content)
        {
            Destroy(button.gameObject);
        }
    }
}
