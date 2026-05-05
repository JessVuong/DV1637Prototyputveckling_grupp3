using UnityEngine;

public class PulleyScript : MonoBehaviour, IInteractable
{
    public Animator animator;
    public Inventory_System inventory;
    public GameObject door;
    public GameObject hammer;

    public void Interact()
    {
        if (!inventory.HasItem(Inv_ItemType.Hammer))
        { 
            Ajar();
        }

        if (inventory.HasItem(Inv_ItemType.Hammer))
        {
            hammer.SetActive(true);
            door.GetComponent<Animator>().SetBool("IsOpen", true);
            door.GetComponent<OpenSystem>().opened = true;  
        }

    }

    public string GetInteractionText()
    {
        return "Pulled";
    }

    private void Ajar()
    {
        
        door.GetComponent<Animator>().SetTrigger("Ajar");

    }

}
