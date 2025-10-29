using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : NetworkBehaviour
{
    public PlayerInventory inventory;
    public Transform slotParent;
    public GameObject slotPrefab;

    private Image[] slotImages;

    void Start()
    {
        slotImages = new Image[inventory.maxSlots];
        for (int i = 0; i < inventory.maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slotImages[i] = slot.GetComponent<Image>();
        }
    }

    void Update()
    {
        for (int i = 0; i < inventory.maxSlots; i++)
        {
            if (i < inventory.items.Count)
                slotImages[i].sprite = inventory.items[i].icon;
            else
                slotImages[i].sprite = null;

            slotImages[i].color = (i == inventory.currentIndex) ? Color.white : Color.gray;
        }
    }
}