using UnityEngine;

public class PickupSystem : MonoBehaviour, IInteractable
{
    public Inventory_System inventory;
    public GameObject playerTorch;
    public GameObject hud;
    public SoundManager soundManager;

    [Tooltip("HUD")]
    [SerializeField] private HUDControl hudScript;

    public void Interact()
    {
        PickUp();
    }

    public string GetInteractionText()
    {
        return "Click to Pick up";
    }

    private void PickUp()
    {
        switch (tag)
        {
            case "Start_Torch":
                SoundManager.PlaySound(SoundType.Interact);
                SoundManager.PlaySound(SoundType.Flame,1, true);
                inventory.CollectItem(Inv_ItemType.Torch);
                playerTorch.SetActive(true);
                hud.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case "CellKey_Pickup":
                SoundManager.PlaySound(SoundType.Interact);
                inventory.CollectItem(Inv_ItemType.Key);
                hud.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                Destroy(gameObject);
                hudScript.ShowHint("I picked up the key...", 1f);
                break;
            case "Rope_Pickup":
                SoundManager.PlaySound(SoundType.Interact);
                inventory.CollectItem(Inv_ItemType.Rope);
                hud.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                Destroy(gameObject);
                hudScript.ShowHint("I picked up the rope...", 1f);
                break;
            case "Paper_Pickup":
                SoundManager.PlaySound(SoundType.Interact);
                inventory.AddPaper_Pieces();
                hud.transform.GetChild(2).GetChild(3).GetChild(inventory.Paper_Pieces-1).gameObject.SetActive(true);
                Destroy(gameObject);
                hudScript.ShowHint("I picked up the piece of paper...", 1f);
                break;
            case "ArmoryKey_Pickup":
                SoundManager.PlaySound(SoundType.Interact);
                inventory.CollectItem(Inv_ItemType.Key);
                hud.transform.GetChild(2).GetChild(4).gameObject.SetActive(true);
                Destroy(gameObject);
                hudScript.ShowHint("I picked up the key...", 1f);
                break;
            case "Hammer_Pickup":
                SoundManager.PlaySound(SoundType.Interact);
                inventory.CollectItem(Inv_ItemType.Hammer);
                hud.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
                Destroy(gameObject);
                hudScript.ShowHint("This can probably weigh something down...");
                break;
            case "Gunpowder_Pickup":
                SoundManager.PlaySound(SoundType.Interact);
                inventory.CollectItem(Inv_ItemType.Gunpowder);
                hud.transform.GetChild(2).GetChild(6).gameObject.SetActive(true);
                Destroy(gameObject);
                hudScript.ShowHint("I picked up the pouch of gunpowder...");
                break;
            case "Cannonball_Pickup":
                SoundManager.PlaySound(SoundType.Interact);
                inventory.CollectItem(Inv_ItemType.Cannonball);
                hud.transform.GetChild(2).GetChild(7).gameObject.SetActive(true);
                Destroy(gameObject);
                hudScript.ShowHint("I picked up the cannonball...", 1f);
                break;
            case "Fuse_Pickup":
                if (inventory.HasItem(Inv_ItemType.Rope))
                {
                    SoundManager.PlaySound(SoundType.RopeCut);
                    inventory.CollectItem(Inv_ItemType.Fuse);
                    hud.transform.GetChild(2).GetChild(8).gameObject.SetActive(true);
                    hudScript.ShowHint("I made a fuse by cutting the rope...");
                    
                }
                else
                {
                    hudScript.ShowHint("I think I saw a rope earlier...", 1f);
                }
                    break;
        }
    }
}
