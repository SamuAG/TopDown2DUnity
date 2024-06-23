using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OA_Invisibility : ObjectAction
{
    private Color playerColor;
    private Color newColor;
    [SerializeField]
    private GameManagerSO gM;

    protected override void Start()
    {
        base.Start();
    }

    public override void PerformAction()
    {
        playerColor = gM.Player.Graphics.GetComponent<SpriteRenderer>().color;
        newColor = new Color(playerColor.r, playerColor.g, playerColor.b, 0.2f);
        gM.Player.Graphics.GetComponent<SpriteRenderer>().color = newColor;
        gM.InitPlayerColor = gM.Player.Graphics.GetComponent<SpriteRenderer>().color;

        // remove the item from the inventory
        gM.InventoryItems.Remove(itemData);
        gM.Inventory.ResetInventory(gM.InventoryItems);
    }
}

