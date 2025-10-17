using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool IsInInventory;
    public int InventoryIndex;
    Rigidbody rb;
    private float moveSpeed = 1f;
    private Vector3 lastImpulse;
    public string rarity;
    public string shape;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //stick to the center of the inventory slot, even if moved
        if(IsInInventory)
        {
            Vector3 positionChange = Vector3.zero - transform.localPosition;
            transform.localPosition += positionChange / moveSpeed;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointerDown");
    }

    //shape stops being phyics object while it is dragged
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("beginDrag");

        int ignoreRaycastLayerIndex = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.layer = ignoreRaycastLayerIndex;
        if(IsInInventory && InventoryIndex != null)
        {
            transform.parent.parent.gameObject.GetComponent<Inventory>().FreeSlot(InventoryIndex);
            if(transform.parent.parent.gameObject.GetComponent<InventoryLoader>() != null)
            {
                transform.parent.parent.gameObject.GetComponent<InventoryLoader>().StartSave();
            }
        }
        IsInInventory = false;
        rb.isKinematic = true;
    }

    //stick to mouse position while dragged
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Vector3 positionChange = hit.point - transform.position;
            transform.position += positionChange / moveSpeed;
            lastImpulse = positionChange;
        }
    }

    //this happens after ondrop, if dropped in pen will feel like the shape was thrown
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("endDrag");
        if (!IsInInventory) {
            Debug.Log("last impulse: " + lastImpulse);
            rb.isKinematic = false;
            rb.AddForce(lastImpulse * 30, ForceMode.Impulse);
        }

    }
}
