using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour
{
    public string[,] inventory = {  
                                    {"Key_cell"     ,   "0" },
                                    {"Paper_pieces" ,   "0" },
                                    {"Key_armory"   ,   "0" },
                                    {"Hammer"       ,   "0" },
                                    {"Gunpowder"    ,   "0" },
                                    {"Connonball"   ,   "0" },
                                    {"Fuse"         ,   "0" }
                                 };

    public string[,] tools =     {  
                                    {"Torch"        ,   "0"},
                                    {"Rope"         ,   "0"}
                                 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
