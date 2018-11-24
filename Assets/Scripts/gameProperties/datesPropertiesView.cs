using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class datesPropertiesView : MonoBehaviour {

    public Dropdown firstTurn;
    public Dropdown gameComplexity;
    public Text startDayText;
    public Text startMonthText;
    public Text finalDayText;
    public Text finalMonthText;
    static int startDay;
    static int startMonth;
    static int finalDay;
    static int finalMonth;
    static int gameLevel;
    static int firstPlayer;
    private int[] numDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    void Start() {
        startDay = 10;
        startMonth = 8;
        finalDay = 31;
        finalMonth = 12;
    }
	
    private int getNumDays(int month) {
        return numDays[month - 1];
    }

    public void startGameClick() {
        if (startDay > finalDay)
            return;
        if (finalMonth < startMonth)
            return;
        if (finalMonth == startMonth && finalDay <= startDay) {
            return;
        }
        firstPlayer = firstTurn.value;
        if (firstPlayer == 2) {
            firstPlayer = new System.Random().Next(0, 2);
        }
        gameLevel = gameComplexity.value;
        SceneManager.LoadScene("DatesGame");
    }

    public void addStartDay() {
        if(startDay < getNumDays(startMonth)) {
            startDay++;
            updateCounter(startDayText, startDay);
        }
    }

    public void deleteStartDay() {
        if (startDay != 1) {
            startDay--;
            updateCounter(startDayText, startDay);
        }
    }

    public void addStartMonth() {
        if (startMonth != 12) {
            startMonth++;
            updateCounter(startMonthText, startMonth);
        }
        if (startDay > getNumDays(startMonth)) {
            startDay = getNumDays(startMonth);
            updateCounter(startDayText, startDay);
        }
    }

    public void deleteStartMonth() {
        if (startMonth != 1) {
            startMonth--;
            updateCounter(startMonthText, startMonth);
        }
        if (startDay > getNumDays(startMonth)) {
            startDay = getNumDays(startMonth);
            updateCounter(startDayText, startDay);
        }
    }

    public void addFinalDay() {
        if (finalDay < getNumDays(finalMonth)) {
            finalDay++;
            updateCounter(finalDayText, finalDay);
        }
    }

    public void deleteFinalDay() {
        if (finalDay != 1) {
            finalDay--;
            updateCounter(finalDayText, finalDay);
        }
    }

    public void addFinalMonth() {
        if (finalMonth != 12) {
            finalMonth++;
            updateCounter(finalMonthText, finalMonth);
        }
        if (finalDay > getNumDays(finalMonth)) {
            finalDay = getNumDays(finalMonth);
            updateCounter(finalDayText, finalDay);
        }
    }

    public void deleteFinalMonth() {
        if (finalMonth != 1) {
            finalMonth--;
            updateCounter(finalMonthText, finalMonth);
        }
        if (finalDay > getNumDays(finalMonth)) {
            finalDay = getNumDays(finalMonth);
            updateCounter(finalDayText, finalDay);
        }
    }

    private void updateCounter(Text label, int value) {
        if(value < 10) 
            label.text = "0" + value;    
        else
            label.text = "" + value;
    }

    public void closeGameProperties() {
        MenuView menuCanvas = GameObject.Find("MenuCanvas").GetComponent<MenuView>();
        menuCanvas.setActiveChangeButton(true);
        menuCanvas.enableLoginMenu(true);
        Instantiate(Resources.Load("menuButtons"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Destroy(GameObject.Find("datesProperties(Clone)"));
    }

    public static int getGameLevel() {
        return gameLevel;
    }

    public static int getFirstTurn() {
        return firstPlayer;
    }

    public static int getStartDay() {
        return startDay;
    }

    public static int getStartMonth() {
        return startMonth;
    }

    public static int getFinalDay() {
        return finalDay;
    }

    public static int getFinalMonth() {
        return finalMonth;
    }
}
