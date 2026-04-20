using TMPro;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [Tooltip("This value determines speed of character")]
    public float speed = 12f;

    [Tooltip("How quickly the player slows down (friction)")]
    public float friction = 100f;

    private Vector3 velocity; // stores current movement

    CharacterController cc;

    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {

        float xAxis = Input.GetAxis("Horizontal"); 
        float zAxis = Input.GetAxis("Vertical");    // Calls on Unity's Player Input. Creates a movement vector on the horizontal (X) and vertical (Z) axes
        Vector3 input = new Vector3(xAxis, 0, zAxis);

        if (input.magnitude > 1f) 
            input = input.normalized;
        
        input = transform.TransformDirection(input);

        // velocity based on input
        Vector3 targetVelocity = input * speed;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;

        if (input.magnitude < 0.1f)
        {
            // Apply friction (slow down when no input)
            velocity = Vector3.Lerp(velocity, Vector3.zero, friction * Time.deltaTime);
        }

        // Apply gravity
        velocity.y = -9.82f;

        cc.Move(velocity * Time.deltaTime);
    }
}
