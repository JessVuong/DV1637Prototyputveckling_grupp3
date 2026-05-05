using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour, IInteractable
{
    public Inventory_System inventory;
    public GameObject cannonBall;
    public GameManagerScript victory;

    private int i = 0;

    public void Interact()
    {
       if (inventory.HasItem(Inv_ItemType.Gunpowder) && inventory.HasItem(Inv_ItemType.Fuse) && inventory.HasItem(Inv_ItemType.Cannonball))
       {
            i++;
            
            if (i == 3)
            {
                StartCoroutine("WinScreen");
            }
            
            Debug.Log(i);
            
       } 
    }
    
    public string GetInteractionText()
    {
        return "Fire Cannon";
    }

    public IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(0.5f);

        cannonBall.GetComponent<Animator>().SetTrigger("Fire");
        
        yield return new WaitForSeconds(2f);
        
        victory.Victory();
    }




}
