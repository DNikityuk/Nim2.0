using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuView : MonoBehaviour {

    public Sprite[] changeImages;
    public GameObject rightChangeGameButton;
    public GameObject leftChangeGameButton;
    public GameObject authButton;
    public GameObject regButton;
    public Sprite[] logo;
    public GameObject logoPlace;
    private static int gameNumber = 1;
    private int maxGameCount;

    void Start() {
        //Screen.SetResolution(1024, 576, false);
        maxGameCount = logo.Length;
        if (Authorization.isAuth()) {
            enableLoginMenu(false);
        }
        Instantiate(Resources.Load("menuButtons"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        setLogo();
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

    public void leftChangerOver() {
        leftChangeGameButton.GetComponent<Image>().sprite = changeImages[1];
    }

    public void rightChangerOver() {
        rightChangeGameButton.GetComponent<Image>().sprite = changeImages[3];
    }

    public void leftChangerExit() {
        leftChangeGameButton.GetComponent<Image>().sprite = changeImages[0];
    }

    public void rightChangerExit() {
        rightChangeGameButton.GetComponent<Image>().sprite = changeImages[2];
    }

    public void enableLoginMenu(bool value) {
        authButton.SetActive(value);
        regButton.SetActive(value);
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
        setLogo();
    }

    private void setLogo() {
        if (gameNumber == 0) {
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
        rightChangeGameButton.SetActive(value);
        leftChangeGameButton.SetActive(value);
    }

    public static void setGameNum(int game) {
        gameNumber = game;
    }

    void Awake() {
        #if UNITY_STANDALONE_WIN
        Screen.SetResolution(1024, 576, false);
        #endif
    }
}
