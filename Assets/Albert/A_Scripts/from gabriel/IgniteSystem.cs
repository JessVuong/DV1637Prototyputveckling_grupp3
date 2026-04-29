using UnityEngine;

public class IgniteSystem : MonoBehaviour, IInteractable
{
    public Inventory_System inventory;

    public void Interact()
    {
        Ignite();
    }

    public string GetInteractionText()
    {
        return "Click to Ignite";
    }

    private void Ignite()
    {
        Debug.Log("Fire!");
    }
}
