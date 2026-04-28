using UnityEngine;

public class Pickup_System : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PickUp();
    }

    public string GetInteractionText()
    {
        return "Click to Pick up";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PickUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Oh! Shiny!");
        }

    }
}
