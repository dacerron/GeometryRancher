using UnityEngine;
using UnityEngine.EventSystems;

public class Pen : MonoBehaviour, IDropHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when dropped in pen, should shape should behave like a physics object
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop in pen");
        if(eventData.pointerDrag != null)
        {
            GameObject item = eventData.pointerDrag;
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Shape>().IsInInventory = false;
            item.GetComponent<Shape>().InventoryIndex = -1;
            int ignoreRaycastLayerIndex = LayerMask.NameToLayer("Default");
            item.layer = ignoreRaycastLayerIndex;
            item.transform.parent = this.transform;
        }

    }
}
