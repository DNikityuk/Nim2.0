using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuView : MonoBehaviour {

    public GameObject rightChangeGameButton;
    public GameObject leftChangeGameButton;
    private GameObject rightChangerCanvas;
    private GameObject leftChangerCanvas;
    public GameObject authButton;
    public GameObject regButton;
    public Sprite[] logo;
    public GameObject logoPlace;
    private int gameNumber;
    private int maxGameCount;

    void Start() {
        gameNumber = 1;
        maxGameCount = logo.Length;
        rightChangerCanvas = GameObject.Find("RightChangerCanvas");
        leftChangerCanvas = GameObject.Find("LeftChangerCanvas"); ;
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

    public void rightChangeGameOver() {
        rightChangeGameButton.GetComponent<Transform>().Translate(10.0f, 0.0f, 0.0f);
    }

    public void rightChangeGameExit() {
        rightChangeGameButton.GetComponent<Transform>().Translate(-10.0f, 0.0f, 0.0f);
    }

    public void leftChangeGameOver() {
        leftChangeGameButton.GetComponent<Transform>().Translate(-10.0f, 0.0f, 0.0f);
    }

    public void leftChangeGameExit() {
        leftChangeGameButton.GetComponent<Transform>().Translate(10.0f, 0.0f, 0.0f);
    }

    public void authClick() {
        authButtonExit();
        GameObject.Find("menuButtons(Clone)").GetComponent<Canvas>().enabled = false;
        enableLoginMenu(false);
        setActiveChangeButton(false);
        Instantiate(Resources.Load("authorization"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }

    public void regClick() {
        regButtonExit();
        GameObject.Find("menuButtons(Clone)").GetComponent<Canvas>().enabled = false;
        enableLoginMenu(false);
        setActiveChangeButton(false);
        Instantiate(Resources.Load("registration"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }

    public void setGameNumber(int num) {
        gameNumber += num;
        if(gameNumber == 0) {
            gameNumber = maxGameCount;
        }
        if (gameNumber == maxGameCount + 1) {
            gameNumber = 1;
        }
        logoPlace.GetComponent<SpriteRenderer>().sprite = logo[gameNumber - 1];
    }

    public int getGameNumber() {
        return gameNumber;
    }

    public void setActiveChangeButton(bool value) {
        rightChangerCanvas.SetActive(value);
        leftChangerCanvas.SetActive(value);
    }

}
