using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public ItemObject itemObject;

    public int amount;

    [HideInInspector]
    public bool allowPickup = true;

    private void Awake() {
        if (amount == 0) { //amount cannot be 0
            amount = 1;
        }

        if (!allowPickup) {
            Invoke("AllowPickupDelay", 5);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player" && allowPickup == true) {
            if (Inventory.instance.AddingItem(itemObject, amount)) {

                //Debug.Log("Picking up item " + "maxAmount: " + itemObject.maxAmount);
                Destroy(gameObject);
            }
        }
    }

    void AllowPickupDelay() {
        Debug.Log("test");
        allowPickup = true;
    }
}
