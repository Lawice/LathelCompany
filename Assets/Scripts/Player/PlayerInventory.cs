using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;

public class PlayerInventory : NetworkBehaviour
{
    public int maxSlots = 5;
    public List<Item> items = new List<Item>();
    public int currentIndex = 0;

    public void AddItem(Item newItem)
    {
        if (items.Count < maxSlots)
        {
            items.Add(newItem);
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public Item GetCurrentItem()
    {
        if (items.Count == 0) return null;
        return items[currentIndex];
    }

    public void NextItem()
    {
        if (items.Count == 0) return;
        currentIndex = (currentIndex + 1) % items.Count;
    }

    public void PreviousItem()
    {
        if (items.Count == 0) return;
        currentIndex = (currentIndex - 1 + items.Count) % items.Count;
    }
}
