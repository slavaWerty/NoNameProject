using System;
using UnityEngine;

public abstract class InteractSignal : MonoBehaviour
{
    public Action<object> Interact;

    public void OnMouseDown()
    {
        Interaction();
        Debug.Log("Mouse Klick");
    }

    protected virtual void Interaction()
    {
        Interact?.Invoke(gameObject.name);
    }
}

