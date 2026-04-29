using System;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Paper : MonoBehaviour, IInteractable
{ 

    [Header("PUZZLE OBJECT REFERENCES")]
    [Tooltip("Interact Object")]
    [SerializeField] private GameObject PaperPuzzleMain;
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


    private float dragDepth; // Distance from camera to object at pickup time (locks depth so it doesn't drift)
    private Vector3 dragOffset; // Difference between object pivot and exact click point (prevents snapping to center)

    void Start()
    {
        mainCam = Camera.main;
    }

    public void Interact()
    {
        StartPuzzle();
    }

    public string GetInteractionText()
    {
        return "Click to start puzzle...";
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

        PaperPuzzleMain.GetComponent<BoxCollider>().enabled = false;
        PaperPile.SetActive(true);

        puzzleStarts = true;

        mainCam.gameObject.SetActive(false); //Reloads Camera possible 1 frame stutter
        mainCam.gameObject.SetActive(true);



    }

    public void EndPuzzle()
    {
        Cursor.lockState = CursorLockMode.Locked;

        closeUpCamera.SetActive(false);
        cc.enabled = true;

        PaperPuzzleMain.GetComponent<BoxCollider>().enabled = true;

        // restoring camera state
        mainCam.transform.SetPositionAndRotation(savedCamPos, savedCamRot);
        mainCam.GetComponent<Camera>().fieldOfView = 60f;

        puzzleStarts = false;
    }

    private RaycastHit Cast()
    {

        Camera cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit hit);
        
        return hit;
    }

}