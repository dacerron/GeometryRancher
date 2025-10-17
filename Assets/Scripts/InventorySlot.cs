using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int InventoryIndex;
    private bool isOccupied = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //shape behaves differently while in inventory, can be clicked again, isn't physics object
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop in inventory");
        if(eventData.pointerDrag != null)
        {
            GameObject item = eventData.pointerDrag;
            //if slot occupied, fail to put in inventory, let shape fall
            if (transform.parent.GetComponent<Inventory>().CheckSlotIsOccupied(InventoryIndex))
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<Shape>().IsInInventory = false;
                item.GetComponent<Shape>().InventoryIndex = -1;
            } else
            {
                
                item.transform.parent = this.transform;
                item.transform.localPosition = Vector3.zero;
                item.GetComponent<Shape>().IsInInventory = true;
                item.GetComponent<Shape>().InventoryIndex = InventoryIndex;
                transform.parent.GetComponent<Inventory>().OccupySlot(InventoryIndex);
                if(transform.parent.GetComponent<InventoryLoader>() != null)
                {
                    transform.parent.GetComponent<InventoryLoader>().StartSave();
                }
            }
            int ignoreRaycastLayerIndex = LayerMask.NameToLayer("Default");
            eventData.pointerDrag.layer = ignoreRaycastLayerIndex;
        }
    }
}
