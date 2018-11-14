using UnityEngine;
using System.Collections;

public class PawCompController : MonoBehaviour {
    public Sprite[] pawSprites;
    float[] x = { -5f, -3.4f, -1.8f, 0.2f, 1.4f, 3f, 4.6f };
    float[] y = { 6.86f, 5.2f, 3.46f, 1.88f, 0.22f };
    int[] speed = { 10, 12, 14, 16, 20 };
    float minDst = 0.01f;
    int flag = -1;
    Vector3 movePos;
    Vector3 rightPos;
    Vector3 endPos;
    int yNum = -1;
    int xStartNum = -1;
    int xEndNum = -1;

    float[] pointChangeSprite = new float[2];

   IEnumerator moveAfterPause() {
        yield return new WaitForSeconds(2.0f);
        transform.position = new Vector3(x[xStartNum], 9.8f, -1.0f);
        movePos = new Vector3(x[xStartNum], y[yNum], -1.0f);
        rightPos = new Vector3(x[xEndNum], y[yNum], -1.0f);
        endPos = new Vector3(x[xEndNum], 9.8f, -1.0f);

        float sum = (x[xEndNum] - x[xStartNum]) / 3;
        pointChangeSprite[0] = x[xStartNum] + sum;
        pointChangeSprite[1] = x[xEndNum] - sum;

        flag = 0;
    }

    public void movePaw() {
        StartCoroutine(moveAfterPause());
    }

    void Update() {
        switch (flag) {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position,
               movePos,
               Time.deltaTime * speed[yNum]);
                if (Vector3.Distance(transform.position, movePos) < minDst) {
                    flag = 1;
                }
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = pawSprites[1];
                transform.position = Vector3.MoveTowards(transform.position,
               rightPos,
               Time.deltaTime * speed[yNum]);
                if (!GetComponent<SpriteRenderer>().sprite.Equals(pawSprites[2]) &&
                    transform.position.x >= pointChangeSprite[0] &&
                    transform.position.x <= pointChangeSprite[1]) {
                    GetComponent<SpriteRenderer>().sprite = pawSprites[2];
                }
                if (!GetComponent<SpriteRenderer>().sprite.Equals(pawSprites[3]) &&
                    transform.position.x >= pointChangeSprite[1] &&
                    transform.position.x <= rightPos.x) {

                    GetComponent<SpriteRenderer>().sprite = pawSprites[3];
                }
                if (Vector3.Distance(transform.position, rightPos) < minDst) {
                    flag = 2;
                }
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position,
               endPos,
               Time.deltaTime * speed[yNum]);
                if (!GetComponent<SpriteRenderer>().sprite.Equals(pawSprites[3])) {

                    GetComponent<SpriteRenderer>().sprite = pawSprites[3];
                }
                if (Vector3.Distance(transform.position, endPos) < minDst) {
                    GetComponent<SpriteRenderer>().sprite = pawSprites[0];
                    flag = -1;
                }
                break;
        }
    }

    public void setPoints(int _xStartNum, int _yNum, int _xEndNum) {
        xStartNum = _xStartNum;
        yNum = _yNum;
        xEndNum = _xEndNum;
    }

}
