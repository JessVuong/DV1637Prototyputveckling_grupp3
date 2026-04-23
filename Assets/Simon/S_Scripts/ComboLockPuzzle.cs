using UnityEngine;
using UnityEngine.Events;

public class ComboLockPuzzle : MonoBehaviour
{
   
    [Header("PUZZLE DATA")]
    [SerializeField] private string Answer;
    [SerializeField] private UnityEvent PuzzleSolved;

    [Header("PUZZLE OBJECT REFERENCES")]
    [SerializeField] private GameObject interactiveLock;
    [SerializeField] private GameObject puzzleLock;
    [SerializeField] private GameObject closeUpCamera;
    [SerializeField] private CC_Script cc;
    [SerializeField] private GameObject UIPanel;

    private bool puzzleStarts;

    [Header("CYLINDER SYSTEM")]

    [SerializeField] private GameObject[] cylinders;
    // Letter mapping for each step index
    private readonly string[] letters = { "L", "W", "A", "TH", "E" }; //L[0], W[1], A[2], TH[3], E[4]
    [SerializeField] private int[] startingSteps; // Starting index for each cylinder

    private int[] cylinderSteps;

    // Each step represents 72 degrees (360/5=72)
    private float rotationStep = 72f;


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
    }

    // INPUT (OPTIONAL EXIT)

    void Update()
    {
        if (!puzzleStarts) return;

        // Right-click exits puzzle (can be replaced with UI later)
        if (Input.GetMouseButtonDown(1))
        {
            EndPuzzle();
        }
    }

    // UI BUTTON CONTROLS

    // Rotate cylinder upward (next letter)
    public void OnCylinderUp(int index)
    {
        RotateCylinder(index, +1);
    }

    // Rotate cylinder downward (previous letter)
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
    }

    // Get current letter for a cylinder (useful for solving logic)
    public string GetCylinderLetter(int index)
    {
        return letters[cylinderSteps[index]];
    }

    // PUZZLE CONTROL

    public void StartPuzzle()
    {
        Cursor.lockState = CursorLockMode.None;
        UIPanel.SetActive(true);

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

        puzzleStarts = false;
    }
}