using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    protected GameObject rockPrefab;
    protected onRockListner rocListener;
    float startX = -5.0f;
    float startY = 3.28f;
    int rockNumber;

    public Rock() { 
    }

    public Rock(int rockNum, int numHeap) {
        rockNumber = rockNum;
        showRock(numHeap);
        rocListener = rockPrefab.GetComponent<onRockListner>();
    }

    void showRock(int numHeap) {
        float rockWidth = ((GameObject)Resources.Load("rock")).GetComponent<Renderer>().bounds.extents.x;
        float x = (startX + rockNumber * (2 * rockWidth + rockWidth / 2));
        float y = (startY - numHeap * (1.66f));
        Vector3 spawnPosition = new Vector3(x, y, 0.0f);
        Quaternion spawnRotation = Quaternion.identity;
        rockPrefab = (GameObject)Instantiate(Resources.Load("rock"), spawnPosition, spawnRotation);
    }
    

    public bool checkSelect() {
        if (rocListener.getIsSelected() == 1) {
            return true;
        }
        return false;
    }

    public void setSelected() {
        rocListener.setSelected();
    }

    public void resetSelected() {
        rocListener.resetSelected();
    }

    public void setMouseDown() {
        rocListener.setMouseDown();
    }

    public void resetMouseDown() {
        rocListener.resetMouseDown();
    }

    public void deleteSelected() {
        rocListener.setReadyForDeletePlayer();
    }

    public void deleteComp() {
        rocListener.deleteComp();
    }

    public void setCanBeSelected(bool can) {
        rocListener.setCanBeSelected(can);
    }

    public bool getSelectRequest() {
        return rocListener.getSelectRequest();
    }

    public void setSelectRequest(bool val) {
        rocListener.setSelectRequest(val);
    }
}
