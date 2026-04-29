using UnityEngine;

public class PickupSystem : MonoBehaviour, IInteractable
{
    public Inventory_System inventory;
    public GameObject playerTorch;
    public GameObject hud;

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
                inventory.CollectItem(Inv_ItemType.Torch);
                playerTorch.SetActive(true);
                hud.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case "CellKey_Pickup":
                inventory.CollectItem(Inv_ItemType.Key);
                hud.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case "Rope_Pickup":
                inventory.CollectItem(Inv_ItemType.Rope);
                hud.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case "Paper_Pickup":
                inventory.AddPaper_Pieces();
                Destroy(gameObject);
                break;
            case "ArmoryKey_Pickup":
                inventory.CollectItem(Inv_ItemType.Key);
                hud.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case "Hammer_Pickup":
                inventory.CollectItem(Inv_ItemType.Hammer);
                hud.transform.GetChild(2).GetChild(4).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case "Gunpowder_Pickup":
                inventory.CollectItem(Inv_ItemType.Gunpowder);
                hud.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case "Cannonball_Pickup":
                inventory.CollectItem(Inv_ItemType.Cannonball);
                hud.transform.GetChild(2).GetChild(6).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
            case "Fuse_Pickup":
                inventory.CollectItem(Inv_ItemType.Fuse);
                hud.transform.GetChild(2).GetChild(7).gameObject.SetActive(true);
                Destroy(gameObject);
                break;
        }
    }
}
