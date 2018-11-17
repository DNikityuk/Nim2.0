using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class constructedNimPropertiesView : MonoBehaviour {

    public Dropdown firstTurn;
    public Dropdown gameComplexity;
    public Text numOfHeapText;
    public Text ftLimitText;
    public Text scLimitText;
    static int ftLimit;
    static int scLimit;
    static int numberOfHeap;
    static int gameLevel;
    static int firstPlayer;

	void Start() {
        ftLimit = 2;
        scLimit = 4;
        numberOfHeap = 2;
	}
	
    public void startGameClick() {
        firstPlayer = firstTurn.value;
        if (firstPlayer == 2) {
            firstPlayer = new System.Random().Next(0, 2);
        }
        gameLevel = gameComplexity.value;
        SceneManager.LoadScene("ConstructedNimGame");
    }

    public void addHeap() {
        if(numberOfHeap < 5) {
            numberOfHeap++;
            updateCounter(numOfHeapText, numberOfHeap);
        }
    }

    public void deleteHeap() {
        if (numberOfHeap > 2) {
            numberOfHeap--;
            updateCounter(numOfHeapText, numberOfHeap);
        }
    }

    public void addFtLimit() {
        if (ftLimit < 5) {
            ftLimit++;
            updateCounter(ftLimitText, ftLimit);
        }
    }

    public void deleteFtLimit() {
        if (ftLimit > 2) {
            ftLimit--;
            updateCounter(ftLimitText, ftLimit);
        }
    }

    public void addScLimit() {
        if (scLimit < 5) {
            scLimit++;
            updateCounter(scLimitText, scLimit);
        }
    }

    public void deleteScLimit() {
        if (scLimit > 2) {
            scLimit--;
            updateCounter(scLimitText, scLimit);
        }
    }

    private void updateCounter(Text label, int value) {
        label.text = "0" + value;        
    }

    public static int getNumberOfHeap() {
        return numberOfHeap;
    }
    
    public static int getGameLevel() {
        return gameLevel;
    }

    public void closeGameProperties() {
        MenuView menuCanvas = GameObject.Find("MenuCanvas").GetComponent<MenuView>();
        menuCanvas.setActiveChangeButton(true);
        menuCanvas.enableLoginMenu(true);
        Instantiate(Resources.Load("menuButtons"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Destroy(GameObject.Find("constructedNimProperties(Clone)"));
    }

    public static int getFirstTurn() {
        return firstPlayer;
    }

    public static int getFtLimit() {
        return ftLimit;
    }

    public static int getScLimit() {
        return scLimit;
    }
}
