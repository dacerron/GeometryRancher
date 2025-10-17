using UnityEngine;

public class DeleteShape : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collision enter");
        if (collider.gameObject.GetComponent<Shape>() != null)
        {
            Debug.Log("destroying:" + collider.gameObject.name);  
            Destroy(collider.gameObject);
        }
    }
}
