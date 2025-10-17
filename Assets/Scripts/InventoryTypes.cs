using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InventoryItem
{
    public string rarity;
    // possible shapes: "Pyramid", "Cube", "Ico", Dod", "Sphere"
    public string shape;
}

[System.Serializable]
public class InventoryData
{
    public int size;
    public List<InventoryItem> items;
}
