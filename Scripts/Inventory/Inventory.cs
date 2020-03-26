using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    MovableItemDrag movableItemDrag;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;

        movableItemDrag = GameObject.Find("movableItem").GetComponent<MovableItemDrag>();


    }


    public const int maxInventorySize=100;
    public int currentInventorySize = 25;

    public delegate void InventoryChanged(int index, ItemObject itemObject, int amount);
    public static event InventoryChanged inventoryChanged; //delegate

    [SerializeField]
    ItemObject[] items = new ItemObject[maxInventorySize];
    [SerializeField]
    int[] amountData = new int[maxInventorySize];


    public bool AddingItem(ItemObject item, int amount = 1, int index = -2, bool draggedItem = false) {
        //SpecifiedSlot
        if (index != -2) {

            //Occupied
            if (items[index] != null) {

                if (item.stackable) {
                    //Same item
                    if (items[index] == item) {

                        //Can pick up all items into the slot
                        Debug.Log("spec slot, stack, same item, can pick up all");
                        if (amountData[index] + amount <= item.maxAmount) {
                            amountData[index] += amount;

                            if (inventoryChanged != null) {
                                inventoryChanged.Invoke(index, item, amountData[index]);
                            }

                            return true;

                        } else {
                            //Items were dragged
                            if (draggedItem) {
                                int toMouse = amountData[index] + amount - item.maxAmount;
                                amountData[index] = item.maxAmount;

                                if (inventoryChanged != null) {
                                    inventoryChanged.Invoke(index, item, item.maxAmount);
                                }

                                Debug.Log("Not enough space in slot, items dragged, " + item.name + toMouse + "to mouse");
                                movableItemDrag.DragItem(true, item, toMouse);
                                return false;

                            } else {
                                int toNext = amountData[index] + amount - item.maxAmount;
                                amountData[index] = item.maxAmount;
                                if (inventoryChanged != null) {
                                    inventoryChanged.Invoke(index, item, item.maxAmount);
                                }
                                Debug.Log("Not enough space in slot, items NOT dragged, " + item.name + toNext + "to nextSlot");
                                AddingItem(item, toNext);

                                return true;
                            }
                        }

                    //Not the same item
                    } else {
                        if (draggedItem) {
                            Debug.Log("switch items around with mouse");
                            //switch items
                            ItemObject temporaryItem = items[index];
                            int temporaryAmount = amountData[index];

                            Inventory.instance.RemoveItem(index);
                            Inventory.instance.AddingItem(movableItemDrag.movableItemObject, movableItemDrag.amount, index, true);

                            movableItemDrag.DragItem(true, temporaryItem, temporaryAmount);
                            return true;

                        } else {
                            Debug.LogError("Inventory: something tried to put an item " + item.name + " " + amount + " into slot " + index + ", which was occupied by " + items[index] + " " + amountData[index]);
                            return false;
                        }
                    }
                    //Not stackable item
                } else {
                    if (draggedItem) {
                        Debug.Log("Dragged item to slot, inSlot item to mouse");
                        ItemObject temporaryItem = items[index];
                        int temporaryAmount = 1;

                        Inventory.instance.RemoveItem(index);
                        Inventory.instance.AddingItem(movableItemDrag.movableItemObject, movableItemDrag.amount, index, true);

                        movableItemDrag.DragItem(true, temporaryItem, temporaryAmount);
                        return true;
                    } else {
                        Debug.LogError("Inventory: something tried to put an item " + item.name + " " + amount + " into slot " + index + ", which was occupied by " + items[index] + " " + amountData[index]);
                        return false;
                    }
                }

                //Not occupied
            } else {
                Debug.Log("spec slot, not occupied");
                items[index] = item;
                amountData[index] = amount;

                if (inventoryChanged != null) {
                    inventoryChanged.Invoke(index, item, amount);
                }
                return true;
            }
        //no specified slot
        } else {
            if (item.stackable) {
                int availableSlot = FindSlot(item);
                if (availableSlot == -123) {
                    Debug.Log("Inventory full");
                    return false;
                } else {
                    Debug.Log("no spec slot, stack");
                    AddingItem(item, amount, availableSlot);
                    return true;
                }
            } else {
                int availableSlot = NextFreeSlot();
                if (availableSlot == -123) {
                    Debug.Log("Inventory full");
                    return false;
                } else {
                    Debug.Log("no spec slot, nostack");
                    AddingItem(item, amount, availableSlot);
                    return true;
                }
            }
        }
    }

    public void RemoveItem(int position, int amount = -2) {
        if (amount == -2) { //remove all items
            if (position >= currentInventorySize) {
                Debug.Log("Requested position larger than maximum inventory size");
            } else {
                items[position] = null;
                amountData[position] = 0;
                if (inventoryChanged != null) {
                    inventoryChanged.Invoke(position, null, 0);
                }
            }
        } else {
            if (position >= currentInventorySize) {
                Debug.Log("Requested position larger than maximum inventory size");
            } else {
                if (amountData[position] - amount <= 0) { //we need to remove all items
                    items[position] = null;
                    amountData[position] = 0;
                    if (inventoryChanged != null) {
                        inventoryChanged.Invoke(position, null, 0);
                    }

                } else {
                    amountData[position] -= amount;

                    if (inventoryChanged != null) {
                        inventoryChanged.Invoke(position, items[position], amountData[position]);
                    }
                }
            }
        }
    }

    public ItemObject[] GetInventory() {
        return items;
    }

    public int[] GetAmountData() {
        return amountData;
    }

    private int NextFreeSlot() {
        int i = 0;
        while (i < currentInventorySize && items[i] != null) {
            i++;
        }

        i = (i == currentInventorySize) ? -123 : i;

        return i;
    }

    int FindSlot(ItemObject item) { //find a slot which already has the specified item (with room for more amount), if not - return the next free slot
        int i = 0;
        int firstFreeSlot = -123;
        bool onlyOnce = false;

        whileStatement:
        while (i < currentInventorySize && items[i] != item) {
            if (onlyOnce == false) {
                if (items[i] == null) {
                    firstFreeSlot = i;
                    onlyOnce = true;
                }
            }
            i++;
        }
        if (i != currentInventorySize) {
            if (item.maxAmount <= amountData[i]) {
                i++;
                goto whileStatement;
            } else return i;
            
        } else return firstFreeSlot;
        
    }
}