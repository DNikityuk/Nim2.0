using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerErrorView : MonoBehaviour {

	public void closeWindow() {
        //GameObject.Find("authorization(Clone)").GetComponent<authView>().setEnabledAuthButton(true);
        Destroy(GameObject.Find("ServerError(Clone)"));
		//GetComponent<Canvas>().enabled = false;
	}
}
