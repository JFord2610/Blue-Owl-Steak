using UnityEngine;
using System.Collections;

public static class EventManager
{
    public delegate void EventHandler();
    public static EventHandler PlayerDroppedItemEvent;
    public static EventHandler GameWinEvent;

    public static void InvokePlayerDroppedItem() { PlayerDroppedItemEvent?.Invoke(); }
    public static void InvokeGameWinEvent() { GameWinEvent?.Invoke(); }
}
