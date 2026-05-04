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

    public GameObject rmbText;

    public Inventory_System inventory;
    private int requiredPaperPieces = 5;


    [Tooltip("Virtual Camera on PlayerPrefab")]
    [SerializeField] private GameObject gameplayCamera;
    
    [SerializeField] private Camera mainCam;


    private float dragDepth; // Distance from camera to object at pickup time (locks depth so it doesn't drift)
    private Vector3 dragOffset; // Difference between object pivot and exact click point (prevents snapping to center)

    [Tooltip("PlayerPrefabTorch")]
    [SerializeField] private GameObject Torch;

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

            selectedPaper.transform.position = new Vector3(targetPosition.x, .81f, targetPosition.z);
        }
        // DROP
        if (Input.GetMouseButtonUp(0) && selectedPaper != null)
        {
            Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragDepth);
            Vector3 worldMousePoint = mainCam.ScreenToWorldPoint(mousePoint);
            Vector3 targetPosition = worldMousePoint + dragOffset;

            selectedPaper.transform.position = new Vector3(targetPosition.x, .78f, targetPosition.z);

            selectedPaper = null;
            Cursor.visible = true;
        }
    }

    // PUZZLE CONTROL

    public void StartPuzzle()
    {
        
        if (inventory.Paper_Pieces < requiredPaperPieces)
        {
            Debug.Log("There are still some missing pieces.");

            return;
        }

        rmbText.SetActive(true);

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;

        mainCam.gameObject.SetActive(false); //Reloads Camera possible 1 frame stutter
        mainCam.gameObject.SetActive(true);

        gameplayCamera.SetActive(false);
        closeUpCamera.SetActive(true);
        cc.enabled = false;

        PaperPuzzleMain.GetComponent<BoxCollider>().enabled = false;
        PaperPile.SetActive(true);

        puzzleStarts = true;

        Torch.SetActive(false);



    }
    public IEnumerator WaitToExit() //using IE to cause a small wait . for camera
    {


        yield return new WaitForSeconds(.2f);


    }
    public void EndPuzzle()
    {
        rmbText.SetActive(false);

        StartCoroutine("WaitToExit");

        Cursor.lockState = CursorLockMode.Locked;

        closeUpCamera.SetActive(false);
        gameplayCamera.SetActive(true);
        cc.enabled = true;

        PaperPuzzleMain.GetComponent<BoxCollider>().enabled = true;

        puzzleStarts = false;

        Torch.SetActive(true);
    }

    private RaycastHit Cast()
    {

        Camera cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit hit);
        
        return hit;
    }

}