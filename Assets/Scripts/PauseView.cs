using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : MonoBehaviour {

    Controller control;
    ConstructedController constControl;
    DatesController datesController;

    public void backToMenu() {
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
