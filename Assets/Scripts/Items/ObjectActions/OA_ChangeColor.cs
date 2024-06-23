using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OA_ChangeColor : ObjectAction
{
    private Color newColor;
    [SerializeField]
    private GameManagerSO gM;

    protected override void Start()
    {
        base.Start();
    }

    public override void PerformAction()
    {
        float playerColorA = gM.Player.Graphics.GetComponent<SpriteRenderer>().color.a;
        newColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, playerColorA, playerColorA);
        gM.Player.Graphics.GetComponent<SpriteRenderer>().color = newColor;
        gM.InitPlayerColor = gM.Player.Graphics.GetComponent<SpriteRenderer>().color;
        // remove the item from the inventory
        gM.InventoryItems.Remove(itemData);
        gM.Inventory.ResetInventory(gM.InventoryItems);
    }
}
