using UnityEngine;

public class Active_Ray : MonoBehaviour
{
    // Max distance for the raycast
    float maxDist = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveRay();
    }

    // The active raycast function
    public void ActiveRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDist))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            interactable?.Interact();
        }
    }
}
