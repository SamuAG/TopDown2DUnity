using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OA_ChangeSpeed : ObjectAction
{
    [SerializeField]
    private float newSpeed = 0.5f;
    [SerializeField]
    private GameManagerSO gM;

    protected override void Start()
    {
        base.Start();
    }

    public override void PerformAction()
    {
        gM.Player.Speed = newSpeed;
        gM.Player.Speed = newSpeed;

        // remove the item from the inventory
        gM.InventoryItems.Remove(itemData);
        gM.Inventory.ResetInventory(gM.InventoryItems);
    }
}

