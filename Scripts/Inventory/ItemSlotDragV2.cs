using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemSlotDragV2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {

    ItemSlot itemSlotScript;
    MovableItemDrag movableItemDragScript;

    void Awake() {
        itemSlotScript = GetComponent<ItemSlot>();
        movableItemDragScript = GameObject.Find("movableItem").GetComponent<MovableItemDrag>();
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            switch (Inventory.instance.GetInventory()[itemSlotScript.index] != null) {
                
                case true when !movableItemDragScript.dragItem:
                    //1 amount to mouse

                    if (Inventory.instance.GetAmountData()[itemSlotScript.index] == 1) {
                        movableItemDragScript.DragItem(true, itemSlotScript.myItemObject, 1);
                        Inventory.instance.RemoveItem(itemSlotScript.index);
                    } else {
                        Inventory.instance.RemoveItem(itemSlotScript.index, 1);
                        movableItemDragScript.DragItem(true, itemSlotScript.myItemObject, movableItemDragScript.amount + 1);
                    }
                    break;

                case false when movableItemDragScript.dragItem:
                    //1 amount to slot

                    if (movableItemDragScript.amount == 1) {
                        Inventory.instance.AddingItem(movableItemDragScript.movableItemObject, 1, itemSlotScript.index, true);
                        movableItemDragScript.DragItem(false);
                    } else {
                        movableItemDragScript.DragItem(true, movableItemDragScript.movableItemObject, movableItemDragScript.amount - 1);
                        Inventory.instance.AddingItem(movableItemDragScript.movableItemObject, 1, itemSlotScript.index, true);
                    }
                    break;

                case true when movableItemDragScript.dragItem:
                    //switch items
                    Debug.Log("create a stacking situation for this");
                    ItemObject temporaryItem = itemSlotScript.myItemObject;
                    int temporaryAmount = itemSlotScript.myAmount;

                    Inventory.instance.RemoveItem(itemSlotScript.index);
                    Inventory.instance.AddingItem(movableItemDragScript.movableItemObject, movableItemDragScript.amount, itemSlotScript.index, true);

                    movableItemDragScript.DragItem(true, temporaryItem, temporaryAmount);

                    break;
            }

        
        }

        if (eventData.button == PointerEventData.InputButton.Left) {

            switch(Inventory.instance.GetInventory()[itemSlotScript.index] != null) {
                case true when !movableItemDragScript.dragItem:
                    //to mouse      
                    movableItemDragScript.DragItem(true,itemSlotScript.myItemObject, itemSlotScript.myAmount);
                    Inventory.instance.RemoveItem(itemSlotScript.index);
                    break;

                case false when movableItemDragScript.dragItem:
                    //add dragged item to inv
                    Inventory.instance.AddingItem(movableItemDragScript.movableItemObject, movableItemDragScript.amount, itemSlotScript.index, true);
                    movableItemDragScript.DragItem(false);
                    break;

                case true when movableItemDragScript.dragItem:
                    //switch items

                    ItemObject movItem = movableItemDragScript.movableItemObject;
                    ItemObject invItem = Inventory.instance.GetInventory()[itemSlotScript.index];

                    //if they are different items
                    if (movItem != invItem) {
                        ItemObject temporaryItem = itemSlotScript.myItemObject;
                        int temporaryAmount = itemSlotScript.myAmount;

                        Inventory.instance.RemoveItem(itemSlotScript.index);
                        Inventory.instance.AddingItem(movableItemDragScript.movableItemObject, movableItemDragScript.amount, itemSlotScript.index, true);

                        movableItemDragScript.DragItem(true, temporaryItem, temporaryAmount);
                    //same stackable item
                    } else if(movItem.stackable){
                        if (Inventory.instance.AddingItem(movItem, movableItemDragScript.amount, itemSlotScript.index, true)) {
                            movableItemDragScript.DragItem(false);
                        } else {
                            ItemObject temporaryItem = itemSlotScript.myItemObject;
                            int temporaryAmount = itemSlotScript.myAmount;

                            Inventory.instance.RemoveItem(itemSlotScript.index);
                            Inventory.instance.AddingItem(movableItemDragScript.movableItemObject, movableItemDragScript.amount, itemSlotScript.index, true);

                            movableItemDragScript.DragItem(true, temporaryItem, temporaryAmount);
                        }
                    }
                    break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        itemSlotScript.HoveringOver();
    }
}
