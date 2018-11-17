using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ConstructedController : MonoBehaviour {
    protected int numOfHeaps;
    int gameNum = 0;
    int gameLevel;
    int ftLimit;
    int scLimit;
    public int[] grundy;
    public Heap[] heaps;
    protected ComputerPlayer cp;
    public ArhiveGame archive;
    protected int selectedHeap = -1;
    protected int currentPlayer = -1;
    protected bool endGame;
    protected bool startPlay = false;
    protected bool pause = false;
    protected bool endButtonIsClick = false;
    //public Canvas firstTurnView;
    public Canvas resultView;
    public PawController paw;
    public PawCompController pawComp;
    protected Button endButton, pauseButton;
    TimerCount timer;
    protected System.Random random;


    void Start() {
        endGame = false;
        ftLimit = constructedNimPropertiesView.getFtLimit();
        scLimit = constructedNimPropertiesView.getScLimit();
        gameLevel = constructedNimPropertiesView.getGameLevel();
        numOfHeaps = constructedNimPropertiesView.getNumberOfHeap();
        random = new System.Random();
        rockGenerator(numOfHeaps);
        //blockAllHeaps();
        cp = new ComputerPlayer(this, gameLevel);
        archive = new ArhiveGame(getState());
        GetComponent<ViewArchiveMenu>().setArchive(archive);
        endButton = GameObject.Find("endTurnButton").GetComponent<Button>();
        pauseButton = GameObject.Find("pauseButton").GetComponent<Button>();
        timer = GameObject.Find("TimerText").GetComponent<TimerCount>();
        enabledGameButtons(false);
        fillGrundy();
        setStartPlayer(constructedNimPropertiesView.getFirstTurn());
        //firstTurnView.GetComponent<firstTurnView>().setEnabled(true);
    }

    protected virtual void Update() {
        if (!pause) {
            if (canContinueGame() && startPlay) {
                if (currentPlayer == 0) {
                    if (selectedHeap != -1) {
                        if (endButtonIsClick) {
                            paw.movePaw(heaps[selectedHeap].getFirstSelectedRock(),
                                selectedHeap,
                                heaps[selectedHeap].getRockCount() - 1);

                            heaps[selectedHeap].deleteRocksFromHeap();
                            archive.addState(getState());
                            selectedHeap = -1;
                            setCurrentPlayer(1);
                            endButton.interactable = false;
                            endButtonIsClick = false;
                        }
                    }

                    for (int i = 0; i < numOfHeaps; i++) {
                        if (heaps[i].hasSelectRequest()) {
                            if (heaps[i].limitComplience(ftLimit, scLimit)) { 
                                for (int j = 0; j < numOfHeaps; j++) {
                                    heaps[j].removeSelectedHeap();
                                }
                                if (heaps[i].isSelectedRequestHeap())
                                    break;
                            }
                        }
                    }

                    for (int i = 0; i < numOfHeaps; i++) {
                        if (heaps[i].hasRocks() && heaps[i].isSelectedHeap()) {
                            selectedHeap = i;
                            //blockUnselectedHeaps();
                            break;
                        }
                        else {
                            selectedHeap = -1;
                            //unblockAllHeaps();
                        }
                    }
                }
                else {
                    endButton.interactable = false;
                    cp.computerStepConstructed();
                    archive.addState(getState());
                    StartCoroutine(pauseAndActiveEndButton());
                    setCurrentPlayer(0);
                }
            }
            else
            {
                if (startPlay && !endGame)
                {
                    string res;
                    int result;
                    timer.enabledTimer(false);
                    float delay;
                    if (currentPlayer == 0)
                    {
                        res = "Поражение";
                        result = 0;
                        delay = 3.0f;
                    }
                    else
                    {
                        res = "Победа";
                        result = 1;
                        delay = 1.0f;
                    }
                    enabledGameButtons(false);
                    resultView.GetComponent<resultView>().setText(res);
                    StartCoroutine(showResultView(delay));
                    if (Authorization.isAuth()&& timer!=null)
                    {
                        string time = timer.getTime();
                        resultView.GetComponent<SaveScore>().saveScore(time, result, gameLevel, gameNum);
                    }
                    endGame = true;
                }
            }
        }
    }

    protected IEnumerator pauseAndActiveEndButton() {
        yield return new WaitForSeconds(3.0f);
        endButton.interactable = true;
    }

    protected IEnumerator showResultView(float delay) {
        yield return new WaitForSeconds(delay);
        resultView.GetComponent<resultView>().setEnabled(true);
    }
    protected IEnumerator hideResultView(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultView.GetComponent<resultView>().setEnabled(false);
    }

    protected virtual void rockGenerator(int num) {
        heaps = new Heap[num];
        for (int i = 0; i < num; i++) {            
           heaps[i] = new Heap(i, random.Next(5, 8));
        }
    }

    public int[] getState() {
        int[] heapsDecode = new int[numOfHeaps];
        for (int i = 0; i < numOfHeaps; i++)
            heapsDecode[i] = heaps[i].getRockCount();
        return heapsDecode;
    }

    protected void blockUnselectedHeaps() {
        for (int i = 0; i < numOfHeaps; i++) {
            if (heaps[i].hasRocks() && getSelectedHeap() != i) {
                heaps[i].setCanBeSelectHeap(false);
            }
        }
    }

    protected void blockAllHeaps() {
        for (int i = 0; i < numOfHeaps; i++) {
            heaps[i].setCanBeSelectHeap(false);
        }
    }

    public void unblockAllHeaps() {
        for (int i = 0; i < numOfHeaps; i++) {
            heaps[i].setCanBeSelectHeap(true);
        }
    }

    public int getSelectedHeap() {
        return selectedHeap;
    }

    public void setSelectedHeap(int sh) {
        selectedHeap = sh;
    }

    public int getNumberOfHeaps() {
        return numOfHeaps;
    }

    public int getNumberRocksInHeap(int heapNumber) {
        return heaps[heapNumber].getRockCount();
    }

    public void deleteSelectedCompRocks(int heapNumber, int numberOfRocks) {
        heaps[heapNumber].deleteCountOfRocks(numberOfRocks);
    }

    public void setCurrentPlayer(int player) {
        currentPlayer = player;
    }
    
    public virtual void setStartPlayer(int player) {
        currentPlayer = player;
        archive.setFirstPlayer(currentPlayer);
        timer.enabledTimer(true);
        enabledGameButtons(true);
        unblockAllHeaps();
        startPlay = true;
    }

    public void endButtonClick() {
        if (selectedHeap != -1) {
            endButtonIsClick = true;
        }
    }

    public bool canContinueGame() {
        for (int i = 0; i < numOfHeaps; i++) {
            if (heaps[i].getRockCount() >= ftLimit || heaps[i].getRockCount() >= scLimit)
                return true;
        }
        return false;
    }

    public virtual void pauseButtonClick() {
        timer.pauseTimer();
        setPause(true);
        blockAllHeaps();
        enabledGameButtons(false);
        Instantiate(Resources.Load("PauseView"));
    }

    public void enabledGameButtons(bool value) {
        endButton.enabled = value;
        pauseButton.enabled = value;
    }

    public void setPause(bool value) {
        pause = value;
    }

    public void startTimer() {
        enabledGameButtons(true);
        timer.startTimer();
    }

    public void moveCompPaw() {
        pawComp.movePaw();
    }

    public void setPointsPawComp(int xStart, int y, int xEnd) {
        pawComp.setPoints(xStart, y, xEnd);
    }

    private void fillGrundy() {
        if(ftLimit == scLimit) {
            grundy = new int[ftLimit * 2];
        }
        else {
            grundy = new int[ftLimit + scLimit];
        }
        for (int i = 0; i < grundy.Length; i++) {
            grundy[i] = mex(i - ftLimit, i - scLimit);
        }
    }

    private int mex(int ftMove, int scMove) {
        int[] grundyMoves = {-1, -1};
        int value = 0;

        if (ftMove >= 0)
            grundyMoves[0] = grundy[ftMove];
        if (scMove >= 0)
            grundyMoves[1] = grundy[scMove];
        while (true) {
            if (grundyMoves[0] == value || grundyMoves[1] == value)
                value++;
            else {
                return value;
            }
        }
    }

    public int getGrundyHeap(int i) {
        int r = getNumberRocksInHeap(i) % grundy.Length;
        return grundy[r];
    }

    public int getGrundyHeapByNum(int num) {
        int r = num % grundy.Length;
        return grundy[r];
    }

    public int getFtLimit() {
        return ftLimit;
    }

    public int getScLimit() {
        return scLimit;
    }
}