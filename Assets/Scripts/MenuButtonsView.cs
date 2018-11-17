using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsView : MonoBehaviour {

    void Start() {
        if (!Authorization.isAuth()) {
            GameObject.Find("statistic").GetComponent<Button>().interactable = false;
        }
    }

    public void ratingGameClick() {
        MenuView menuCanvas = GameObject.Find("MenuCanvas").GetComponent<MenuView>();
        int game = menuCanvas.getGameNumber();
        menuCanvas.enableLoginMenu(false);
        menuCanvas.setActiveChangeButton(false);
        Destroy(GameObject.Find("menuButtons(Clone)"));

        switch(game) {
            case 1:
                Instantiate(Resources.Load("originalNimProperties"), new Vector3(0.0f, -48.0f, 0.0f), Quaternion.identity);
                break;
            case 2:
                Instantiate(Resources.Load("constructedNimProperties"), new Vector3(0.0f, -48.0f, 0.0f), Quaternion.identity);
                break;
            case 3:
                break;
        }
    }

    public void exitClick() {
        Application.Quit();
    }

    public void trainingGameClick() {
        SceneManager.LoadScene("EducationGame");
    }

    public void statisticClick() {
        SceneManager.LoadScene("StatisticWindow");
    }

}
