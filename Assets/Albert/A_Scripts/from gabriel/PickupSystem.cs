using UnityEngine;

public class PickupSystem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PickUp();
    }

    public string GetInteractionText()
    {
        return "Click to Pick up";
    }

    private void PickUp()
    {
        Debug.Log("Oh! Shiny!");
    }
}
