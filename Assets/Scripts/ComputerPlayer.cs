using UnityEngine;
using System.Collections;

public class ComputerPlayer : MonoBehaviour {
    Controller controller;
    ConstructedController constrController;
    DatesController datesController;
    int gameLevel=-1;
    int playerNum;

    public ComputerPlayer(Controller rg, int level) {
        controller = rg;
        gameLevel = level;
    }

    public ComputerPlayer(ConstructedController rg, int level) {
        constrController = rg;
        gameLevel = level;
    }

    public ComputerPlayer(DatesController rg, int level, int plNum) {
        datesController = rg;
        gameLevel = level;
        playerNum = plNum;
        fill();
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

    public void wrongStepConstructed() {
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

    public void computerStepDates() {
        switch (gameLevel) {
            case 0:
                if (Random.Range(1, 7) == 1)
                    rightStepDates();
                else wrongStepDates();
                break;
            case 1:
                if (Random.Range(1, 3) == 1)
                    rightStepDates();
                else wrongStepDates();
                break;
            case 2:
                rightStepDates();
                break;
        }
    }


    
    public void rightStepDates() {
        int month = datesController.getCurrentGameMonth();
        int day = datesController.getCurrentDay();
        int rand = Random.Range(1, 5);
        for (int i = 0; i < 2; i++) {
            if (rand != 1) {
                if (datesController.getFinalDay() >= day + 1 && win[month, day + 1] == 2) {
                    datesController.computerStepWait(day + 1, month);
                    return;
                }
                if (datesController.getFinalDay() >= day + 2 && win[month, day + 2] == 2) {
                    datesController.computerStepWait(day + 2, month);
                    return;
                }
            }
            else {
                if (datesController.getFinalMonth() >= month + 1 && win[month + 1, day] == 2) {
                    datesController.computerStepWait(day, month + 1);
                    return;
                }
                if (datesController.getFinalMonth() >= month + 2 && win[month + 2, day] == 2) {
                    datesController.computerStepWait(day, month + 2);
                    return;
                }
            }
            rand = 2;
        }
        wrongStepDates();
    }

    public void wrongStepDates() {
        int month = datesController.getCurrentGameMonth();
        int day = datesController.getCurrentDay();
        int move;
        while (true) {
            move = Random.Range(0, 4);
            switch (move) {
                case 0:
                    if (datesController.getFinalDay() >= day + 1) {
                        datesController.computerStepWait(day + 1, month);
                        return;
                    }
                    break;
                case 1:
                    if (datesController.getFinalDay() >= day + 2) {
                        datesController.computerStepWait(day + 2, month);
                        return;
                    }
                    break;
                case 2:
                    if (datesController.getFinalMonth() >= month + 1) {
                        datesController.computerStepWait(day, month + 1);
                        return;
                    }
                    break;
                case 3:
                    if (datesController.getFinalMonth() >= month + 2) {
                        datesController.computerStepWait(day, month + 2);
                        return;
                    }
                    break;
            }
        }
    }



    int[,] win = new int[12, 31];

    bool isValid(int month, int day) {
        if (month == 0 || month == 2 || month == 4 || month == 6 || month == 7 || month == 9 || month == 11)
            if (day >= 0 && day <= 30)
                return true;
            else
                return false;
        if (month == 1)
            if (day >= 0 && day <= 28)
                return true;
            else
                return false;
        if (month == 3 || month == 5 || month == 8 || month == 10)
            if (day >= 0 && day <= 29)
                return true;
            else
                return false;
        return false;
    }

    public void fill() {
        int i, j;
        win[datesController.getFinalMonth(), datesController.getFinalDay()] = 1;
        for (i = datesController.getFinalMonth(); i >= datesController.getStartMonth(); i--) {
            int m = 0;
            int jLimit;
            if (i == datesController.getStartMonth()) {
                jLimit = datesController.getStartDay();
            }
            else {
                jLimit = 0;
            }
            if (i == datesController.getFinalMonth()) {
                m = datesController.getFinalDay() - 1;
            }
            else {
                m = datesController.getDaysCount(i) - 1;
            }
            for (j = m; j >= jLimit; j--) { 
                if ((isValid(i + 1, j) && win[i + 1, j] == 2) ||
                    (isValid(i + 2, j) && win[i + 2, j] == 2) ||
                    (isValid(i, j + 1) && win[i, j + 1] == 2) ||
                    (isValid(i, j + 2) && win[i, j + 2] == 2)) win[i, j] = 1;
                else win[i, j] = 2;
            }
        }
    }

}
