using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class PulleyScript : MonoBehaviour, IInteractable
{
    public Animator animator;
    public SoundManager soundManager;
    public Inventory_System inventory;
    public GameObject door;
    public GameObject hammer;
    public bool hasInteracted = false;

    [Tooltip("HUD")]
    [SerializeField] private HUDControl hud;

    public void Interact()
    {
        if (!inventory.HasItem(Inv_ItemType.Hammer))
        {
            StartCoroutine("Ajar");
        }

        if (inventory.HasItem(Inv_ItemType.Hammer))
        {
            hammer.SetActive(true);
            door.GetComponent<Animator>().SetBool("IsOpen", true);
            door.GetComponent<OpenSystem>().opened = true;
            SoundManager.PlaySound(SoundType.UnlockDoor);
        }

    }

    public string GetInteractionText()
    {
        return "Pulled";
    }


    public IEnumerator Ajar() //using IE to cause a small wait . for camera
    {
        hud.ShowHint("I need to weigh this down with something...");
        door.GetComponent<Animator>().SetTrigger("Ajar");
        SoundManager.PlaySound(SoundType.UnlockDoor);
        yield return new WaitForSecondsRealtime(1.5f);
        SoundManager.PlaySound(SoundType.AjarClose);
        hasInteracted = true;



    }

}
