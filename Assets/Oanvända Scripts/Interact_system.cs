using UnityEngine;

public class Interact_system : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("Raycast_System").GetComponent<Raycast_System>().ActiveRay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
