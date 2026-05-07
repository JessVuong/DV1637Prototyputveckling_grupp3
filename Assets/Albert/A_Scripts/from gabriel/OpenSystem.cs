using UnityEngine;

public class OpenSystem : MonoBehaviour, IInteractable
{
    public Animator animator;
    public Inventory_System inventory;
    public SoundManager soundManager;

    public bool opened = false;

    public void Interact()
    {
            
        if (!opened && inventory.HasItem(Inv_ItemType.Key))
        {
            Open();
            inventory.RemoveItem(Inv_ItemType.Key);
            //audio clip open door
            
        }

    }

    public string GetInteractionText()
    {
        return "Click to Open";
    }

    // Changes the animation state in the animator
    private void Open()
    {
        animator.SetBool("IsOpen", true);
        opened = true;
        SoundManager.PlaySound(SoundType.UnlockDoor);
    }

    
}
