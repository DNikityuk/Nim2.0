using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class originalNimPropertiesView : MonoBehaviour {

    public Dropdown firstTurn;
    public Dropdown gameComplexity;
    public Text numOfHeapText;
    static int numberOfHeap;
    static int gameLevel;
    static int firstPlayer;

	void Start() {
		numberOfHeap = 2;
	}
	
    public void startGameClick() {
        firstPlayer = firstTurn.value;
        if(firstPlayer == 2) {
            firstPlayer = new System.Random().Next(0, 2);
        }
        gameLevel = gameComplexity.value;
        SceneManager.LoadScene("Game");
    }

    public void addHeap() {
        if(numberOfHeap < 5) {
            numberOfHeap++;
            updateHeapCount();
        }
    }

    public void deleteHeap() {
        if (numberOfHeap > 2) {
            numberOfHeap--;
            updateHeapCount();
        }
    }

    private void updateHeapCount() {
        numOfHeapText.text = "0" + numberOfHeap;        
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
        Destroy(GameObject.Find("originalNimProperties(Clone)"));
    }

    public static int getFirstTurn() {
        return firstPlayer;
    }       
}
