using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemDropCatcher : MonoBehaviour, IPointerClickHandler {

    InventoryManager inventoryManager;
    MovableItemDrag movableItemDrag;
    public void Awake() {
        inventoryManager = GameObject.Find("Scene Manager").GetComponent<InventoryManager>();
        movableItemDrag = GameObject.Find("movableItem").GetComponent<MovableItemDrag>();
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("drop item on ground");
        inventoryManager.DropItem(movableItemDrag.movableItemObject, movableItemDrag.amount);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (movableItemDrag.dragItem == true) {
            if (eventData.button == PointerEventData.InputButton.Left) {

                inventoryManager.DropItem(movableItemDrag.movableItemObject, movableItemDrag.amount);
                movableItemDrag.DragItem(false);

            } else if (eventData.button == PointerEventData.InputButton.Right) {
                if (movableItemDrag.movableItemObject.stackable) {
                    if (movableItemDrag.amount == 1) {
                        inventoryManager.DropItem(movableItemDrag.movableItemObject, movableItemDrag.amount);
                        movableItemDrag.DragItem(false);
                    } else {
                        inventoryManager.DropItem(movableItemDrag.movableItemObject, 1);
                        movableItemDrag.DragItem(true, movableItemDrag.movableItemObject, movableItemDrag.amount - 1);
                    }
                } else {
                    inventoryManager.DropItem(movableItemDrag.movableItemObject, movableItemDrag.amount);
                    movableItemDrag.DragItem(false);
                }
            }
        }
    }


}
