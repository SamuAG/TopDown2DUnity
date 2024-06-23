using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public float damage;
    public float price;
    public Sprite icon;
    public Action action;

    public void PerformAction()
    {
        action?.Invoke();
    }
}
