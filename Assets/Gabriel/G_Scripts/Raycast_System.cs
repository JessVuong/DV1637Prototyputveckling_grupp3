using UnityEngine;
using UnityEngine.UI;

public class Raycast_System : MonoBehaviour
{
    public Text label;
    float maxDist = 100f;
    public LayerMask LayersToCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActiveRay();
        }
            
    }

    private void ActiveRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxDist, LayersToCheck))
        {
            label.text = hit.collider.gameObject.name;
        }
        else
        {
            Debug.Log("Low range");
        }
    }
}
