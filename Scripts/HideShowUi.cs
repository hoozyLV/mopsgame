using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowUi {

    public void activateUI(bool i, CanvasGroup canvasGroup) {
        if (i == true) { //we show the ui
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        else { //we hide the ui
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
        }
       
    }
}
