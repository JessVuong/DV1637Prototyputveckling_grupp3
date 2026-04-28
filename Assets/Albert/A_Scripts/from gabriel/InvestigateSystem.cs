using UnityEngine;

public class InvestigateSystem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Investigate();
    }

    public string GetInteractionText()
    {
        return "Click to Investigate";
    }

    private void Investigate()
    {
        Debug.Log("Hmm?...");

    }
}
