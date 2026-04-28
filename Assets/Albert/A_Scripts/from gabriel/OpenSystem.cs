using UnityEngine;

public class OpenSystem : MonoBehaviour, IInteractable
{
    public Animator animator;

    public bool opened = false;

    public void Interact()
    {
        Open();
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
    }
}
