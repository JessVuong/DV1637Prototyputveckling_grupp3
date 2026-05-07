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
                this.transform.position = investigatePosition.position;
            }

        if (Input.GetMouseButtonUp(0))
        {
            heldItem = null;
            isInteractable = false;
            isHolding = false;
        }
    }

  


}
