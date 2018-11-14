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
        GameObject.Find("MenuCanvas").GetComponent<MenuView>().enableLoginMenu(false);
        Destroy(GameObject.Find("menuButtons(Clone)"));
        Instantiate(Resources.Load("gameProperties"), new Vector3(0.0f, -48.0f, 0.0f), Quaternion.identity);
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
