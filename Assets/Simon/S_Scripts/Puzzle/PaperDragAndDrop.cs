using System;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Paper : MonoBehaviour
{ 

    [Header("PUZZLE OBJECT REFERENCES")]
    [Tooltip("Interact Object")]
    [SerializeField] private GameObject interactiveBlock;
    [SerializeField] private GameObject blockReplacement;
    [Tooltip("Puzzle Object")]
    [SerializeField] private GameObject PaperPile;
    [Tooltip("Virtual Camera")]
    [SerializeField] private GameObject closeUpCamera;
    [Tooltip("Object with CharacterController script")]
    [SerializeField] private CC_Script cc;

    private bool puzzleStarts;
    private GameObject selectedPaper;

    /*
    [SerializeField] private Inventory_System inventory;
    private int requiredPaperPieces = 5;
     */


    private Vector3 savedCamPos;
    private Quaternion savedCamRot;
    [SerializeField] private Camera mainCam;
    //because I had some issues I'm just making sure the cameras work


    private float dragDepth; // Distance from camera to object at pickup time (locks depth so it doesn't drift)
    private Vector3 dragOffset; // Difference between object pivot and exact click point (prevents snapping to center)

    void Start()
    {
        mainCam = Camera.main;
        //EndPuzzle();
        StartPuzzle();
    }



    void Update() { 
        if (!puzzleStarts) return; 
        
        // Right-click exits puzzle (can be replaced later)
        if (Input.GetMouseButtonDown(1)) 
        { 
            EndPuzzle(); 
        }
        // PICK UP
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = Cast();

            if (hit.collider != null && hit.collider.CompareTag("Paper_Drag"))
            {
                selectedPaper = hit.collider.gameObject;
                Cursor.visible = false;

                dragDepth = mainCam.WorldToScreenPoint(selectedPaper.transform.position).z;

                Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragDepth);
                Vector3 worldMousePoint = mainCam.ScreenToWorldPoint(mousePoint);

                dragOffset = selectedPaper.transform.position - worldMousePoint;
            }
        }
        // DRAG
        if (Input.GetMouseButton(0) && selectedPaper != null)
        {
            Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragDepth);
            Vector3 worldMousePoint = mainCam.ScreenToWorldPoint(mousePoint);
            Vector3 targetPosition = worldMousePoint + dragOffset;

            selectedPaper.transform.position = new Vector3(targetPosition.x, .95f, targetPosition.z);
        }
        // DROP
        if (Input.GetMouseButtonUp(0) && selectedPaper != null)
        {
            Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragDepth);
            Vector3 worldMousePoint = mainCam.ScreenToWorldPoint(mousePoint);
            Vector3 targetPosition = worldMousePoint + dragOffset;

            selectedPaper.transform.position = new Vector3(targetPosition.x, .92f, targetPosition.z);

            selectedPaper = null;
            Cursor.visible = true;
        }
    }

    // PUZZLE CONTROL

    public void StartPuzzle()
    {
        /*      TEMPORARY IDEA UNTIL I GET CONFIRMATION ON HOW TO DO IT
        if (inventory.GetItemAmount("Paper_pieces") < requiredPaperPieces)
        {
            Debug.Log("There are still some missing pieces.");
            return;
        }
        */

        Cursor.lockState = CursorLockMode.None;
        
        // saving camera state
        savedCamPos = mainCam.transform.position;
        savedCamRot = mainCam.transform.rotation;

        closeUpCamera.SetActive(true);
        cc.enabled = false;

        interactiveBlock.SetActive(false);
        PaperPile.SetActive(true);
        blockReplacement.SetActive(true);

        puzzleStarts = true;

        mainCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);



    }

    public void EndPuzzle()
    {
        Cursor.lockState = CursorLockMode.Locked;

        closeUpCamera.SetActive(false);
        cc.enabled = true;

        interactiveBlock.SetActive(true);

        // restoring camera state
        mainCam.transform.SetPositionAndRotation(savedCamPos, savedCamRot);
        mainCam.GetComponent<Camera>().fieldOfView = 60f;

        puzzleStarts = false;
    }

    private RaycastHit Cast()
    {

        Camera cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);

        Physics.Raycast(ray, out RaycastHit hit);
        
        return hit;
    }

}