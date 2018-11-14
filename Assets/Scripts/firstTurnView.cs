using UnityEngine;
using System.Collections;

public class firstTurnView : MonoBehaviour {

    public Controller controller;

    void Start () {
        GetComponent<Canvas>().enabled = false;
    }

	public void yesClickButton() {
        controller.setStartPlayer(0);
        setEnabled(false);
    }

    public void noClickButton() {
        controller.setStartPlayer(1);
        setEnabled(false);
    }

    public void setEnabled(bool val) {
        GetComponent<Canvas>().enabled = val;
    }
}
