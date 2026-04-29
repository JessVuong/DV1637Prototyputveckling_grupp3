using UnityEngine;

public class IgniteSystem : MonoBehaviour, IInteractable
{
    public Inventory_System inventory;

    public void Interact()
    {
        if (inventory.HasItem(Inv_ItemType.Torch))
            Ignite();
    }

    public string GetInteractionText()
    {
        return "Click to Ignite";
    }

    private void Ignite()
    {
        Destroy(gameObject);
    }
}
