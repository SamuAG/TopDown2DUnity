using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OA_Dead : ObjectAction
{
    [SerializeField]
    private GameObject particles;
    [SerializeField]
    private GameManagerSO gM;

    protected override void Start()
    {
        base.Start();
    }

    public override void PerformAction()
    {
        gM.Player.gameObject.SetActive(false);
        gM.Inventory.HideShowInventory();
        gM.Inventory.enabled = false;
        KillAndRevive temp = Instantiate(particles, gM.Player.transform.position, Quaternion.identity).GetComponent<KillAndRevive>();
        
        temp.Kill(itemData);
    }
}

