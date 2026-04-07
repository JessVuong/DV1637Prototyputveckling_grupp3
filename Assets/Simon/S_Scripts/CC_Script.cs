using TMPro;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [Tooltip("This value determines speed of character")]
    public float speed = 12f;
    CharacterController cc;


    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {

        float xAxis = Input.GetAxis("Horizontal"); 
        float zAxis = Input.GetAxis("Vertical");    // Calls on Unity's Player Input. Creates a movement vector on the horizontal (X) and vertical (Z) axes
        Vector3 move = new Vector3(xAxis, 0, zAxis);

        if (move.magnitude > 1f)
        {
            move = move.normalized; // Normalizes speed on diagonal. So speed on (1,1) = √2 which is > 1 becomes same as speed on (1,0) = 1
        }

        move *= speed * Time.deltaTime; // Deltatime makes movement consistent regardless of framerate

        move = transform.TransformDirection(move);

        move.y = -9.82f * Time.deltaTime; // Simple gravity via a constant force down on y axis


        cc.Move(move);
    }


    /* // Interact function that can be moved out if necessary
    void Interact() 
    {

            Ray ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.CompareTag("Interactable"))
                {
                 
                }
            }

    }*/
}
