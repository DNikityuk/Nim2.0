using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : MonoBehaviour {

    Controller control;
    ConstructedController constControl;
    DatesController datesController;
    public Image[] rulesImage;
    public Button continueButton;
    public Button mainMenuButton;
    public Button rulesButton;

    public void rules() {
        control = GameObject.Find("gameField").GetComponent<Controller>();
        constControl = GameObject.Find("gameField").GetComponent<ConstructedController>();
        datesController = GameObject.Find("gameField").GetComponent<DatesController>();
        mainMenuButton.gameObject.SetActive(false);
        rulesButton.gameObject.SetActive(false);
        float x = continueButton.transform.position.x;
        continueButton.transform.position = new Vector3 (x, 163, 0);
        if (control == null && datesController == null) {
            rulesImage[0].gameObject.SetActive(true);
        }
        else {
            if (datesController == null)
                rulesImage[2].gameObject.SetActive(true);
            else
                rulesImage[1].gameObject.SetActive(true);
        }
    }

    public void backToMenu() {
        control = GameObject.Find("gameField").GetComponent<Controller>();
        constControl = GameObject.Find("gameField").GetComponent<ConstructedController>();
        datesController = GameObject.Find("gameField").GetComponent<DatesController>();
        if (control == null && datesController == null) {
            MenuView.setGameNum(constControl.getGameNum());
        }
        else {
            if (datesController == null)
                MenuView.setGameNum(control.getGameNum());
            else
                MenuView.setGameNum(datesController.getGameNum());
        }
        SceneManager.LoadScene("MenuWindow");
    }

    public virtual void backToGame() {
        control = GameObject.Find("gameField").GetComponent<Controller>();
        constControl = GameObject.Find("gameField").GetComponent<ConstructedController>();
        datesController = GameObject.Find("gameField").GetComponent<DatesController>();
        if (control == null && datesController == null) {
            continueGameConstruct();
        }
        else 
        {
            if (datesController == null)
                continueGame();
            else
                continueGameDates();
        }
    }


    public virtual void continueGame() {
        if (GameObject.Find("gameField") == null)
            control = GameObject.Find("educationField").GetComponent<EducationController>();
        else
        {
            control = GameObject.Find("gameField").GetComponent<Controller>();
            control.startTimer();
        }
        control.unblockAllHeaps();
        control.setPause(false);        
        Destroy(GameObject.Find("PauseView(Clone)"));
    }

    public virtual void continueGameConstruct() {
        if (GameObject.Find("gameField") == null)
            ; //constControl = GameObject.Find("educationField").GetComponent<EducationController>();
        else {
            constControl = GameObject.Find("gameField").GetComponent<ConstructedController>();
            constControl.startTimer();
        }
        constControl.unblockAllHeaps();
        constControl.setPause(false);
        Destroy(GameObject.Find("PauseView(Clone)"));
    }

    public virtual void continueGameDates() {
        if (GameObject.Find("gameField") == null)
            ; //constControl = GameObject.Find("educationField").GetComponent<EducationController>();
        else {
            datesController = GameObject.Find("gameField").GetComponent<DatesController>();
            datesController.startTimer();
        }

        datesController.setEnabledCells(true);
        datesController.setPause(false);
        Destroy(GameObject.Find("PauseView(Clone)"));
    }

}
