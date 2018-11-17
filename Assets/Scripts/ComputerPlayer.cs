using UnityEngine;
using System.Collections;

public class ComputerPlayer : MonoBehaviour {
    Controller controller;
    ConstructedController constrController;
    int gameLevel=-1;

    public ComputerPlayer(Controller rg, int level) {
        controller = rg;
        gameLevel = level;
    }

    public ComputerPlayer(ConstructedController rg, int level) {
        constrController = rg;
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

    void movePawConstructed(int i, int difference) {
        constrController.setPointsPawComp(
                    constrController.getNumberRocksInHeap(i) - difference,
                    i,
                    constrController.getNumberRocksInHeap(i) - 1);
        constrController.deleteSelectedCompRocks(i, difference);
        constrController.moveCompPaw();
    }

    public void computerStepConstructed() {
        switch (gameLevel) {
            case 0:
                if (Random.Range(1, 7) == 1)
                    rightStepConstructed();
                else wrongStepConstructed();
                break;
            case 1:
                if (Random.Range(1, 3) == 1)
                    rightStepConstructed();
                else wrongStepConstructed();
                break;
            case 2:
                rightStepConstructed();
                break;
        }
    }

    int xor_sum_find_constructed() {
        int xor_sum = 0;
        for (int i = 0; i < constrController.getNumberOfHeaps(); i++)
            xor_sum = (xor_sum ^ constrController.getGrundyHeap(i));
        return xor_sum;
    }

    private int xor_sum_find_constProc(int heap, int num) {
        int xor_sum = 0;
        for (int i = 0; i < constrController.getNumberOfHeaps(); i++)
            if (i == heap) {
                xor_sum = xor_sum ^ constrController.getGrundyHeapByNum(num);
            }
            else {
                xor_sum = xor_sum ^ constrController.getGrundyHeap(i);
            }
        return xor_sum;
    }

    public void rightStepConstructed() {
        int xor_sum = xor_sum_find_constructed(), difference;
        if (xor_sum == 0) {
            wrongStepConstructed();
            return;
        }
        int ft = constrController.getFtLimit();
        int sc = constrController.getScLimit();
        int rocksInHeap, rocksInHeapAfter;
        for (int i = 0, n = constrController.getNumberOfHeaps(); i < n; i++) {
            rocksInHeap = constrController.getNumberRocksInHeap(i);
            rocksInHeapAfter = rocksInHeap - ft;
            if (rocksInHeapAfter >= 0 && xor_sum_find_constProc(i, rocksInHeapAfter) == 0) {
                difference = ft;
                movePawConstructed(i, difference);
                break;
            }
            rocksInHeapAfter = rocksInHeap - sc;
            if (rocksInHeapAfter >= 0 && xor_sum_find_constProc(i, rocksInHeapAfter) == 0) {
                difference = sc;
                movePawConstructed(i, difference);
                break;
            }
        }
    }

    void wrongStepConstructed() {
        int difference;
        int[] limits = { constrController.getFtLimit(), constrController.getScLimit() };
        int numberHeap;
        int rocksInHeap;
        while (true) {
            numberHeap = Random.Range(0, constrController.getNumberOfHeaps());
            rocksInHeap = constrController.getNumberRocksInHeap(numberHeap);
            if (rocksInHeap < limits[0] && rocksInHeap < limits[1])
                continue;
            difference = limits[Random.Range(0, limits.Length)];
            if (rocksInHeap - difference >= 0) {
                movePawConstructed(numberHeap, difference);
                break;
            }
            else
                continue;
        }
    }
}
