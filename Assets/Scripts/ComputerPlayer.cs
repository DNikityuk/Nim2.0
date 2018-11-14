using UnityEngine;
using System.Collections;

public class ComputerPlayer : MonoBehaviour {
    Controller controller;
    int gameLevel=-1;

    public ComputerPlayer(Controller rg, int level) {
        controller = rg;
        gameLevel = level;
    }


    public void computerStep() {
        switch (gameLevel) {
            case 0:
                if (Random.Range(1, 7) == 1)
                    rightStep();
                else wrongStep();
                break;
            case 1:
                if (Random.Range(1, 3) == 1)
                    rightStep();
                else wrongStep();
                break;
            case 2:
                rightStep();
                break;
        }
    }

    int xor_sum_Find() {
        int xor_sum = 0;
        for (int i = 0, n = controller.getNumberOfHeaps(); i < n; i++)
            xor_sum = (xor_sum ^ controller.getNumberRocksInHeap(i));
        return xor_sum;
    }

    public void rightStep() {
        int xor_sum = xor_sum_Find(), difference;
        if (xor_sum == 0) {
            wrongStep();
            return;
        }
        for (int i = 0, n = controller.getNumberOfHeaps(); i < n; i++) {
            int rocksInHeap = controller.getNumberRocksInHeap(i);
            xor_sum = (xor_sum ^ rocksInHeap);
            if (rocksInHeap >= xor_sum) {
                difference = rocksInHeap - xor_sum;
                rocksInHeap = xor_sum;
                movePaw(i, difference);                
                break;
            }
            xor_sum = (xor_sum ^ rocksInHeap);
        }
    }
    
    void wrongStep() {
        int xor_sum = 0;
        while (true) {
            int numberHeap = Random.Range(0, controller.getNumberOfHeaps());
            int rocksInHeap = controller.getNumberRocksInHeap(numberHeap);
            if (rocksInHeap == 0) continue;
            int difference = Random.Range(1, rocksInHeap + 1);
            if (difference != xor_sum)
                if (rocksInHeap >= difference) {
                    movePaw(numberHeap, difference);
                    break;
                }
        }
    }

    void movePaw(int i, int difference) {
        controller.setPointsPawComp(
                    controller.getNumberRocksInHeap(i) - difference,
                    i,
                    controller.getNumberRocksInHeap(i) - 1);
        controller.deleteSelectedCompRocks(i, difference);
        controller.moveCompPaw();
    }
}
