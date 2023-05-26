using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents : MonoBehaviour
{
    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler onItemPickedUp;
    public static event ItemEventHandler onItemDropped;

    public static void PickUpItem(Item item)
    {
        if (onItemPickedUp != null)
        {
            onItemPickedUp(item);
        }
    }
    public static void DropItem(Item item)
    {
        if (onItemPickedUp != null)
        {
            onItemDropped(item);
        }
    }
}
