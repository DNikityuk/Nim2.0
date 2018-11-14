
using UnityEngine;

public class buttonClick : MonoBehaviour {

    EducationController controller;
    // Use this for initialization
    void Start () {
        controller = GameObject.Find("educationField").GetComponent<EducationController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void whyButtonClick()
    {
        controller.clikOnWhyButton();
    }
    public void howButtonclick()
    {
        controller.clikOnHowButton();
    }
    public void continueButtonClick()
    {
        controller.clikOnContinueButton();
    }
    public void checkButtonClick()
    {
        controller.clikOnCheckButton();
    }
    public void yesButtonClick()
    {
        controller.clikOnYesButton();
    }
    public void noButtonClick()
    {
        controller.clikOnNoButton();
    }

    public void skipButtonClick()
    {
        //controller.clickOnSkip();
    }
}
