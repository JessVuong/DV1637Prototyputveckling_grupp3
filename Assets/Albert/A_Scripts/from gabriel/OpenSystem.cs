using UnityEngine;

public class OpenSystem : MonoBehaviour, IInteractable
{
    public Animator animator;
    public Inventory_System inventory;

    public bool opened = false;

    public void Interact()
    {
        if (!opened && inventory.HasItem(Inv_ItemType.Key))
        {
            Open();
            // PUT BACK AFTER DEMO inventory.RemoveItem(Inv_ItemType.Key);
        }
    }

    public string GetInteractionText()
    {
        return "Click to Open";
    }

    // Changes the animation state in the animator
    public void Open()
    {
        animator.SetBool("IsOpen", true);
        opened = true;
    }
}
