using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour, IInteractable
{
    public Inventory_System inventory;
    public GameObject cannonBall;
    public GameManagerScript victory;

    public void Interact()
    {
        if(inventory.HasItem(Inv_ItemType.Gunpowder)&& inventory.HasItem(Inv_ItemType.Fuse)&& inventory.HasItem(Inv_ItemType.Cannonball))
        {
            StartCoroutine("WinScreen");

        }
    }
    
    public string GetInteractionText()
    {
        return "Fire Cannon";
    }

    public IEnumerator WinScreen()
    {
        Debug.Log("Fire");
        cannonBall.GetComponent<Animator>().SetTrigger("Fire");
        
        yield return new WaitForSeconds(3f);
        
        victory.Victory();
    }




}
