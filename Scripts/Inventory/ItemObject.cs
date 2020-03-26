using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject {

    public string itemName = "Item";
    public string description;
    public Sprite image = null;

    [HideInInspector]
    public bool stackable;
    [HideInInspector]
    public int maxAmount;


    public enum ItemType {
        melee,
        ranged,
        food,
        potion,
        cosmetic,
        storage,
        magic,
        quest
    }

    public ItemType itemType;

    
}
