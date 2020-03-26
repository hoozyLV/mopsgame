using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public GameObject inventoryUI;
    public Transform playerTransform;
    CanvasGroup canvasGroup;
    readonly HideShowUi hideShowUi = new HideShowUi();

    public GameObject itemPrefab;


    void Awake() {
        inventoryUI.SetActive(true);

        canvasGroup = inventoryUI.GetComponent<CanvasGroup>();

        hideShowUi.activateUI(false, canvasGroup);

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            hideShowUi.activateUI(canvasGroup.alpha == 1f && canvasGroup.blocksRaycasts == true ? false : true, canvasGroup);
        }
    }

    public void DropItem(ItemObject item, int amount = 1) {
        Debug.Log("Dropping item");
        GameObject obj = (GameObject) Instantiate(itemPrefab, new Vector3(playerTransform.position.x, playerTransform.transform.position.y, playerTransform.position.z), Quaternion.identity);

        obj.GetComponent<ItemPickup>().itemObject = item;
        obj.GetComponent<ItemPickup>().amount = amount;
        obj.GetComponent<ItemPickup>().allowPickup = false;

        obj.GetComponent<SpriteRenderer>().sprite = item.image;
    }
}
