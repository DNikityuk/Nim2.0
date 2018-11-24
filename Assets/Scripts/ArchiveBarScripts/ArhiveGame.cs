using UnityEngine;
using System.Collections.Generic;


public class ArhiveGame {

    public List<int[]> archiveHeaps;
    public int[] heapsBefore;
    public int[] heapsAfter;
    public int firstPlayer = -1;
    public int numStep = 0;

    public List<string> archiveDates;

    public ArhiveGame(string startDate) {
        archiveDates = new List<string>();
        archiveDates.Add(startDate);
    }

    public void addStateDate(string date) {
        archiveDates.Add(date);
        numStep++;
    }

    public string getStateDates(int step) {
        return archiveDates[step];
    }
    public string getPrevStateDates(int step) {
        return archiveDates[step - 1];
    }

    public ArhiveGame(int[] heap) {
        archiveHeaps = new List<int[]>();
        archiveHeaps.Add(heap);
    }

    public void addState(int[] heap) {
        archiveHeaps.Add(heap);
        numStep++;
    }

    public int[] getState(int step) {
        return archiveHeaps[step];
    }

    public int getNumberOfRock(int step) {
        if (step < 0)
            return 0;
        heapsBefore = archiveHeaps[step - 1];
        heapsAfter = archiveHeaps[step];

        for (int i = 0; i < heapsBefore.Length; i++)
            if (heapsBefore[i] - heapsAfter[i] != 0)
                return heapsBefore[i] - heapsAfter[i];
        return 0;
    }

    public int getNumberOfHeap(int step) {
        if (step < 0)
            return -1;
        heapsBefore = archiveHeaps[step - 1];
        heapsAfter = archiveHeaps[step];

        for (int i = 0; i < heapsBefore.Length; i++)
            if (heapsBefore[i] - heapsAfter[i] != 0)
                return i;
        return -1;
    }
    public void setFirstPlayer(int player) {
        firstPlayer = player;
    }
    public int getFirstPlayer() {
        return firstPlayer;
    }

    public int getStepPlayer(int step) {
        if (step % 2 != 0)
            return firstPlayer;
        else {
            if (firstPlayer == 0)
                return 1;
            else
                return 0;
        }
    }

    public int getNumStep() {
        return numStep;
    }
}
