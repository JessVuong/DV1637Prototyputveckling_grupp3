using System.Threading;
using System.Collections;
using UnityEngine;

public class SequenceBrazier : MonoBehaviour
{ 
    [Tooltip("Input for each of the Braziers")]
    [SerializeField] private GameObject[] braziers;
    private string CorrectSequence = "10342";
    string input = "";
    public GameObject door;
    public SoundManager soundManager;
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
                
                if (input.Length != 5) { SoundManager.PlaySound(SoundType.BraizerIgnite); }
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
            SoundManager.PlaySound(SoundType.BraizerIgnite);
            Debug.Log("Victory"); //Open Door
            door.GetComponent<Animator>().SetBool("IsOpen", true);
            door.GetComponent<OpenSystem>().opened = true;

            SoundManager.PlaySound(SoundType.UnlockDoor);

        }
        else 
        {
            StartCoroutine("Wrong");
            SoundManager.PlaySound(SoundType.WrongSequence);
        }
    }

    public IEnumerator Wrong() //using IE to cause a small wait . for camera
    {


        yield return new WaitForSeconds(.2f);
        input = "";
        for (int i = 0; i < braziers.Length; i++)
        {
            braziers[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        hud.ShowHint("Let's try that again...");




    }
}
