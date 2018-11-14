using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamePropertiesView : MonoBehaviour {

    public Dropdown gameName;
    public Dropdown gameComplexity;
    public Text numOfHeapText;
    static int numberOfHeap;
    static int gameLevel;
    static int game;

	void Start() {
		numberOfHeap = 2;
	}
	
    public void startGameClick() {
        game = gameName.value;
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
        GameObject.Find("MenuCanvas").GetComponent<MenuView>().enableLoginMenu(true);
        Instantiate(Resources.Load("menuButtons"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Destroy(GameObject.Find("gameProperties(Clone)"));
    }

    public static int getGameName() {
        return game;
    }       
}
