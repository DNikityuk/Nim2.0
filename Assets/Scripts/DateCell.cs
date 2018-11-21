using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DateCell : MonoBehaviour {
    private int firstX = -300;
    private int firstY = 107;
    private GameObject dateCell;
    
    public DateCell(int i) {
        showDateCell(i);

        dateCell.GetComponentInChildren<Text>().text = "" + (i - 1);
    }

    void showDateCell(int posCell) {
        int x = firstX + (posCell % 7) * 100;
        int num = 0;
        while (true) {
            posCell -= 7;
            if (posCell >= 0) {
                num++;
            }
            else {
                break;
            }
        }
        int y = firstY - num * 80;
        Vector3 spawnPosition = new Vector3(x, y, 0.0f);
        Quaternion spawnRotation = Quaternion.identity;
        dateCell = Instantiate(Resources.Load("dateCellButton"), spawnPosition, spawnRotation) as GameObject;
        dateCell.transform.SetParent(GameObject.Find("Canvas").GetComponent<Canvas>().transform, false);
    }

    public void setActiveCell(bool val) {
        dateCell.SetActive(val);
    }

    public void setInteractable(bool val) {
        dateCell.GetComponent<Button>().interactable = val;
    }

    public bool getIsSelected() {
        return dateCell.GetComponent<DateCellListener>().getSelected();
    }

    public void setIsSelected(bool val) {
        dateCell.GetComponent<DateCellListener>().setSelected(val);
    }

    public void setCanSelected(bool val) {
        dateCell.GetComponent<DateCellListener>().setCanSelected(val);
    }

    public void setCurrentCell(bool val) {
        dateCell.GetComponent<DateCellListener>().setCurrentCell(val);
    }
    
    public void setFinalCell(bool val) {
        dateCell.GetComponent<DateCellListener>().setFinalCell(val);
    }
}
