using UnityEngine;

public class PawController : MonoBehaviour {
    public Sprite[] pawSprites;
    float[] x = { -5f, -3.4f, -1.8f, 0.2f, 1.4f, 3f, 4.6f };
    float[] y = { -0.22f, -1.88f, -3.46f, -5.2f, -6.86f };
    int[] speed = { 20, 16, 14, 12, 10  };
    float minDst = 0.01f;
    int flag = -1;
    Vector3 movePos;
    Vector3 rightPos;
    Vector3 endPos;
    int heapNumber = -1;
    float[] pointChangeSprite = new float[2];

    public void movePaw(int xStartNum, int yNum, int xEndNum) {
        transform.position = new Vector3(x[xStartNum], -9.87f, -1.0f);
        movePos = new Vector3(x[xStartNum], y[yNum], -1.0f);
        rightPos = new Vector3(x[xEndNum], y[yNum], -1.0f);
        endPos = new Vector3(x[xEndNum], -9.87f, -1.0f);
        heapNumber = yNum;

        float sum = (x[xEndNum] - x[xStartNum]) / 3;
        pointChangeSprite[0] = x[xStartNum] + sum;
        pointChangeSprite[1] = x[xEndNum] - sum;

        flag = 0;
    }

    void Update() {
        switch (flag) {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position,
               movePos,
               Time.deltaTime * speed[heapNumber]);
                if (Vector3.Distance(transform.position, movePos) < minDst) {
                    flag = 1;
                }
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = pawSprites[1];
                transform.position = Vector3.MoveTowards(transform.position,
               rightPos,
               Time.deltaTime * speed[heapNumber]);
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
               Time.deltaTime * speed[heapNumber]);
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

}
