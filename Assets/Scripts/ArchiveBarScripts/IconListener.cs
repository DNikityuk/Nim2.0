using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconListener : MonoBehaviour {

    GameObject rockPrefab;
    float startX = -5f;
    float startY = 3.28f;
    private List<GameObject> rockList;
    public GameObject background;
    public GameObject arrow;
    private int[] state;
    private int takeRock, takeHeap;
    private int step = 0;      
    public int numberInList = 0;
    float arrowX = -6.24201f;
    float arrowY = 3.6f;

    // Use this for initialization
    void Start() {
        rockList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {

    }
    void OnMouseOver() {
        if (background == null) {
            background = (GameObject)Instantiate(Resources.Load("backgroundArchive"));
            arrow = (GameObject)Instantiate(Resources.Load("arrowPrevTurn"), 
                new Vector3(arrowX, arrowY - (numberInList * 1.43f), -1.0f), 
                Quaternion.identity);
            showStep();
        }
    }
    void OnMouseExit() {
        destroyAll();
    }

    public void showStep() {
        Quaternion spawnRotation = Quaternion.identity;

        for (int i = 0; i < state.Length; i++) {
            if (takeHeap == i && state[i] == 0) {
                for (int t = state[i]; t < (state[i] + takeRock); t++) {
                    float rockWidth = ((GameObject)Resources.Load("rockState")).GetComponent<Renderer>().bounds.extents.x;
                    float x = (startX + t * (2 * rockWidth + rockWidth / 2));
                    float y = (startY - i * (1.66f));
                    Vector3 spawnPosition = new Vector3(x, y, 0.0f);
                    rockPrefab = (GameObject)Instantiate(Resources.Load("rockState"), spawnPosition, spawnRotation);
                    rockPrefab.GetComponent<Renderer>().material.color = Color.red;
                    rockList.Add(rockPrefab);
                }
            }
            for (int j = 0; j < state[i]; j++) {
                float rockWidth = ((GameObject)Resources.Load("rockState")).GetComponent<Renderer>().bounds.extents.x;
                float x = (startX + j * (2 * rockWidth + rockWidth / 2));
                float y = (startY - i * (1.66f));
                Vector3 spawnPosition = new Vector3(x, y, 0.0f);
                rockPrefab = (GameObject)Instantiate(Resources.Load("rockState"), spawnPosition, spawnRotation);
                rockList.Add(rockPrefab);

                if (takeHeap == i && state[i] == j + 1) {
                    for (int t = state[i]; t < (state[i] + takeRock); t++) {
                        rockWidth = ((GameObject)Resources.Load("rockState")).GetComponent<Renderer>().bounds.extents.x;
                        x = (startX + t * (2 * rockWidth + rockWidth / 2));
                        y = (startY - i * (1.66f));
                        spawnPosition = new Vector3(x, y, 0.0f);
                        rockPrefab = (GameObject)Instantiate(Resources.Load("rockState"), spawnPosition, spawnRotation);
                        rockPrefab.GetComponent<Renderer>().material.color = Color.red;
                        rockList.Add(rockPrefab);
                    }
                }
            }
        }
    }

    private void destroyAll() {
        foreach (GameObject r in rockList)
            Destroy(r);
        rockList.Clear();
        Destroy(background);
        Destroy(arrow);
    }

    public void setStep(int step) {
        this.step = step;
    }
    public void setState(int[] arh) {
        this.state = arh;
    }
    public void setTakeRock(int take) {
        this.takeRock = take;
    }
    public void setTakeHeah(int numHeap) {
        this.takeHeap = numHeap;
    }

    public void setNumber(int num) {
        numberInList = num;
    }
}
