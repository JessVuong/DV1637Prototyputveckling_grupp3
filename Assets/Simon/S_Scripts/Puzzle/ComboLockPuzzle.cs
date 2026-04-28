using System;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ComboLockPuzzle : MonoBehaviour
{
   
    [Header("PUZZLE DATA")]
    private string Answer = "WEALTH";
    [Tooltip("UnityEvent called on puzzle being solved")]
    [SerializeField] private UnityEvent PuzzleSolved;

    [Header("PUZZLE OBJECT REFERENCES")]
    [Tooltip("Interact Object")]
    [SerializeField] private GameObject interactiveLock;
    [Tooltip("Puzzle Object")]
    [SerializeField] private GameObject puzzleLock;
    [Tooltip("Virtual Camera")]
    [SerializeField] private GameObject closeUpCamera;
    [Tooltip("Object with CharacterController script")]
    [SerializeField] private CC_Script cc;
    [Tooltip("Puzzle Overlay UI Panel")]
    [SerializeField] private GameObject UIPanel;

    private bool puzzleStarts;

    [Header("CYLINDER SYSTEM")]

    [Tooltip("Input for each of the Cylinder Disks")]
    [SerializeField] private GameObject[] cylinders;
    // Letter mapping for each step index
    private readonly string[] letters = { "L", "W", "A", "TH", "E" }; //L[0], W[1], A[2], TH[3], E[4]

    [Tooltip("Starting Position for disks \n L [0], W [1], A [2], TH [3], E [4]")]
    [SerializeField] private int[] startingSteps; // Starting index for each cylinder

    private int[] cylinderSteps;

    // Each step represents 72 degrees (360/5=72)
    private float rotationStep = 72f;


    private Vector3 savedCamPos;
    private Quaternion savedCamRot;
    private Camera mainCam;
    //because I had some issues I'm just making sure the cameras work

    void Start()
    {
        cylinderSteps = new int[cylinders.Length];

        for (int i = 0; i < cylinders.Length; i++)
        {
            int start = (startingSteps != null && i < startingSteps.Length)
                ? startingSteps[i]
                : 0;

            cylinderSteps[i] = start;
        }
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
    }

    // UI BUTTON CONTROLS

    // Rotate cylinder upward
    public void OnCylinderUp(int index)
    {
        RotateCylinder(index, +1);
    }

    // Rotate cylinder downward
    public void OnCylinderDown(int index)
    {
        RotateCylinder(index, -1);
    }

    // Core rotation logic for any cylinder
    private void RotateCylinder(int index, int direction)
    {

        // Wrap around between 0–4 (5 states total) +4=-1 in a 5-step cycle.
        cylinderSteps[index] = (cylinderSteps[index] + direction + 5) % 5;

        // Apply rotation in world space
        cylinders[index].transform.localEulerAngles =
            new Vector3(cylinderSteps[index] * rotationStep, 0f, 0f);
        
        CheckCode();
    }

    // Get current letter for a cylinder
    public string GetCylinderLetter(int index)
    {
        return letters[cylinderSteps[index]];
    }

    public void CheckCode()
    {
        string currentCode = GetCylinderLetter(0) + GetCylinderLetter(1) + GetCylinderLetter(2) + GetCylinderLetter(3) + GetCylinderLetter(4);
        

        if (currentCode == Answer)
        {
            StartCoroutine("CompletedGame");
            Debug.Log("Code is Correct");
        }
        else
        {
            Debug.Log("Incorrect Code");
        }


    }
    public IEnumerator CompletedGame() //using IE to cause a small wait before showing results, unlock animation
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIPanel.SetActive(false);

        // ----------------------------
        // play OpenLock animation here
        // ----------------------------
        yield return new WaitForSeconds(1.0f);

        interactiveLock.SetActive(true);
        EndPuzzle();
        PuzzleSolved.Invoke();
        this.gameObject.SetActive(false);

    }
    // PUZZLE CONTROL

    public void StartPuzzle()
    {
        Cursor.lockState = CursorLockMode.None;
        UIPanel.SetActive(true);

        // saving camera state
        savedCamPos = mainCam.transform.position;
        savedCamRot = mainCam.transform.rotation;

        closeUpCamera.SetActive(true);
        cc.enabled = false;

        interactiveLock.SetActive(false);
        puzzleLock.SetActive(true);

        puzzleStarts = true;
    }

    public void EndPuzzle()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIPanel.SetActive(false);

        closeUpCamera.SetActive(false);
        cc.enabled = true;

        interactiveLock.SetActive(true);
        puzzleLock.SetActive(false);

        // restoring camera state
        mainCam.transform.SetPositionAndRotation(savedCamPos, savedCamRot);

        puzzleStarts = false;
    }
}