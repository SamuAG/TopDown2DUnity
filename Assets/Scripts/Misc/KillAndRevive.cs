using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAndRevive : MonoBehaviour
{
    [SerializeField]
    private GameManagerSO gM;

    public void Kill(ItemSO itemData)
    {
        StartCoroutine(Revive(itemData));
    }

    IEnumerator Revive(ItemSO itemData)
    {
        yield return new WaitForSeconds(2);
        gM.Inventory.enabled = true;
        gM.Player.gameObject.SetActive(true);
        // remove the item from the inventory
        gM.InventoryItems.Remove(itemData);
        gM.Inventory.ResetInventory(gM.InventoryItems);
        Destroy(gameObject);
    }
}
