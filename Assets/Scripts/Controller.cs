using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Controller : MonoBehaviour {
    protected int numOfHeaps;
    int gameLevel;
    int gameNum;
    public Heap[] heaps;
    protected ComputerPlayer cp;
    public ArhiveGame archive;
    protected int selectedHeap = -1;
    protected int currentPlayer = -1;
    protected bool endGame;
    protected bool startPlay = false;
    protected bool pause = false;
    protected bool endButtonIsClick = false;
    public Canvas firstTurnView;
    public Canvas resultView;
    public PawController paw;
    public PawCompController pawComp;
    protected Button endButton, pauseButton;
    TimerCount timer;
    protected System.Random random;


    void Start() {
        endGame = false;
        gameNum = gamePropertiesView.getGameName();
        gameLevel = gamePropertiesView.getGameLevel();
        numOfHeaps = gamePropertiesView.getNumberOfHeap();
        random = new System.Random();
        rockGenerator(numOfHeaps);
        blockAllHeaps();
        cp = new ComputerPlayer(this, gameLevel);
        archive = new ArhiveGame(getState());
        endButton = GameObject.Find("endTurnButton").GetComponent<Button>();
        pauseButton = GameObject.Find("endTurnButton").GetComponent<Button>();
        timer = GameObject.Find("TimerText").GetComponent<TimerCount>();
        enabledGameButtons(false);
        firstTurnView.GetComponent<firstTurnView>().setEnabled(true);
    }

    protected virtual void Update() {
        if (!pause) {
            if (canContinueGame() && startPlay) {
                if (currentPlayer == 0) {
                    if (selectedHeap != -1) {
                        if (endButtonIsClick) {
                            paw.movePaw(heaps[selectedHeap].getFirseSelectedRock(),
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
                        if (heaps[i].hasRocks() && heaps[i].isSelectedHeap()) {
                            selectedHeap = i;
                            blockUnselectedHeaps();
                            break;
                        }
                        else {
                            selectedHeap = -1;
                            unblockAllHeaps();
                        }
                    }
                }
                else {
                    endButton.interactable = false;
                    cp.computerStep();
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
            heaps[i] = new Heap(i, random.Next(1, 8));
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
            if (heaps[i].getRockCount() > 0)
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
}