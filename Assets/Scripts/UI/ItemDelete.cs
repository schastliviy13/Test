using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDelete : MonoBehaviour
{
    [SerializeField] GameObject itemCanvas;
    [SerializeField] InventoryUI inventoryUI;
    public int indexItem;
    public void Start()
    {
        
    }
    public void DeleteItem()
    {
        inventoryUI._inventory.DeleteItem(indexItem);
        inventoryUI.DrawInventoryUI();
        CloseItemCanvas();
    }
    public void OpenItemCanvas()
    {
        if (inventoryUI._inventory.GetItemByIndex(indexItem)!=null)
        {
            itemCanvas.SetActive(true);
        }
        
    }
    public void CloseItemCanvas()
    {
        itemCanvas.SetActive(false);
    }
}
