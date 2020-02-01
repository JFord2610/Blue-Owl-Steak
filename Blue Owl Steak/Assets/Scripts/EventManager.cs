using UnityEngine;
using System.Collections;

public static class EventManager
{
    public delegate void EventHandler();
    public static EventHandler PlayerDroppedItemEvent;

    public static void InvokePlayerDroppedItem()
    {
        PlayerDroppedItemEvent?.Invoke();
    }
}
