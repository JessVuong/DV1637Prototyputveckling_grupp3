using UnityEngine;

public class Open_System : MonoBehaviour, IInteractable
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Open()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsOpen", true);
            opened = true;
        }
            
    }
}
