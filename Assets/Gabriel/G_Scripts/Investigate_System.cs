using UnityEngine;

public class Investigate_System : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Investigate();
    }

    public string GetInteractionText()
    {
        return "Click to Investigate";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Investigate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hmm?...");
        }

    }
}
