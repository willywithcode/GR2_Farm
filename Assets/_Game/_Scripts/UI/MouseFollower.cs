using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private InventoryItemUI inventoryItemUI;
    private RectTransform rectTransform;
    private void Awake()
    {
        //canvas = transform.root.GetComponent<Canvas>();
        inventoryItemUI = GetComponentInChildren<InventoryItemUI>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetData(Sprite image, int quantity)
    {
        inventoryItemUI.SetData(image, quantity);
    }
    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position
            );
        transform.position = canvas.transform.TransformPoint(position);
        //rectTransform.anchoredPosition = Input.mousePosition - new Vector3(canvas.pixelRect.width / 2, canvas.pixelRect.height / 2);
    }

    public void Toggle(bool val)
    {
        //if (val)
        //{
        //    Update();
        //}
        Debug.Log(val);
        gameObject.SetActive(val);
    }
}
