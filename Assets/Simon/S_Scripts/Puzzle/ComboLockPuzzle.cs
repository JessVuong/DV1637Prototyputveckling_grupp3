using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;

public class ComboLockPuzzle : MonoBehaviour, IInteractable
{
   
    [Header("PUZZLE DATA")]
    private string Answer = "WEALTH";

    [Header("PUZZLE OBJECT REFERENCES")]
    [Tooltip("Virtual Camera")]
    [SerializeField] private GameObject closeUpCamera;
    [Tooltip("Object with CharacterController script")]
    [SerializeField] private CC_Script cc;

    [Tooltip("Virtual Camera on PlayerPrefab")]
    [SerializeField] private GameObject gameplayCamera;

    [SerializeField] private Camera mainCam;

    [Tooltip("Puzzle Overlay UI Panel")]
    [SerializeField] private GameObject UIPanel;

    private bool puzzleStarts;

    [Header("CYLINDER SYSTEM")]

    [Tooltip("Input for each of the Cylinder Disks")]
    [SerializeField] private GameObject[] cylinders;
    // Letter mapping for each step index
    private readonly string[] letters = { "E", "L", "W", "A", "TH" }; //E[0], L[1], W[2], A[3], TH[4]

    [Tooltip("Starting Position for disks \n E [0], L [1], W [2], A [3], TH [4]")]
    [SerializeField] private int[] startingSteps; // Starting index for each cylinder

    private int[] cylinderSteps;

    // Each step represents 72 degrees (360/5=72)
    private float rotationStep = 72f;
    
    [Header("GRAPHICS")]
    [Tooltip("UI RightClick Text")]
    public GameObject rmbText;
    [Tooltip("LockAnimator")]
    [SerializeField] private Animator LockAnim;
    [Tooltip("ChestAnimator")]
    [SerializeField] private Animator ChestAnim;

    public void Interact()
    {
        StartPuzzle();
    }

    public string GetInteractionText()
    {
        return "Click to start puzzle...";
    }
    void Start()
    {
        cylinderSteps = new int[cylinders.Length];

        for (int i = 0; i < cylinders.Length; i++)
        {
            int start = (startingSteps != null && i < startingSteps.Length)? startingSteps[i]: 0;

            cylinderSteps[i] = start;

            // Apply starting visual rotation
            cylinders[i].transform.localEulerAngles =
                new Vector3(start * rotationStep, 0f, 0f);
        }
        //StartPuzzle(); //Only for testing prior to Interact
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
        RotateCylinder(index, -1); //Changes direction of rotation
    }

    // Rotate cylinder downward
    public void OnCylinderDown(int index)
    {
        RotateCylinder(index, +1);
    }

    // Core rotation logic for any cylinder
    private void RotateCylinder(int index, int direction)
    {

        // Wrap around between 0–4 (5 states total) +4=-1 in a 5-step cycle.
        cylinderSteps[index] = (cylinderSteps[index] + direction + 5) % 5;

        // Apply rotation in world space
        cylinders[index].transform.localEulerAngles = new Vector3(cylinderSteps[index] * rotationStep, 0f, 0f);
        
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
            Debug.Log("Debug.Log: Code is Correct");
        }
        else
        {
            Debug.Log("Debug.Log: Incorrect Code: " + GetCylinderLetter(0) + GetCylinderLetter(1) + GetCylinderLetter(2) + GetCylinderLetter(3) + GetCylinderLetter(4));
        }


    }
    public IEnumerator CompletedGame() //using IE to cause a small wait before showing results, unlock animation
    {
        
        UIPanel.SetActive(false);
        EndPuzzle();
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        LockAnim.SetTrigger("t_LockOpen");
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        ChestAnim.SetTrigger("t_ChestOpen");


        

    }
    // PUZZLE CONTROL

    public void StartPuzzle()
    {
        rmbText.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        UIPanel.SetActive(true);

        mainCam.gameObject.SetActive(false); //Reloads Camera possible 1 frame stutter
        mainCam.gameObject.SetActive(true);

        gameplayCamera.SetActive(false);
        closeUpCamera.SetActive(true);
        cc.enabled = false;


        puzzleStarts = true;
    }
    public IEnumerator WaitToExit() //using IE to cause a small wait . for camera
    {

        
        yield return new WaitForSeconds(.2f);


    }
    // PUZZLE CONTROL

    public void EndPuzzle()
    {
        rmbText.SetActive(false);
        StartCoroutine("WaitToExit");

        Cursor.lockState = CursorLockMode.Locked;
        UIPanel.SetActive(false);

        

        gameplayCamera.SetActive(true);
        closeUpCamera.SetActive(false);
        cc.enabled = true;


        

        puzzleStarts = false;
    }
}