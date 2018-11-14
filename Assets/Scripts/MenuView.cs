using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuView : MonoBehaviour {

    public GameObject authButton;
    public GameObject regButton;

    void Start() {
        if (Authorization.isAuth()) {
            enableLoginMenu(false);
        }
        //Screen.SetResolution(1024, 576, false);
        Instantiate(Resources.Load("menuButtons"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }
	
	public void authButtonOver() {
		authButton.GetComponent<Transform>().Translate(0.0f, 5.0f, 0.0f);
    }

    public void regButtonOver() {
        regButton.GetComponent<Transform>().Translate(0.0f, 5.0f, 0.0f);
    }

    public void authButtonExit() {
        authButton.GetComponent<Transform>().Translate(0.0f, -5.0f, 0.0f);
    }

    public void regButtonExit() {
        regButton.GetComponent<Transform>().Translate(0.0f, -5.0f, 0.0f);
    }

    public void enableLoginMenu(bool value) {
        authButton.SetActive(value);
        regButton.SetActive(value);
    }

    public void authClick() {
        authButtonExit();
        GameObject.Find("menuButtons(Clone)").GetComponent<Canvas>().enabled = false;
        enableLoginMenu(false);
        Instantiate(Resources.Load("authorization"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }

    public void regClick() {
        regButtonExit();
        GameObject.Find("menuButtons(Clone)").GetComponent<Canvas>().enabled = false;
        enableLoginMenu(false);
        Instantiate(Resources.Load("registration"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }

}
