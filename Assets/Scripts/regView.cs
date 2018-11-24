using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class regView : MonoBehaviour {

    public InputField passwordField;
    public InputField loginField;
    Registration registration;

    void Start() {
        registration = new Registration();
        passwordField.contentType = InputField.ContentType.Password;
    }

    public void closeWindow() {
        GameObject.Find("menuButtons(Clone)").GetComponent<Canvas>().enabled = true;
        GameObject.Find("MenuCanvas").GetComponent<MenuView>().enableLoginMenu(true);
        GameObject.Find("MenuCanvas").GetComponent<MenuView>().setActiveChangeButton(true);
        Destroy(GameObject.Find("registration(Clone)"));
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

    public void createProfile() {
        string login = (loginField.text).Trim().ToLower();
        string password = passwordField.text.ToLower();
        if (password != "" && login != "") {
            string result = registration.registerUser(login, password);

            switch(result) {
                case "create":
                    clearField();
                    closeWindow();
                    GameObject.Find("MenuCanvas").GetComponent<MenuView>().enableLoginMenu(true);
                    break;
                case "exists":
                    GameObject info = (GameObject)Instantiate(Resources.Load("InfoWindow"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                    info.GetComponent<InfoWindowView>().setWhoCreated(1);
                    info.GetComponent<InfoWindowView>().setMessage("Логин занят");
                    break;
                case "serverError":
                    Instantiate(Resources.Load("ServerError"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                    break;
            }
        }
        else {
            GameObject info = (GameObject)Instantiate(Resources.Load("InfoWindow"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            info.GetComponent<InfoWindowView>().setWhoCreated(1);
            info.GetComponent<InfoWindowView>().setMessage("Заполните поля");
        }
    }

    public void setEnabledRegButton(bool state) {
        GameObject.Find("registration(Clone)/registrationButton").GetComponent<Button>().enabled = state;
        GameObject.Find("registration(Clone)/closeButton").GetComponent<Button>().enabled = state;
    }
}
