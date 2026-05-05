using UnityEngine;

public class BrazierInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private SequenceBrazier BrazierPuzzle;
    public void Interact() 
    {
        BrazierPuzzle.GetComponent<SequenceBrazier>().ActivateFire(gameObject);

    }
    public string GetInteractionText()
    {
        return "Click to start puzzle...";
    }
}
