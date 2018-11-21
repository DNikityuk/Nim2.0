using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class Note : MonoBehaviour {

    public InputField[] rockCount;
    public InputField[] difference;
    int moveFlag = 0;
    Vector3 openPosition = new Vector3(512f, 288f, 0.0f);
    Vector3 closePosition = new Vector3(512f, -208f, 0.0f);

    void Update() {
        switch (moveFlag) {
            case 1:
                transform.position = Vector3.MoveTowards(transform.position,
                openPosition,
                Time.deltaTime * 2000);
                if (Vector3.Distance(transform.position, openPosition) < 0.01f) {
                    moveFlag = 0;
                }
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position,
                closePosition,
                Time.deltaTime * 2000);
                if (Vector3.Distance(transform.position, openPosition) < 0.01f) {
                    moveFlag = 0;
                }
                break;
        }
    }

    public void changeField(InputField field) {
        field.text = Regex.Replace(field.text, @"[^0-1 ]", "");
    }

    public void changeFieldEnd(InputField field) {
        while(field.text.Length < 3) {
            field.text = "0" + field.text;
        }
        getXOR();
    }

    void getXOR() {
        int xorBuf = 0;
        for (int i = 0; i < 5; i ++) {
            xorBuf ^= Convert.ToByte(rockCount[i].text, 2);
        }
        string xorStr = Convert.ToString(xorBuf, 2);
        while (xorStr.Length < 3) {
            xorStr = "0" + xorStr;
        }
        rockCount[5].text = difference[0].text = xorStr;
        getDifference();
    }

    public void changeFieldDifferenceEnd(InputField field) {
        while (field.text.Length < 3) {
            field.text = "0" + field.text;
        }
        getDifference();
    }

    void getDifference() {
        int differenceBuf = 0;
        for (int i = 0; i < 2; i ++) {
            differenceBuf ^= Convert.ToByte(difference[i].text, 2);
        }

        string differenceStr = Convert.ToString(differenceBuf, 2);
        while (differenceStr.Length < 3) {
            differenceStr = "0" + differenceStr;
        }
        difference[2].text = differenceStr;
    }

    public void clearFields() {
        for(int i = 0; i < rockCount.Length; i++) {
            rockCount[i].text = "000";
        }
        for (int i = 0; i < difference.Length; i++) {
            difference[i].text = "000";
        }
    }

    public void open() {
        GameObject.Find("gameField").GetComponent<Controller>().enabledGameButtons(false);
        moveFlag = 1;
    }

    public void hide() {
        GameObject.Find("gameField").GetComponent<Controller>().enabledGameButtons(true);
        moveFlag = 2;
    }


}
