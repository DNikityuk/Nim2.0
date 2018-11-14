using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindowView : MonoBehaviour {

    int parent = -1;

    public void closeWindow() {
        if(parent == 0) {
            GameObject.Find("authorization(Clone)").GetComponent<authView>().setEnabledAuthButton(true);
        }
        else {
            GameObject.Find("registration(Clone)").GetComponent<regView>().setEnabledRegButton(true);
        }
        Destroy(GameObject.Find("InfoWindow(Clone)"));
        //GetComponent<Canvas>().enabled = false;
    }

    public void setMessage(string textMessage) {
        GameObject.Find("InfoWindow(Clone)/MessageText").GetComponent<Text>().text = textMessage;
    }

    public void setWhoCreated(int _parent) {
        parent = _parent;
        if (parent == 0) {
            GameObject.Find("authorization(Clone)").GetComponent<authView>().setEnabledAuthButton(false);
        }
        else {
            GameObject.Find("registration(Clone)").GetComponent<regView>().setEnabledRegButton(false);
        }
    }
    
}
