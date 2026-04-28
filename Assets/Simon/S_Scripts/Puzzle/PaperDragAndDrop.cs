using System;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Paper : MonoBehaviour
{ //Most parts copied from ComboLockPuzzle

    [Header("PUZZLE OBJECT REFERENCES")]
    [Tooltip("Interact Object")]
    [SerializeField] private GameObject interactiveBlock;
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
    private Camera mainCam;
    //because I had some issues I'm just making sure the cameras work


    void Start()
    {
        mainCam = Camera.main;
        EndPuzzle();
    }

    void Update()
    {
        if (!puzzleStarts) return;

        // Right-click exits puzzle (can be replaced later)
        if (Input.GetMouseButtonDown(1))
        {
            EndPuzzle();
        }

        // PICKUP
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = Cast();
            if (hit.collider != null && hit.collider.CompareTag("drag"))
            {
                selectedPaper = hit.collider.gameObject;
                Cursor.visible = false;
            }
        }

        // DRAG
        if (Input.GetMouseButton(0) && selectedPaper != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.WorldToScreenPoint(selectedPaper.transform.position).z);
            Vector3 worldposition = Camera.main.ScreenToWorldPoint(position);
            selectedPaper.transform.position = new Vector3(worldposition.x, 1.25f, worldposition.z);
        }

        // DROP
        if (Input.GetMouseButtonUp(0) && selectedPaper != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.WorldToScreenPoint(selectedPaper.transform.position).z);
            Vector3 worldposition = Camera.main.ScreenToWorldPoint(position);
            selectedPaper.transform.position = new Vector3(worldposition.x, 1.153f, worldposition.z);
            selectedPaper = null;
            Cursor.visible = true;
        }
    }


    public IEnumerator CompletedGame() 
    {
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(1.0f);

        interactiveBlock.SetActive(true);
        EndPuzzle();
        this.gameObject.SetActive(false);

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

        puzzleStarts = true;
    }

    public void EndPuzzle()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cc.enabled = true;

        interactiveBlock.SetActive(true);

        // restoring camera state
        mainCam.transform.SetPositionAndRotation(savedCamPos, savedCamRot);

        puzzleStarts = false;
    }

    private RaycastHit Cast()
    {
        Vector3 screenMousePos_F = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePos_N = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePos_F = Camera.main.ScreenToWorldPoint(screenMousePos_F);
        Vector3 worldMousePos_N = Camera.main.ScreenToWorldPoint(screenMousePos_N);

        RaycastHit hit;

        Physics.Raycast(worldMousePos_N, worldMousePos_F, out hit);

        return hit;
    }

}