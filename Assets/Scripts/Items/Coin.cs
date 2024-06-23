using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    protected override void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        gM.Coins++;
        Destroy(gameObject);
        Debug.Log("Coins" + gM.Coins);
        base.Interact();
    }
}
