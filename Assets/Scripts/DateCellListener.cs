using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateCellListener : MonoBehaviour {
    private bool isSelected = false;
    private bool canSelected = true;

    public void onClick() {
        if (canSelected) 
            isSelected = true;
    }

    public bool getSelected() {
        return isSelected;
    }

    public void setSelected(bool val) {
        isSelected = val;
    }

    public void setCanSelected(bool val) {
        canSelected = val;
    }

    public void setCurrentCell(bool val) {
        ColorBlock cb = GetComponent<Button>().colors;
        if (val) {
            cb.disabledColor = new Color32(86, 92, 255, 255);
            GetComponent<Button>().GetComponentInChildren<Text>().color = new Color32(200, 121, 56, 255);
        }
        else {
            cb.disabledColor = new Color32(190, 190, 190, 255);
            GetComponent<Button>().GetComponentInChildren<Text>().color = new Color32(70, 20, 9, 255);
        }
        GetComponent<Button>().colors = cb;
    }

    public void setFinalCell(bool val) {
        ColorBlock cb = GetComponent<Button>().colors;
        if (val) {
            cb.disabledColor = new Color32(231, 75, 82, 255);
            cb.normalColor = new Color32(255, 140, 147, 255);
            cb.highlightedColor = new Color32(245, 130, 137, 255);
            cb.pressedColor = new Color32(200, 85, 92, 255);
        }
        else {
            cb.disabledColor = new Color32(190, 190, 190, 255);
            cb.normalColor = new Color32(255, 255, 255, 255); 
            cb.highlightedColor = new Color32(245, 245, 245, 255); 
            cb.pressedColor = new Color32(200, 200, 200, 255); 
        }
        GetComponent<Button>().colors = cb;
    }
}
