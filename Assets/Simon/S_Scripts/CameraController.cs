using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("This value determines sensitivity of mouse")]
    [SerializeField] // Makes the private variable right below it editable within the Editor without making it public
    float mSensitivity = 300f;
    float xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks Cursor when looking around.
    }

    private void Update()
    {
        float mX = Input.GetAxis("Mouse X"); 
        float mY = Input.GetAxis("Mouse Y");

        mX *= mSensitivity * Time.deltaTime; // Mouse movement is scaled by sensitivity and deltatime
        mY *= mSensitivity * Time.deltaTime;

        xRot -= mY;

        xRot = Mathf.Clamp(xRot, -90f, 90f); // Vertical rotation is clamped to 90 degrees

        transform.localRotation =Quaternion.Euler(xRot, 0, 0); 

        transform.parent.Rotate(Vector3.up, mX, Space.World); // Rotates the grandparent object (the character) on the y axis when looking left or right
    }
}
