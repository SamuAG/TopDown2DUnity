using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectAction : MonoBehaviour
{
    // De esta manera solo funcionaria para objetos unicos o para cambiar lo que
    // hacia anteriormente el objeto ya que el performAction se aplica
    // a todos los objetos que tengan la misma instancia del ScriptableObject (logicamente)
    // lo voy a dejar asi por tiempo pero esto se podria hacer con un script que estuviera todo en el start
    
    protected ItemSO itemData;
    protected virtual void Start()
    {
        if (TryGetComponent(out PickUpItem pickUpItem))
        {
            itemData = pickUpItem.ItemData;
            itemData.action += PerformAction;
        }
        else
        {
            return;
        }
    }
    public abstract void PerformAction();
}
