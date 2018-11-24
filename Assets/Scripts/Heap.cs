using UnityEngine;
using System.Collections;

public class Heap : MonoBehaviour {
    private Rock[] rocks;
    
    int heapNumber;
    int rockCount;

    public Heap(int heapNum, int col)
    {
        heapNumber = heapNum;
        rocks = new Rock[col];
        for (int i = 0; i < col; i++)
        {
            rocks[i] = new Rock(i, heapNumber);
        }
        rockCount = rocks.Length;
    }
    public Heap(int heapNum, int col, int p)
    {
        heapNumber = heapNum;
        rocks = new RockEducation[col];
        for (int i = 0; i < col; i++)
        {
            rocks[i] = new RockEducation(i, heapNumber);
        }
        rockCount = rocks.Length;
    }

    void Update() {
    }
    
    public void deleteRocksFromHeap() {
        for (int i = rockCount - 1; i >= 0; i--) {
            if (rocks[i].checkSelect()) {
                rockCount--;
                rocks[i].deleteSelected();
                rocks[i] = null;
            }
        }
    }
    public int getChoiceToDelete()
    {
        int choice=0;
        for (int i = rockCount - 1; i >= 0; i--)
        {
            if (rocks[i].checkSelect())
            {
                choice++;
            }
        }
        return choice;
    }

    public bool isSelectedHeap() {
        for (int i = 0; i < rockCount; i++) {
            if (rocks[i].checkSelect()) {
                return true;
            }
        }
        return false;
    }

    public bool isSelectedRequestHeap() {
        for (int i = 0; i < rockCount; i++) {
            if (rocks[i].getSelectRequest()) {
                rocks[i].setSelectRequest(false);
                rocks[i].setSelected();
                rocks[i].setMouseDown();
                for (int j = i + 1; j < rockCount; j++) {
                    rocks[j].setSelected();
                    rocks[j].resetMouseDown();
                }
                return true;
            }
        }
        return false;
    }

    public void removeSelectedHeap() {
        for (int i = 0; i < rockCount; i++) {
            if (rocks[i].checkSelect()) {
                rocks[i].resetSelected();
                rocks[i].resetMouseDown();
            }
        }
    }


    public void setCanBeSelectHeap(bool csh) {
        for (int i = 0; i < rockCount; i++) {
            rocks[i].setCanBeSelected(csh);
        }
    }

    public bool hasRocks() {
        if (rockCount > 0) {
            return true;
        }
        return false;
    }

    public int getRockCount() {
        return rockCount;
    }

    public void deleteCountOfRocks(int numberOfRocks) {
        for (int i = rockCount - 1, n = rockCount - numberOfRocks; i >= n; i--) {
            rockCount--;
            rocks[i].deleteComp();
        }
    }

    public int getFirstSelectedRock() {
        for (int i = 0; i < rockCount; i++) {
            if (rocks[i].checkSelect()) {
                return i;
            }
        }
        return -1;
    }

    public bool hasSelectRequest() {
        for (int i = 0; i < rockCount; i++) {
            if (rocks[i].getSelectRequest())
                return true;
        }
        return false;
    }

    public bool limitComplience(int ftLimit, int scLimit) {
        for (int i = 0; i < rockCount; i++) {
            if (rocks[i].getSelectRequest()) {
                if (rockCount - i == ftLimit || rockCount - i == scLimit)
                    return true;
                else {
                    rocks[i].setSelectRequest(false);
                }
            }
        }
        return false;
    }

    public void setShowRightRocks(int ftLimit, int scLimit) {
        if (ftLimit != scLimit) {
            int buf = rockCount - ftLimit;
            if (buf >= 0) {
                rocks[buf].setShowRightRocks(true);
            }
            buf = rockCount - scLimit;
            if (buf >= 0) {
                rocks[buf].setShowRightRocks(true);
            }
        }
        else {
            int buf = rockCount - ftLimit;
            if (buf >= 0) {
                rocks[buf].setShowRightRocks(true);
            }
        }
    }
}
