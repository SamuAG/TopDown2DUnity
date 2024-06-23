using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject inventoryText;
    [SerializeField]
    private Button[] inventoryButtons;

    private int displayedItems = 0;

    public int DisplayedItems { get => displayedItems; }
    public Button[] InventoryButtons { get => inventoryButtons; set => inventoryButtons = value; }

    void Start()
    {
        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            int buttonIndex = i;
            inventoryButtons[i].onClick.AddListener(() =>
            {
                ClickedButton(buttonIndex);
            });
        }
        inventoryPanel.SetActive(false);
        inventoryText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            HideShowInventory();
        }
    }

    public void HideShowInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        inventoryText.SetActive(!inventoryText.activeSelf);
    }

    private void ClickedButton(int i)
    {
        Debug.Log("button: " + i);
    }

    public void NewItem(ItemSO itemData)
    {
        Button button = inventoryButtons[displayedItems];
        button.gameObject.SetActive(true);
        button.GetComponent<Image>().sprite = itemData.icon;

        button.onClick.RemoveAllListeners();
        if (itemData.action != null)
        {
            button.onClick.AddListener(() => itemData.PerformAction());
        }
        else
        {
            button.onClick.AddListener(() => Debug.Log("No action assigned to this item"));
        }
        displayedItems++;
    }
    
    public void ResetInventory(List<ItemSO> InventoryItems)
    {
        displayedItems = 0;
        foreach (Button button in inventoryButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (ItemSO item in InventoryItems)
        {
            NewItem(item);
        }
    }
}
