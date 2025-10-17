using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class InventoryLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string filePath = Application.dataPath + "/../inventory.json";

    void Start()
    {
        if(Application.platform == RuntimePlatform.OSXPlayer)
        {
            filePath = Application.dataPath + "/../../inventory.json";
        } else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            filePath = Application.dataPath + "/../inventory.json";
        }
        LoadInventory();
    }

    public void LoadInventory()
    {
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            InventoryData loadedData = JsonUtility.FromJson<InventoryData>(jsonString);

            foreach(InventoryItem item in loadedData.items)
            {
                Debug.Log($"found item: {item.shape}, {item.rarity}");
            }
            GetComponent<Inventory>().InitialiseInventory(loadedData);
        }
        else
        {
            Debug.LogError("Inventory JSON file not found");
        }
    }

    public void OnApplicationQuit()
    {
        StartSave();
    }

    public void StartSave()
    {
        List<InventoryItem> items = new List<InventoryItem>();
        foreach (InventorySlot slot in GetComponentsInChildren<InventorySlot>())
        {
            if (slot.transform.childCount > 0)
            {
                Shape shape = slot.transform.GetChild(0).GetComponent<Shape>();
                items.Add(new InventoryItem { rarity = shape.rarity, shape = shape.shape });
            }
        }
        InventoryData data = new InventoryData { size = GetComponentsInChildren<InventorySlot>().Length, items = items };
        SaveInventory(data);

    }

    

    public void SaveInventory(InventoryData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }
}
