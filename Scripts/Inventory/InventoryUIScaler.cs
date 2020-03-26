using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIScaler : MonoBehaviour {

    //This script creates and puts inventory cells in inventory UI, scales the UI according to the declared column count and the delcared inv size in Player script

    public GameObject player;
    public GameObject invSlotPrefab;
    public int columns; //the number of max columns

    private void Awake() {
        int invSize = player.GetComponent<Inventory>().currentInventorySize;
        Vector2 size = new Vector2((float)columns * 110 + 40, Mathf.Ceil(invSize / columns * 110 + 40));

        Transform itemParent = gameObject.transform.Find("itemParent");

        gameObject.GetComponent<RectTransform>().sizeDelta = size;
        itemParent.GetComponent<RectTransform>().sizeDelta = size;

        for (int i = 0; i < invSize; i++) { //create the item slots
            GameObject slot = Instantiate(invSlotPrefab);
            slot.transform.SetParent(itemParent);
            slot.transform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            slot.GetComponent<ItemSlot>().index = i;

        }

        Debug.Log("Inventory created");
    }


}
