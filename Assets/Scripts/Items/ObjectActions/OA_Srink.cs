using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OA_Srink : ObjectAction
{
    [SerializeField]
    private GameManagerSO gM;

    protected override void Start()
    {
        base.Start();
    }

    public override void PerformAction()
    {
        gM.Player.Graphics.transform.localScale *= 0.5f;
        gM.InitPlayerScale = gM.Player.Graphics.transform.localScale;
        // remove the item from the inventory
        gM.InventoryItems.Remove(itemData);
        gM.Inventory.ResetInventory(gM.InventoryItems);
    }
}