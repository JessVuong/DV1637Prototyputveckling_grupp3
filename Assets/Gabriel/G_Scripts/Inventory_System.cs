using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour
{
    public HashSet<Inv_ItemType> collectedItems = new HashSet<Inv_ItemType>();

    public int Paper_Pieces = 0;
    public int maxPaper_Pieces = 5;

    public bool HasItem(Inv_ItemType item)
    {
        return collectedItems.Contains(item);
    }

    public void CollectItem(Inv_ItemType item)
    {
        collectedItems.Add(item);
    }

    public void AddPaper_Pieces()
    {
        Paper_Pieces++;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
