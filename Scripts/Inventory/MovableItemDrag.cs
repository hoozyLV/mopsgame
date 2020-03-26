using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MovableItemDrag : MonoBehaviour {

    CanvasGroup canvasGroup;

    //[HideInInspector]
    public ItemObject movableItemObject; 
    public int amount;
    public bool dragItem = false;

    CanvasGroup textBGcanvasGroup;
    TextMeshProUGUI textMesh;

    Image image;

    private void Awake() {

        canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;

        image = this.GetComponent<Image>();

        textBGcanvasGroup = this.gameObject.transform.Find("TextBackground").GetComponent<CanvasGroup>();
        textMesh = this.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    }

    void Update() {
        if (dragItem) {
            transform.position = Input.mousePosition;
            
        }
    }

    public void DragItem(bool shouldDrag,ItemObject item = null, int c_amount = 0) {
        if (shouldDrag) {
            movableItemObject = item;
            amount = c_amount;
            dragItem = true;
            canvasGroup.alpha = 1f;
            image.sprite = item.image;

            if (item.stackable) {
                textMesh.text = c_amount.ToString();
                textBGcanvasGroup.alpha = 1f;
            } else {
                textMesh.text = "";
                textBGcanvasGroup.alpha = 0f;

            }

        } else {
            movableItemObject = null;
            amount = 0;
            dragItem = false;
            canvasGroup.alpha = 0f;
            image.sprite = null;
        }
    }




}
