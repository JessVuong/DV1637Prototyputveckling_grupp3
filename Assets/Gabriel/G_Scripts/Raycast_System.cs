using UnityEngine;
using UnityEngine.UI;

public class Raycast_System : MonoBehaviour
{
    // String "toolTip" is to be used as the string for the HUD tooltip the player gets when hovering over an object
        public string toolTip;
    
    // Max distance for the raycast
        float maxDist = 100f;

        public LayerMask LayersToCheck;

    [Header("Interact systems")]
    // Interact to open
        public Open_System Open;
    // Interact to investigate
        public Investigate_System Investigate;
    // Interact to pick up
        public Pickup_System Pickup;
    // Interact using torch
        public Ignite_System Ignite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveRay();
        Interact();
    }

    public void ActiveRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxDist, LayersToCheck))
        {
            if (hit.collider.CompareTag("OBJ_Open"))
            {
                toolTip = "Open";
            }
            else if (hit.collider.CompareTag("OBJ_Investigate"))
            {
                toolTip = "Investigate";
            }
            else if (hit.collider.CompareTag("OBJ_Pickup"))
            {
                toolTip = "Pick up";
            }
            else if (hit.collider.CompareTag("OBJ_Ignite"))
            {
                toolTip = "Ignite";
            }
        }
    }

    public void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (toolTip == "Open")
            {

            }
        }
    }

    public void Trigger()
    {

    }
}
