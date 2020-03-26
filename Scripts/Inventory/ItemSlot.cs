using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemSlot : MonoBehaviour {

    public int index;
    //ItemObject[] items;
    GameObject imageObject;

    HideShowUi hideShowUi = new HideShowUi();

    TextMeshProUGUI textMesh;
    public ItemObject myItemObject;
    CanvasGroup canvasGroupText;

    public int myAmount;

    void Awake() {
        Inventory.inventoryChanged += inventoryChanged;
        imageObject = gameObject.transform.Find("Image").gameObject;

        textMesh = this.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        canvasGroupText = this.gameObject.transform.Find("TextBackground").GetComponent<CanvasGroup>();

        hideShowUi.activateUI(false, imageObject.GetComponent<CanvasGroup>());

    }

    void inventoryChanged(int invIndex, ItemObject itemObject, int newAmount) {
        //we ask if the changed index in the inv array is our corresponding index of inv item slot, then we update the item slot ui if it matches
        if (invIndex == index) {

            Debug.Log("invChanged for slot " + invIndex);

            if (itemObject != null) {
                if (itemObject.stackable == false) { //unstackable
                    myItemObject = itemObject;
                    myAmount = newAmount;
                    hideShowUi.activateUI(true, imageObject.GetComponent<CanvasGroup>());
                    imageObject.GetComponent<Image>().sprite = myItemObject.image;
                    textMesh.text = "";
                    canvasGroupText.alpha = 0f;

                } else { //stackable
                    myItemObject = itemObject;
                    myAmount = newAmount;
                    hideShowUi.activateUI(true, imageObject.GetComponent<CanvasGroup>());
                    imageObject.GetComponent<Image>().sprite = myItemObject.image;
                    textMesh.text = myAmount.ToString();
                    canvasGroupText.alpha = 1f;
                    
                    
                }
            }
            else {
                myItemObject = null;
                myAmount = 0;
                imageObject.GetComponent<Image>().sprite = null;
                hideShowUi.activateUI(false, imageObject.GetComponent<CanvasGroup>());
                textMesh.text = "";
                canvasGroupText.alpha = 0f;
            }

        }

    }

    public void HoveringOver() {
        if (myItemObject) {
            //Debug.Log("display item info");
        }
    }
}
