using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    protected Dictionary<int, bool> occupiedSlots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        occupiedSlots = new Dictionary<int, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckSlotIsOccupied(int slot)
    {
        if(occupiedSlots.ContainsKey(slot))
        {
            return occupiedSlots[slot];
        } else
        {
            return false;
        }
    }

    public void FreeSlot(int slot)
    {
        if(occupiedSlots.ContainsKey(slot))
        {
            occupiedSlots[slot] = false;
        }
    }

    public void OccupySlot(int slot)
    {
        if(occupiedSlots.ContainsKey(slot))
        {
            occupiedSlots[slot] = true;
        }
    }

    public void InitialiseInventory(InventoryData inventoryData)
    {
        if(occupiedSlots == null)
        {
            occupiedSlots = new Dictionary<int, bool>();
        }
        if(inventoryData != null)
        {
            int slotNumber = 0;
            //create slots with shapes in them
            foreach(InventoryItem item in inventoryData.items)
            {
                GameObject slot = Instantiate(Resources.Load("Prefabs/InventorySlot") as GameObject, this.transform);
                occupiedSlots.Add(slotNumber, true);
                slot.GetComponent<InventorySlot>().InventoryIndex = slotNumber;
                GenerateShape(item, slot.transform, slotNumber);
                slotNumber++;
            }
            //create the rest of the empty slots
            for(int i = 0; i < (inventoryData.size - inventoryData.items.Count); i++)
            {
                GameObject slot = Instantiate(Resources.Load("Prefabs/InventorySlot") as GameObject, this.transform);
                occupiedSlots.Add(slotNumber, false);
                slot.GetComponent<InventorySlot>().InventoryIndex = slotNumber;
                slotNumber++;
            }
        }
        Resources.UnloadUnusedAssets();
    }

   

    public GameObject GenerateShape(InventoryItem item, Transform parent, int slot)
    {
        //create the correct shape for the inventory item
        GameObject shape = null;
        switch (item.shape)
        {
            case "Pyramid":
                shape = Instantiate(Resources.Load("Prefabs/PyramidShape") as GameObject, parent, true);
                break;
            case "Cube":
                shape = Instantiate(Resources.Load("Prefabs/CubeShape") as GameObject, parent, true);
                break;
            case "Dod":
                shape = Instantiate(Resources.Load("Prefabs/DodecaShape") as GameObject, parent, true);
                break;
            case "Ico":
                shape = Instantiate(Resources.Load("Prefabs/IcosaShape") as GameObject, parent, true);
                break;
            case "Sphere":
                shape = Instantiate(Resources.Load("Prefabs/SphereShape") as GameObject, parent, true);
                break;
        }

        //set up shape to be in inventory slot
        if (shape != null)
        {
            shape.transform.position = Vector3.zero;
            shape.GetComponent<Shape>().IsInInventory = true;
            shape.GetComponent<Shape>().InventoryIndex = slot;
            shape.GetComponent<Shape>().rarity = item.rarity;
            shape.GetComponent<Shape>().shape = item.shape;
            shape.GetComponent<Rigidbody>().isKinematic = true;
            shape.GetComponent<Rigidbody>().useGravity = false;
        }

        //assign material based on inventory item
        Material rarityMaterial = null;
        switch (item.rarity)
        {
            case "Common":
                rarityMaterial = Resources.Load("Materials/Common") as Material;
                break;
            case "Uncommon":
                rarityMaterial = Resources.Load("Materials/Uncommon") as Material;
                break;
            case "Magic":
                rarityMaterial = Resources.Load("Materials/Magic") as Material;
                break;
            case "Rare":
                rarityMaterial = Resources.Load("Materials/Rare") as Material;
                break;
            case "Legendary":
                rarityMaterial = Resources.Load("Materials/Legendary") as Material;
                break;
        }
        if (rarityMaterial != null)
        {
            Material[] materials = shape.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
            materials[0] = rarityMaterial;
            shape.transform.GetChild(0).GetComponent<MeshRenderer>().materials = materials;
        }

        return shape;
    }
}
