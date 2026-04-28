using UnityEngine;

public class IgniteSystem : MonoBehaviour, IInteractable
{
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
