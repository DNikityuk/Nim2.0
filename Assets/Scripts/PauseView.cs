using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : MonoBehaviour {

    Controller control;

	public void backToMenu() {
        SceneManager.LoadScene("MenuWindow");
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

}
