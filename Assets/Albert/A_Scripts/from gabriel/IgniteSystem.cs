using UnityEngine;

public class IgniteSystem : MonoBehaviour, IInteractable
{
    public Inventory_System inventory;

    [Tooltip("HUD")]
    [SerializeField] private HUDControl hud;

    public SoundManager soundManager;

    public void Interact()
    {
        Ignite();
    }

    public string GetInteractionText()
    {
        return "Click to Ignite";
    }

    private void Ignite()
    {
        if (inventory.HasItem(Inv_ItemType.Torch))
        {
            Destroy(gameObject);
            SoundManager.PlaySound(SoundType.BraizerIgnite);

        }
        else
        {
            hud.ShowHint("Gotta get through these vines...");
        }
    }
}
