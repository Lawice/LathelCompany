using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject   
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
    public bool canBeUsed;
    public float sellPrice;
}
