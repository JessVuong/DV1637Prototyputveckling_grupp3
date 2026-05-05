using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour, IInteractable
{
    public Inventory_System inventory;
    public GameObject cannonBall;
    public GameManagerScript victory;

    private int i = 0;

    [Tooltip("HUD")]
    [SerializeField] private HUDControl hud;
    private string hintText;

    public void Interact()
    {
        if (inventory.HasItem(Inv_ItemType.Gunpowder) && inventory.HasItem(Inv_ItemType.Fuse) && inventory.HasItem(Inv_ItemType.Cannonball))
        {

            switch (i)
            {
                case 0:
                    hud.ShowHint("Gunpowder loaded", 1f);
                    break;
                case 1:
                    hud.ShowHint("Cannonball loaded", 1f);
                    break;
                case 2:
                    hud.ShowHint("Fuse inserted", 1f);
                    break;
                case 3:
                    hud.ShowHint("Fire!");
                    break;
            }
            
            if (i == 3)
            {
                StartCoroutine("WinScreen");
            }

            i++;

        } 
        
        if (!inventory.HasItem(Inv_ItemType.Gunpowder) || !inventory.HasItem(Inv_ItemType.Fuse) || !inventory.HasItem(Inv_ItemType.Cannonball))
        {
            hintText = "I think I need:";
            if (!inventory.HasItem(Inv_ItemType.Gunpowder))
                hintText += " Gunpowder";
            if (!inventory.HasItem(Inv_ItemType.Fuse))
                hintText += " Fuse";
            if (!inventory.HasItem(Inv_ItemType.Cannonball))
                hintText += " Cannonball";
            hud.ShowHint(hintText);
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
