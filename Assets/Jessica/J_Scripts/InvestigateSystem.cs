using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvestigateSystem : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform investigatePosition;
    private bool isInteractable = false;
    private bool isHolding = false;
    GameObject heldItem;
    private Rigidbody objectRigidbody;

    

    public void Interact()
    {
        isInteractable = true;
        
    }

    public string GetInteractionText()
    {
        return "Click to Investigate";
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && isInteractable) //moue button held + item is interactable
        {
            if (!isHolding)
            {
                heldItem = this.gameObject;
                isHolding = true;
            }

        }
        if (isHolding)
        {
            //this.gameObject.GetComponent<Rigidbody>().MovePosition(investigatePosition.position);
            this.transform.position = investigatePosition.position;
            //not collide with anything.

            if (this.gameObject.GetComponent<CapsuleCollider>()) { this.gameObject.GetComponent<CapsuleCollider>().enabled = false; }
            if (this.gameObject.GetComponent<BoxCollider>()) { this.gameObject.GetComponent<BoxCollider>().enabled = false; }
            if (this.gameObject.GetComponent<SphereCollider>()) { this.gameObject.GetComponent<SphereCollider>().enabled = false; }


        }

        if (Input.GetMouseButtonUp(0))
        {
            heldItem = null;
            isInteractable = false;
            isHolding = false;

            if (this.gameObject.GetComponent<CapsuleCollider>()) { this.gameObject.GetComponent<CapsuleCollider>().enabled = true; }
            if (this.gameObject.GetComponent<BoxCollider>()) { this.gameObject.GetComponent<BoxCollider>().enabled = true; }
            if (this.gameObject.GetComponent<SphereCollider>()) { this.gameObject.GetComponent<SphereCollider>().enabled = true; }
        }
    }
  


}
