using UnityEngine;

public class ActiveRay : MonoBehaviour
{
    // Max distance for the raycast
    public float maxDist = 2f;

    // Variables to track the current interactable being looked at
    private IInteractable currentInteractable;

    [SerializeField] private GameObject hud;
    private GameObject crosshairHighlight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        crosshairHighlight = hud.transform.GetChild(4).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Updates currentInteractable
        CheckRaycast();

        // If the player presses LMB and is looking at an interactable, call the interact function of the interactable looked at
        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    // The function checking what type of interactable the player is looking at and updating the variable currentInteractable, if the player is not looking at an interactable the variable is set to null
    void CheckRaycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDist))
        {
            currentInteractable = hit.collider.GetComponent<IInteractable>();

            if (currentInteractable != null)
            {
                crosshairHighlight.SetActive(true);
                //Debug.Log(currentInteractable.GetInteractionText());
            }
        }
        else
        {
            crosshairHighlight.SetActive(false);
            currentInteractable = null;
        }
    }
}
