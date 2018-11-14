using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPlayer : MonoBehaviour {

    public int step = 0;
    GameObject iconPrefab;
    public int player = -1;

    void Start() {

    }

    void Update() {

    }

    public IconPlayer(ArhiveGame archive, int step, int player) {
        this.step = step;
        this.player = player;
        if (player == 0)
            iconPrefab = (GameObject)Instantiate(Resources.Load("userIcon"));
        else
            iconPrefab = (GameObject)Instantiate(Resources.Load("compIcon"));
        iconPrefab.GetComponent<IconListener>().setStep(step);
        iconPrefab.GetComponent<IconListener>().setState(archive.getState(step));
        iconPrefab.GetComponent<IconListener>().setTakeRock(archive.getNumberOfRock(step));
        iconPrefab.GetComponent<IconListener>().setTakeHeah(archive.getNumberOfHeap(step));
    }

    public IconPlayer(IconPlayer icon) {
        player = icon.getPlayer();
        step = icon.getStep();
        iconPrefab = icon.getIconPrefab();
    }


    public GameObject getIconPrefab() {
        return iconPrefab;
    }
    public int getPlayer() {
        return player;
    }
    public int getStep() {
        return step;
    }
    public void destroyPrefab() {
        Destroy(iconPrefab);
    }

    public void setNumber(int num) {
        iconPrefab.GetComponent<IconListener>().setNumber(num);
    }
}