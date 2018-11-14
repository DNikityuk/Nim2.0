using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EducationController : Controller  {
    
    SpriteRenderer gameBackground, cat;
    SpeakInField tapField;
    public int catSay ;
    public int rightHeap=-1, rightRockTake=0, xorSum=0, leaveRocks=0;
    bool confirmedTheChoice = false;
    bool askedInUser = false;
    bool rightChoice;
    public bool catSpeak = false;
    public bool catCanSpeak = true;
    protected int result;
    Canvas catCanvas, canvas1, canvas2, canvas3, canvas4;
    Button whyButton, howButton, continueButton;
    public InputField input;


    // Use this for initialization
    void Start () {
        numOfHeaps = 3;
        catSay = -1;
        gameBackground = GameObject.Find("background").GetComponent<SpriteRenderer>();
        endButton = GameObject.Find("endTurnButton").GetComponent<Button>();
        pauseButton = GameObject.Find("endTurnButton").GetComponent<Button>();
        cat = GameObject.Find("catWindow").GetComponent<SpriteRenderer>();
        catCanvas = GameObject.Find("CatCanvas").GetComponent<Canvas>();
        whyButton = GameObject.Find("whyButton").GetComponent<Button>();
        howButton = GameObject.Find("howButton").GetComponent<Button>();
        continueButton = GameObject.Find("continueButton").GetComponent<Button>();
        canvas1 = GameObject.Find("Canvas1").GetComponent<Canvas>();
        canvas2 = GameObject.Find("Canvas2").GetComponent<Canvas>();
        canvas3 = GameObject.Find("Canvas3").GetComponent<Canvas>();
        canvas4 = GameObject.Find("Canvas4").GetComponent<Canvas>();
        canvas1.enabled = false; canvas2.enabled = false; canvas3.enabled = false;
        canvas4.enabled = false;
        tapField = gameObject.GetComponent<SpeakInField>();
        input = GameObject.Find("InputField").GetComponent<InputField>();
        
        cp = new ComputerPlayer(this, 2);
        random = new System.Random();
        rockGenerator(numOfHeaps);
        blockAllHeaps();        
        archive = new ArhiveGame(getState());
        setStartPlayer(0);
        StartCoroutine(scenario());
    }
	
	// Update is called once per frame
	protected override void Update()
    {
        if (!pause)
        {
            if (!catSpeak)
            {
                if (catCanSpeak)
                    StartCoroutine(scenario());

                if (canContinueGame() && startPlay)
                {
                    switch (currentPlayer)
                    {
                        case 0:
                            for (int i = 0; i < numOfHeaps; i++)
                            {
                                if (heaps[i].hasRocks() && heaps[i].isSelectedHeap())
                                {
                                    selectedHeap = i;
                                    blockUnselectedHeaps();
                                    break;
                                }
                                else
                                {
                                    selectedHeap = -1;
                                    unblockAllHeaps();
                                }
                            }
                            if (selectedHeap != -1 && endButtonIsClick)
                            {
                                if (!askedInUser)
                                {
                                    if (rightChoice=isRightChoise(selectedHeap, heaps[selectedHeap].getChoiceToDelete()))
                                        takeTheRocks();
                                    else {
                                        askedInUser = true;
                                        catSay = 21;
                                        StartCoroutine(scenario());
                                        break;
                                    }
                                }
                                else
                                {
                                    if (confirmedTheChoice)
                                        takeTheRocks();
                                }
                            }
                            break;
                        case 1:
                            endButton.interactable = false;
                            cp.computerStep();
                            archive.addState(getState());
                            StartCoroutine(pauseAndActiveEndButton());
                            if (canContinueGame())
                            {
                                setCurrentPlayer(0);
                                startPlay = false;
                                catSay = 24;
                                StartCoroutine(scenario());
                                catSay = 0;
                                catCanSpeak = true;
                            }
                            else
                                showEndGame();
                            break;
                    }
                }
            }
        } 
    }

    void takeTheRocks()
    {
        paw.movePaw(heaps[selectedHeap].getFirseSelectedRock(),
                                        selectedHeap,
                                        heaps[selectedHeap].getRockCount() - 1);

        heaps[selectedHeap].deleteRocksFromHeap();
        archive.addState(getState());
        if (canContinueGame())
        {
            if (rightChoice)
            {
                if (rightRockTake != 0)                
                {
                    catSay = 22;
                    StartCoroutine(scenario());
                }
            }
            else
            {
                catSay = 23;
                StartCoroutine(scenario());
            }
            blockAllHeaps();
            rightHeap = 0; rightRockTake = 0; xorSum = 0; leaveRocks = 0;
            askedInUser = false;
            endButton.interactable = false;
            endButtonIsClick = false;
            selectedHeap = -1;
            setCurrentPlayer(1);
        }
        else
        {            
            showEndGame();
            catSay = 25;
            StartCoroutine(scenario());
        }
    }

    IEnumerator scenario()
    {
        catCanSpeak = false;
        catSpeak = true;
        switch (catSay)
        {
            case -1:
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(showCat());
                yield return StartCoroutine(tapField.catSpeek(catSay));
                yield return new WaitForSeconds(1f);
                catSay = 0;
                break;
            case 1:
                yield return StartCoroutine(tapField.catSpeek(catSay));                
                canvas1.enabled = true;
                break;
            case 2:
                yield return StartCoroutine(tapField.catSpeek(catSay));
                canvas1.enabled = true;
                break;
            case 3:
                for (; catSay < 5;)
                {
                    yield return StartCoroutine(tapField.catSpeek(catSay));
                    catSay++;
                }
                canvas2.enabled = true; 
                break;
            case 5:
                for (; catSay < 7;)
                {
                    yield return StartCoroutine(tapField.catSpeek(catSay));
                    catSay++;
                }
                unblockAllHeaps();
                startPlay = true;
                break;            
            case 7:
                for (; catSay < 9;)
                {
                    yield return StartCoroutine(tapField.catSpeek(catSay));
                    if (catSay == 8) {
                        yield return new WaitForSeconds(0.3f);
                        canvas3.enabled = true;
                        break;
                    }
                    else
                        catSay++;
                }
                break;
            case 9:
                for (; catSay < 18;)
                {
                    yield return StartCoroutine(tapField.catSpeek(catSay));
                    if (catSay == 17)
                    {
                        yield return new WaitForSeconds(1f);
                        canvas3.enabled = true;
                        break;
                    }
                    if (catSay == 11)
                        catSay = 13;
                    else
                        catSay++;
                }
                break;
            case 12:
                for (; catSay < 18;)
                {
                    yield return StartCoroutine(tapField.catSpeek(catSay));
                    if (catSay == 17)
                    {
                        yield return new WaitForSeconds(1f);
                        canvas3.enabled = true;
                        break;
                    }
                    catSay++;
                }
                break;
            case 18:
                for (; catSay < 21;)
                {
                    yield return StartCoroutine(tapField.catSpeek(catSay));
                    if (catSay == 20)
                    {
                        unblockAllHeaps();
                        startPlay = true;
                    }
                    if (catSay == 19)
                        catSay = 20;
                    else
                        catSay++;
                    
                }
                break;
            case 19:
                for (; catSay < 21;)
                {
                    yield return StartCoroutine(tapField.catSpeek(catSay));
                    if (catSay == 20)
                    {
                        unblockAllHeaps();
                        startPlay = true;
                    }
                    catSay++;
                }
                break;

            case 21:
                yield return StartCoroutine(tapField.catSpeek(catSay));
                canvas4.enabled = true;
                break;
            case 22:
                yield return StartCoroutine(tapField.catSpeek(catSay));
                break;
            case 23:
                yield return StartCoroutine(tapField.catSpeek(catSay));
                break;
            case 24:
                tapField.clearCatText();
                yield return StartCoroutine(tapField.catSpeek(catSay));
                break;
            case 25:
                yield return new WaitForSeconds(5.5f);
                yield return StartCoroutine(tapField.catSpeek(catSay));
                yield return new WaitForSeconds(5f);
                SceneManager.LoadScene("MenuWindow");
                break;
        }
        catSpeak = false;
    }

    int xorSumFind()
    {
        int xor_sum = 0;
        for (int i = 0, n = getNumberOfHeaps(); i < n; i++)
            xor_sum = (xor_sum ^ getNumberRocksInHeap(i));
        return xor_sum;
    }

    public bool findRightStep()
    {
        int xor_sum = xorSumFind();
        if(xor_sum==0)
            return false;
        xorSum = xor_sum;
        for (int i = 0, n = getNumberOfHeaps(); i < n; i++)
        {
            int rocksInHeap = getNumberRocksInHeap(i);
            xor_sum = (xor_sum ^ rocksInHeap);
            if (rocksInHeap >= xor_sum)
            {
                leaveRocks = xor_sum;
                rightRockTake = rocksInHeap - xor_sum;
                rocksInHeap = xor_sum;
                rightHeap = i;
                return true;
            }
            xor_sum = (xor_sum ^ rocksInHeap);
        }        
        return false;
    }
    public bool isRightChoise(int numOfHeap, int numOfRock)
    {
        if(rightRockTake==0)
            return true;
        if (numOfHeap == rightHeap && numOfRock == rightRockTake)
            return true;
        else
            return false;
    }
    
    protected override void rockGenerator(int num)
    {
        heaps = new Heap[num];
        for (int i = 0; i < num; i++)
        {
            heaps[i] = new Heap(i, random.Next(1, 8), 0);
        }
    }

    public override void setStartPlayer(int player)
    {
        currentPlayer = player;
        archive.setFirstPlayer(currentPlayer);
        enabledGameButtons(true);        
    }

    public override void pauseButtonClick()
    {
        setPause(true);
        blockAllHeaps();
        enabledGameButtons(false);
        Instantiate(Resources.Load("PauseView"));
    }

   IEnumerator showCat()
    {
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(0.1f);
            cat.transform.position += new Vector3(0, 0.425f);
        }
    }

    IEnumerator hideCat()
    {
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(0.1f);
            cat.transform.position -= new Vector3(0, 0.425f);
        }
    }
    public void clikOnWhyButton()
    {
        if(catSay==1)
            catSay = 3;
        if (catSay == 2)
            catSay = 5;
        catCanSpeak = true;
        canvas1.enabled = false;
    }
    public void clikOnHowButton()
    {
        catSay = 7;
        catCanSpeak = true;
        canvas2.enabled = false; ;
    }
    public void clikOnContinueButton()
    {
        unblockAllHeaps();
        startPlay = true;
        canvas2.enabled = false;
        canvas1.enabled = false;
    }
    public void clikOnYesButton()
    {
         confirmedTheChoice= true;
         canvas4.enabled = false;
    }
    public void clikOnNoButton()
    {
        confirmedTheChoice = false;
        canvas4.enabled = false;
        askedInUser = false;
        endButtonIsClick = false;
        heaps[getSelectedHeap()].removeSelectedHeap();
        selectedHeap = -1;
    }

    public void clikOnCheckButton()
    {
        string xor = Convert.ToString(xorSum, 2);
        xor = gameObject.GetComponent<SpeakInField>().convertThreeDigit(xor);

        if (catSay == 8 && input.text== xor)
        {
            canvas3.enabled = false;
            catSay = 12;
            catCanSpeak = true;
            return;
        }
        if (catSay == 8 && input.text != xor)
        {
            canvas3.enabled = false;
            catSay = 9;
            catCanSpeak = true;
            return;
        }

        string leave = Convert.ToString(leaveRocks, 2);
        leave = gameObject.GetComponent<SpeakInField>().convertThreeDigit(leave);
        if (catSay == 17 && input.text == leave)
        {
            canvas3.enabled = false;
            catSay = 19;
            catCanSpeak = true;
            return;
        }
        if (catSay == 17 && input.text != leave)
        {
            canvas3.enabled = false;
            catSay = 18;
            catCanSpeak = true;
            return;
        }
    }
    public void showEndGame()
    {
        if (!endGame)
        {
            string res;

            float delay;
            if (currentPlayer == 1)
            {
                res = "Поражение";
                result = 0;
                delay = 3.0f;
            }
            else
            {
                res = "Победа";
                delay = 1.0f;
                GameObject.Find("playAgainButton").SetActive(false);
                GameObject.Find("backMenuButton").SetActive(false);
                StartCoroutine(hideResultView(5f));
            }
            enabledGameButtons(false);
            resultView.GetComponent<resultView>().setText(res);
            StartCoroutine(showResultView(delay));
            endGame = true;
        }
    }


    //public void clickOnSkip()
    //{
    //    skipButtonClick = true;
    //}
}
