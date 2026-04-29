using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour
{
    public HashSet<Inv_ItemType> collectedItems = new HashSet<Inv_ItemType>();

    public int Paper_Pieces = 0;

    public bool HasItem(Inv_ItemType item)
    {
        return collectedItems.Contains(item);
    }

    public void CollectItem(Inv_ItemType item)
    {
        collectedItems.Add(item);
    }

    public void RemoveItem(Inv_ItemType item)
    {
        collectedItems.Remove(item);
    }

    public void AddPaper_Pieces()
    {
        Paper_Pieces++;
    }
}
