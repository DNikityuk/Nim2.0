using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class authView : MonoBehaviour {

    public InputField passwordField;
    public InputField loginField;
    Authorization authorization;

    void Start() {
        passwordField.contentType = InputField.ContentType.Password;
        authorization = new Authorization();
    }

    public void closeWindow() {
        GameObject.Find("menuButtons(Clone)").GetComponent<Canvas>().enabled = true;
        GameObject.Find("MenuCanvas").GetComponent<MenuView>().enableLoginMenu(true);
        Destroy(GameObject.Find("authorization(Clone)"));
    }

    public void clearField() {
        loginField.text = "";
        passwordField.text = "";
    }

    public void filterLogin() {
        loginField.text = Regex.Replace(loginField.text, @"[^а-яА-Яa-zA-Z0-9 ]", "");
    }

    public void filterPassword() {
        passwordField.text = Regex.Replace(passwordField.text, @"[^а-яА-Яa-zA-Z0-9 ]", "");
    }

    public void authUser() {
        string login = (loginField.text).Trim().ToLower();
        string password = passwordField.text.ToLower();
        if (password != "" && login != "") {
            //setEnabledAuthButton(false);
            int result = authorization.authorizedUser(login, password);
            Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
            Quaternion spawnRotation = Quaternion.identity;
            switch (result) {
                case -1:
                    Instantiate(Resources.Load("ServerError"), spawnPosition, spawnRotation);
                    break;
                case 1:
                    clearField();
                    closeWindow();
                    GameObject.Find("statistic").GetComponent<Button>().interactable = true;
                    GameObject.Find("MenuCanvas").GetComponent<MenuView>().enableLoginMenu(false);
                    break;
                case 0:
                    GameObject info = (GameObject)Instantiate(Resources.Load("InfoWindow"), spawnPosition, spawnRotation);
                    info.GetComponent<InfoWindowView>().setWhoCreated(0);
                    info.GetComponent<InfoWindowView>().setMessage("Неверный логин или пароль");
                    break;
            }
        }
        else {
            GameObject info = (GameObject)Instantiate(Resources.Load("InfoWindow"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            info.GetComponent<InfoWindowView>().setWhoCreated(0);
            info.GetComponent<InfoWindowView>().setMessage("Заполните поля");
        }
    }

    public void setEnabledAuthButton(bool state) {
        GameObject.Find("authorization(Clone)/authorizateButton").GetComponent<Button>().enabled = state;
        GameObject.Find("authorization(Clone)/closeButton").GetComponent<Button>().enabled = state;
    }
}
