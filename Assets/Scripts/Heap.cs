﻿using UnityEngine;
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
                rocks[i].setMouseDown();
                for (i++; i < rockCount; i++) {
                    rocks[i].setSelected();
                    rocks[i].resetMouseDown();
                }
                return true;
            }
        }
        return false;
    }
    public void removeSelectedHeap()
    {
        for (int i = 0; i < rockCount; i++)
        {
            if (rocks[i].checkSelect())
            {
                rocks[i].resetSelected();
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

    public int getFirseSelectedRock() {
        for (int i = 0; i < rockCount; i++) {
            if (rocks[i].checkSelect()) {
                return i;
            }
        }
        return -1;
    }
    
}