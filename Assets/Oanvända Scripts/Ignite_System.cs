using UnityEngine;

public class Ignite_System : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Ignite();
    }

    public string GetInteractionText()
    {
        return "Click to Ignite";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Ignite()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fire!");
        }
    }
}
