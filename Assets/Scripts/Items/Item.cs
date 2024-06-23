using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractive
{
    [SerializeField]
    protected int id;
    [SerializeField]
    private ItemSO itemData;
    [SerializeField]
    protected bool alreadyObtained;
    [SerializeField]
    protected GameManagerSO gM;

    private Dialogue dialogue;
    
    public ItemSO ItemData { get => itemData; set => itemData = value; }

    protected virtual void Start()
    {
        dialogue = GetComponent<Dialogue>();
        if (gM.Items.ContainsKey(id))
        {
            if (!gM.Items[id]) Destroy(gameObject);
        }
        else{
            gM.Items.Add(id, true);
        }
    }

    public virtual void Interact()
    {
        if (itemData)
        {
            if (gM.Inventory.DisplayedItems < gM.Inventory.InventoryButtons.Length)
            {
                if (!alreadyObtained)
                {
                    alreadyObtained = true;
                    gM.Inventory.NewItem(itemData);
                    gM.InventoryItems.Add(itemData);
                    gM.Items[id] = false;
                }
                if (dialogue)
                {
                    dialogue.Sentences = new string[1];
                    dialogue.Sentences[0] = "You just obtained a new " + itemData.itemName + "!";
                    dialogue.DestroyItem = true;
                    dialogue.StartDialogue();
                    
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (dialogue)
                {
                    dialogue.Sentences = new string[1];
                    dialogue.Sentences[0] = "Inventory is full!";
                    dialogue.StartDialogue();
                }
                return;
            }
        }
        else
        {
            gM.Items[id] = false;
        }
    }

}
