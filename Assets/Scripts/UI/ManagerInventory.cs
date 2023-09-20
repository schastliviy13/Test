using System;
using UnityEngine;

public class ManagerInventory : MonoBehaviour
{
    public static ManagerInventory Instance { get; private set; }

    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject UIPanel;

    public event EventHandler OnInventoryCloseAction;
    public void Awake()
    {
        Instance = this;
    }
    public void OpenInventory()
    {
        InventoryPanel.SetActive(true);
        UIPanel.SetActive(false);
    }
    public void CloseInventory()
    {
        InventoryPanel.SetActive(false);
        UIPanel.SetActive(true);
        OnInventoryCloseAction?.Invoke(this, EventArgs.Empty);
    }
}
