using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DatesController : MonoBehaviour {
    int gameNum = 3;
    int gameLevel;
    protected ComputerPlayer cp;
    protected int currentPlayer = -1;
    protected bool endGame;
    protected bool startPlay = false;
    protected bool pause = false;
    public ArhiveGame archive;
    //public Canvas firstTurnView;
    public Canvas resultView;
    protected Button pauseButton;
    TimerCount timer;
    protected System.Random random;
    private DateCell[] dateCells;
    private int startDay;
    private int startMonth;
    private int finalDay;
    private int finalMonth;
    public Text month;
    private String[] months = {"январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август",
            "сентябрь", "октябрь", "ноябрь", "декабрь"};
    private int[] numDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    int currentMonth = 0, prevMonth;
    int currentDay = 0;
    int dateNum;
    public Button leftChanger, rightChanger;
    int currentGameMonth;
    private int selectedCell;

    void Start() {
        endGame = false;
        gameLevel = datesPropertiesView.getGameLevel();
        random = new System.Random();
        setDates();
        leftChanger.interactable = false;
        currentMonth = startMonth;
        currentDay = startDay;
        currentGameMonth = currentMonth;
        prevMonth = currentMonth;

        archive = new ArhiveGame(prepDate(currentDay, currentGameMonth));
        GetComponent<ViewArchiveMenu>().setArchive(archive);

        if (currentMonth == 11) {
            rightChanger.interactable = false;
        }
        dateCellsGenerator();
        setCurrentCell(true, currentDay);
        if (currentMonth == finalMonth) {
            setFinalCell(true, finalDay);
        }
        unblockMovesCells();

        cp = new ComputerPlayer(this, gameLevel, 2);
        pauseButton = GameObject.Find("pauseButton").GetComponent<Button>();
        timer = GameObject.Find("TimerText").GetComponent<TimerCount>();
        enabledGameButtons(false);
        setStartPlayer(datesPropertiesView.getFirstTurn());
    }

    public void setEnabledCells(bool val) {
        for (int i = 0; i < numDays[currentMonth]; i++) {
            dateCells[i].setCanSelected(val);
        }
        if (currentMonth != finalMonth)
            rightChanger.enabled = val;
        else
            rightChanger.enabled = true;
        if (currentMonth == finalMonth)
            rightChanger.interactable = false;
    }

    protected virtual void Update() {
        if (!pause) {
            if (canContinueGame() && startPlay) {
                if (currentPlayer == 0) {
                    selectedCell = checkSelected();
                    if (selectedCell != -1) {
                        leftChanger.interactable = false;
                        dateCells[selectedCell].setIsSelected(false);
                        setCurrentCell(false, currentDay);
                        currentDay = selectedCell;
                        currentGameMonth = currentMonth;
                        archive.addStateDate(prepDate(currentDay, currentGameMonth));
                        setCurrentCell(true, currentDay);
                        unblockMovesCells();
                        setCurrentPlayer(1);
                    }
                }
                else {
                    setEnabledCells(false);
                    cp.computerStepDates();
                    currentPlayer = 0;
                }
            }
            else {
                if (startPlay && !endGame) {
                    string res;
                    int result;
                    timer.enabledTimer(false);
                    float delay;
                    if (currentPlayer == 1) {
                        res = "Поражение";
                        result = 0;
                        delay = 1.0f;
                    }
                    else {
                        res = "Победа";
                        result = 1;
                        delay = 1.0f;
                    }
                    enabledGameButtons(false);
                    resultView.GetComponent<resultView>().setText(res);
                    StartCoroutine(showResultView(delay));
                    if (Authorization.isAuth() && timer != null) {
                        string time = timer.getTime();
                        resultView.GetComponent<SaveScore>().saveScore(time, result, gameLevel, gameNum);
                    }
                    endGame = true;
                }
            }
        }
    }

    protected IEnumerator waitCompStep(int newDay, int newMonth) {
        yield return new WaitForSeconds(1.0f);
        computerStep(newDay, newMonth);
    }

    public void computerStepWait(int newDay, int newMonth) {
        StartCoroutine(waitCompStep(newDay, newMonth));
    }

    public void computerStep(int newDay, int newMonth) {
        int newMonthBuf = newMonth;
        while (newMonthBuf != currentMonth) {
            rightDateChange();
        }
        leftChanger.interactable = false;
        setCurrentCell(false, currentDay);
        currentDay = newDay;
        currentGameMonth = newMonth;
        archive.addStateDate(prepDate(currentDay, currentGameMonth));
        setCurrentCell(true, currentDay);
        unblockMovesCells();
        setEnabledCells(true);
        setCurrentPlayer(0);
    }

    private void setDates() {
        startDay = datesPropertiesView.getStartDay() - 1;
        startMonth = datesPropertiesView.getStartMonth() - 1;
        finalDay = datesPropertiesView.getFinalDay() - 1;
        finalMonth = datesPropertiesView.getFinalMonth() - 1;
    }

    private int checkSelected() {
        for (int i = 0; i < numDays[currentMonth]; i++) {
            if (dateCells[i].getIsSelected())
                return i;
        }
        return -1;
    }

    protected IEnumerator showResultView(float delay) {
        yield return new WaitForSeconds(delay);
        resultView.GetComponent<resultView>().setEnabled(true);
    }
    protected IEnumerator hideResultView(float delay) {
        yield return new WaitForSeconds(delay);
        resultView.GetComponent<resultView>().setEnabled(false);
    }

    protected virtual void dateCellsGenerator() {
        dateNum = 31;
        dateCells = new DateCell[dateNum];
        for (int i = 0; i < dateNum; i++) {
            dateCells[i] = new DateCell(i + 2);
        }
        fixButtons();
    }

    private string formatMonth(string curMonth) {
        if (currentMonth > 8) {
            return (currentMonth + 1) + " - " + curMonth;
        }
        else {
            return "0" + (currentMonth + 1) + " - " + curMonth;
        }
    } 

    private void fixButtons() {
        month.text = formatMonth(months[currentMonth]);
        if (numDays[prevMonth] == numDays[currentMonth]) {
            if (dateNum > numDays[currentMonth]) {
                for (int i = dateNum - 1; i > numDays[currentMonth] - 1; i--) {
                    dateCells[i].setActiveCell(false);
                }
            }
        }
        if (numDays[prevMonth] > numDays[currentMonth]) {
            for (int i = numDays[prevMonth] - 1; i > numDays[currentMonth] - 1; i--) {
                dateCells[i].setActiveCell(false);
            }
        }
        else {
            if (numDays[prevMonth] < numDays[currentMonth]) {
                for (int i = numDays[prevMonth]; i < numDays[currentMonth]; i++) {
                    dateCells[i].setActiveCell(true);
                }
            }
        }
    }

    public void rightDateChange() {
        prevMonth = currentMonth;
        currentMonth++;
        if (currentMonth == currentGameMonth) {
            setCurrentCell(true, currentDay);
        }
        else {
            setCurrentCell(false, currentDay);
        }
        if (currentMonth == finalMonth) {
            setFinalCell(true, finalDay);
        }
        else {
            setFinalCell(false, finalDay);
        }
        if (currentMonth == finalMonth) {
            rightChanger.interactable = false;
        }
        leftChanger.interactable = true;
        fixButtons();
        unblockMovesCells();
    }

    public void leftDateChange() {
        prevMonth = currentMonth;
        currentMonth--;
        if (currentMonth == currentGameMonth) {
            setCurrentCell(true, currentDay);
        }
        else {
            setCurrentCell(false, currentDay);
        }
        if (currentMonth == finalMonth) {
            setFinalCell(true, finalDay);
        }
        else {
            setFinalCell(false, finalDay);
        }
        if (currentMonth == startMonth || currentMonth == currentGameMonth) {
            leftChanger.interactable = false;
        }
        rightChanger.interactable = true;
        fixButtons();
        unblockMovesCells();
    }

    public void blockCells() {
        for (int i = 0; i < dateCells.Length; i++) {
            dateCells[i].setInteractable(false);
        }
    }

    public void unblockMovesCells() {
        blockCells();
        if (currentGameMonth == currentMonth) {
            if (currentDay + 1 < numDays[currentMonth] && currentDay + 1 <= finalDay) {
                dateCells[currentDay + 1].setInteractable(true);
            }
            if (currentDay + 2 < numDays[currentMonth] && currentDay + 2 <= finalDay) {
                dateCells[currentDay + 2].setInteractable(true);
            }
        }
        if (currentGameMonth + 1 == currentMonth) {
            if (currentDay <= numDays[currentMonth]) {
                dateCells[currentDay].setInteractable(true);
            }
        }
        if (currentGameMonth + 2 == currentMonth) {
            if (currentDay <= numDays[currentMonth]) {
                dateCells[currentDay].setInteractable(true);
            }
        }
    }

    public void setCurrentCell(bool val, int i) {
        dateCells[i].setCurrentCell(val);
    }

    public void setFinalCell(bool val, int i) {
        dateCells[i].setFinalCell(val);
    }

    public void setCurrentPlayer(int player) {
        currentPlayer = player;
    }
    
    public virtual void setStartPlayer(int player) {
        currentPlayer = player;
        archive.setFirstPlayer(currentPlayer);
        timer.enabledTimer(true);
        enabledGameButtons(true);
        startPlay = true;
    }

    public bool canContinueGame() {
        if(currentGameMonth == finalMonth && currentDay == finalDay) {
            return false;
        }
        return true;
    }

    public virtual void pauseButtonClick() {
        timer.pauseTimer();
        setPause(true);
        setEnabledCells(false);
        enabledGameButtons(false);
        Instantiate(Resources.Load("PauseView"));
    }

    public void enabledGameButtons(bool value) {
        pauseButton.enabled = value;
    }

    public void setPause(bool value) {
        pause = value;
    }

    public void startTimer() {
        enabledGameButtons(true);
        timer.startTimer();
    }

    //public void moveCompPaw() {
    //    pawComp.movePaw();
    //}

    //public void setPointsPawComp(int xStart, int y, int xEnd) {
    //    pawComp.setPoints(xStart, y, xEnd);
    //}

    public int getFinalDay() {
        return finalDay;
    }

    public int getFinalMonth() {
        return finalMonth;
    }

    public int getStartDay() {
        return startDay;
    }

    public int getStartMonth() {
        return startMonth;
    }

    public int getDaysCount (int month) {
        return numDays[month];
    }

    public int getCurrentDay() {
        return currentDay;
    }

    public int getCurrentGameMonth() {
        return currentGameMonth;
    }

    public int getGameNum() {
        return gameNum;
    }

    public string prepDate(int day, int month) {
        day++;
        month++;
        string bufDay = "";
        string bufMonth = "";
        if (day > 9) {
            bufDay += day;
        }
        else {
            bufDay = "0" + day;
        }
        if (month > 9) {
            bufMonth += month;
        }
        else {
            bufMonth = "0" + month;
        }
        return bufDay + "." + bufMonth;
    }


}