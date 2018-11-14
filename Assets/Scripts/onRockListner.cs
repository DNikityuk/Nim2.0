using UnityEngine;
using System.Collections;

public class onRockListner : MonoBehaviour {
    int isSelected = 0;
    bool canBeSelected = true;
    private bool isShake = false;
    private bool onMouseDown = false;
    int position = 0;
    float x;
    float y;
    int readyForDelete = 0;

	void Start () {
        x = transform.position.x;
        y = transform.position.y;
    }

	void OnMouseDown() {
        if(isSelected == 0 && canBeSelected) {
            isSelected = 1;
            StartCoroutine(shake());
            isShake = true;
            onMouseDown = true;          
        }
        else {
            if (onMouseDown){
                isSelected = 0;
                StopCoroutine(shake());
                isShake = false;
                onMouseDown = false;
            }
        }
    }

    void Update() {
        if (isSelected == 1) {
            if (!isShake) {
                StartCoroutine(shake());
                isShake = true;
            }
        }
        else {
            if (isShake) {
                StopCoroutine(shake());
                isShake = false;
            }
        }
    }

    public IEnumerator shake() {
        while (isSelected == 1) {
            switch (position) {
                case 0:
                    transform.Translate(0.0f, 0.05f, 0.0f);
                    position = 1;
                    break;
                case 1:
                    transform.Translate(0.0f, -0.1f, 0.0f);
                    position = -1;
                    break;
                case -1:
                    transform.Translate(0.0f, 0.1f, 0.0f);
                    position = 1;
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        transform.position = new Vector2(x, y);
        position = 0;
    }

    public void setMouseDown() {
        onMouseDown = true;
    }

    public void resetMouseDown() {
        onMouseDown = false;
    }

    public void setSelected() {
        isSelected = 1;
    }
    
    public void resetSelected() {
        isSelected = 0;
    }

    public int getIsSelected() {
        return isSelected;
    }

    public void deleteFromField() {
        Destroy(gameObject);
    }

    public void deleteComp() {
        setSelected();
        setReadyForDeleteComp();
        //StartCoroutine(pauseAndDelete());
    }

    public IEnumerator pauseAndDelete() {
        yield return new WaitForSeconds(2.0f);
        deleteFromField();
    }

    public void setCanBeSelected(bool cbs) {
        canBeSelected = cbs;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if ((col.gameObject.name == "longpaw" && readyForDelete == 1) ||
            col.gameObject.name == "comppaw" && readyForDelete == 2) {
            deleteFromField();
        }
    }

    public void setReadyForDeletePlayer() {
        readyForDelete = 1;
    }

    public void setReadyForDeleteComp() {
        readyForDelete = 2;
    }
}
