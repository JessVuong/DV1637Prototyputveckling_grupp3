using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvestigateSystem : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform investigatePosition;
    private Rigidbody objectRigidbody;

    

    public void Interact()
    {
        //when click ineract

        isInteractable = true;
    }

    public string GetInteractionText()
    {
        return "Click to Investigate";
    }

    
    void Update()
    {
   
        if (Input.GetMouseButton(0) && isinteractable) //moue button held + item is interactable
        {
            this.investigatePosition = investigatePosition;

            Debug.Log("Pressed left hold");

            if (investigatePosition != null)
            {
                objectRigidbody.MovePosition(investigatePosition.position);
            }

        }
    }

  


}
