using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDialogueEvent : DialogueEvent
{
    protected override void InitializeEvents()
    {
        events = new List<Action>
        {
            EventForCondition0,
            EventForCondition1
        };
    }

    protected override int CheckConditions()
    {
        if (gM.InventoryItems.Count < 6)
            return 0;
        else
            return 1; 
    }

    private void EventForCondition0()
    {
        Debug.Log("No has traido las frutas!");
    }

    private void EventForCondition1()
    {
        Debug.Log("Ganaste! trajiste las frutas");
    }
}
