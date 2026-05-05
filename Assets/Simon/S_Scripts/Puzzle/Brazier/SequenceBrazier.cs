using System.Threading;
using UnityEngine;

public class SequenceBrazier : MonoBehaviour
{ 
    [Tooltip("Input for each of the Braziers")]
    [SerializeField] private GameObject[] braziers;
    private string CorrectSequence = "10342";
    string input = "";
    public GameObject door;

    [Tooltip("HUD")]
    [SerializeField] private HUDControl hud;

    public void ActivateFire(GameObject brazier) 
    {
        for (int i = 0;i < braziers.Length; i++)
        {

            if (braziers[i] == brazier && !input.Contains(i.ToString()))
            {
                Debug.Log(i);
                input +=  i;
                brazier.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (input.Length == 5) 
        { 
            CheckSequence();  
        }
    }

    void CheckSequence() 
    {
        if (input == CorrectSequence)
        {
            Debug.Log("Victory"); //Open Door
            door.GetComponent<Animator>().SetBool("IsOpen", true);
            door.GetComponent<OpenSystem>().opened = true;
        }
        else 
        { 
            input = "";
            for (int i = 0; i < braziers.Length; i++) 
            {
                braziers[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            hud.ShowHint("Let's try that again...");

        }
    }
}
