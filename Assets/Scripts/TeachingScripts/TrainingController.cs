using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingController :MonoBehaviour  {

    bool nextButtonClick=false;
    bool backButtonClick =false;
    bool skipButtonClick = false;
    int currentPage = 0;
    int catSay = 0;
    int trainingStage = 3;
    bool tapText = false;
    //bool animationCat = false;
    Text TextUI, TextTitle, CatText;
    SpriteRenderer cat, startBackground, gameBackground;
    Canvas rulesCanvas, catCanvas;
   


    // Use this for initialization
    void Start () {
        rulesCanvas = GameObject.Find("RulesCanvas").GetComponent<Canvas>();
        rulesCanvas.enabled = false;
        catCanvas = GameObject.Find("CatCanvas").GetComponent<Canvas>();        
        TextUI =GameObject.Find("TextUI").GetComponent<Text>();
        TextTitle= GameObject.Find("TextTitle").GetComponent<Text>();
        CatText = GameObject.Find("catText").GetComponent<Text>();
        cat = GameObject.Find("catWindow").GetComponent<SpriteRenderer>();
        startBackground = GameObject.Find("startBackground").GetComponent<SpriteRenderer>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (!tapText) {
            if (nextButtonClick)
            {
                currentPage++;
                nextButtonClick = false;
                if (currentPage == 7)
                    StartCoroutine(scenario());
                else {
                    
                }
            }
            if (backButtonClick)
            {
                backButtonClick = false;
                if (currentPage > 0)
                    currentPage--;
                if (trainingStage == 1)
                    StartCoroutine(showRules());
                if (trainingStage == 2)
                    ;
                if (trainingStage == 3)
                    ;
            }
            if (skipButtonClick)
            {
                currentPage = 0;
                trainingStage++;
                skipButtonClick = false;
                if (trainingStage == 1)
                    showRules();
                if (trainingStage == 2)
                    ;
                if (trainingStage == 3)
                    ;
            }
        }
        else
        {
            if (nextButtonClick || backButtonClick)
            {
                nextButtonClick = false;
                backButtonClick = false;
            }
        }
    }
  
    IEnumerator scenario()
    {
        switch (catSay)
        {
            case 0:
                yield return StartCoroutine(showCat());
                for (; catSay < 4;)
                {
                    yield return StartCoroutine(catSpeek());
                    catSay++;
                }
                catCanvas.enabled = false;
                yield return StartCoroutine(hideCat());
                //catSay = 4;//check
                rulesCanvas.enabled = true;
                yield return StartCoroutine(showRules());
                showRules();
                break;
            case 4:
                catCanvas.enabled = true;
                rulesCanvas.enabled = false;
                yield return StartCoroutine(showCat());
                for (; catSay < 6;)
                {
                    yield return StartCoroutine(catSpeek());
                    catSay++;
                }
                catCanvas.enabled = false;
                yield return StartCoroutine(hideCat());
                rulesCanvas.enabled = true;
                yield return StartCoroutine(showRules());
                break;

        }

    }

    IEnumerator showRules()
    {
        string text = "";
        switch (currentPage)
        {
            case 0:
                TextTitle.text = "Что такое Игра?";
                text = "    Нет, мы не будем говорить о компьютерных играх, и не о подкидном дураке, " +
                    "и, конечно, не о футболе. Речь пойдет об играх математических, а точнее, комбинаторных.\n" +
                    "   Оказывается, математики, кроме синусов и косинусов, изучают также игры. " +
                    "Этот факт школьные учебники по математике почему - то скрывают от учеников. " +
                    "Многие математические теории можно рассматривать как игры с определёнными правилами. " +
                    "А часто и наоборот — игры порождают сложные математические теории.";
                break;
            case 1:
                TextTitle.text = "Что такое Игра?";
                text = "    В математике слово «игра» может обозначать как собственно игру, в которой " +
                    "участвует более одного игрока, имеются определенные правила, а цель игры — одержать " +
                    "победу в партии, так и математические развлечения и головоломки. \n" +
                    "     С точки зрения математики существует признак, определяющий две большие группы: " +
                    "игры с полной информацией и игры, в которых присутствует элемент неопределенности. " +
                    "Мы же рассматриваем игры первой группы и будем называть ее стратегическими играми. \t\n";
                break;
            case 2:
                TextTitle.text = "Что такое Игра?";
                text = "" +
                    "     Из всех подобных игр нам больше всего знакомы шахматы, хотя подобных стратегических игр, " +
                    "как традиционных (го, манкала, шашки, крестики-нолики), так и современных (гекс, ним, реверси, " +
                    "абалон и другие), существует великое множество — это известные примеры комбинаторных игр. " +
                    "Можно сказать, что комбинаторные игры — это игры, где нет элементов случайности, все " +
                    "правила чётко описаны, и игроки имеют полную информацию о текущей ситуации.";
                break;
            case 3:
                TextTitle.text = "Что такое Игра?";
                text = "    Строгое определение звучит так:\t\n" +
                    "   Комбинаторная игра — это множество игровых позиций, про каждую " +
                    "из которых игрокам известно, как(в какие другие позиции) может ходить каждый из них.\n" +
                    "   \tВ дальнейшем мы будем говорить об игре, в которой участвуют два игрока. ";
                break;
            case 4:
                TextTitle.text = "Математика в стратегических играх";
                text = "    О какой математике идет речь? \t" +
                    "На примере стратегической игры НИМ мы увидим, как математика используется в анализе " +
                    "игр для определения преимущества одного из игроков и для нахождения выигрышной стратегии. " +
                    "\tПрименительно к стратегическим играм математика особенно полезна для определения выигрышной " +
                    "стратегии. Стратегическая игра очень похожа на процесс решения математической задачи: речь " +
                    "идет не о том, чтобы выиграть одну партию, совершая более удачные ходы, но о том, чтобы " +
                    "найти способ, ";
                break;
            case 5:
                TextTitle.text = "Математика в стратегических играх";
                text = "как выигрывать всегда.\t\n" +
                    "   По этой причине при определении выигрышных стратегий используются эвристические методы: " +
                    "способ «от обратного»; предположение, что игра «решена»; применение симметрии; проведение " +
                    "аналогии с другой, уже решенной игрой и прочие. Они аналогичны тем, что используются при " +
                    "решении математических задач. \t\n" +
                    "   Определение общей выигрышной стратегии, применимой к любой игре такого типа, — " +
                    "одно из ярчайших проявлений того, как математика";
                break;
            case 6:
                TextTitle.text = "Математика в стратегических играх";
                text = "используется для анализа игр, и в " +
                    "особенности того, насколько эффективно представление чисел в двоичной системе.";
                break;
            case 7:
                TextTitle.text = "НИМ";
                text = "    Ним — одна из старейших и увлекательных комбинаторных игр; кроме того, ним — фундамент, " +
                    "на котором воздвигается математическая теория комбинаторных игр о которых мы говорили ранее. \n" +
                    "   \tНекоторые считают, что она родом с Востока. В неё любили играть китайские императоры. Тем, " +
                    "кто у них выигрывал, отрубали голову. Также неясно и происхождение названия. Среди возможных " +
                    "версий — староанглийское слово «ним», означавшее «брать», «красть».";
                break;
            case 8:
                TextTitle.text = "НИМ";
                text = "    Некто очень остроумный заметил, что если применить к слову NIM центральную симметрию, " +
                    "получится слово WIN — «выиграть» в переводе с английского. \n  Как бы то ни было, игре Ним " +
                    "больше ста лет: первый анализ выигрышной стратегии для игр подобного типа был впервые " +
                    "опубликован в 1902 году математиком Гарвардского университета Чарльзом Леонардом Боутоном.";
                break;
        }
        tapText = true;
        yield return StartCoroutine(tapRulesText(text));
        tapText = false;
    }

    void showWinningStrategy()
    {
        string text="";
        switch (currentPage)
        {
            case 1:
                text = "";
                break;
            case 2:
                break;
            case 3:
                break;


        }
    }
    IEnumerator catSpeek()
    {
        string text ="" ;
        switch (catSay)
        {
            case 0:
                text = "Мяу, мяу мяуу мяу.\t\t";
                break;
            case 1:
                text = "Тьфу ты ну ты.. забыл что ты человек. Хех).\t\t";
                break;
            case 2:
                text = "Привет еще раз.\t Меня зовут Том.\t И сейчас я хочу предложить тебе " +
                    "ознакомиться с нашей игрой.\t";
                break;
            case 3:
                text ="Я так понимаю ты согласен раз " +
                    "зашел ко мне.\t Что же..\t давай расскажу тебе немного теории.\t Мрряуу..\t\t";
                break;
            case 4:
                text = "Таакк..\tну тут все скучно..\t Давай-ка расскажу тебе в какую именно игру мы будем " +
                    "играть, о правилах нашей игры. \t";
                break;
            case 5:
                text = "Тут все просто..\t\t";
                break;
            case 6:
                text = "";
                break;
            case 7:
                text = "";
                break;
        }
        yield return StartCoroutine(tapCatText(text));        
    }


    IEnumerator tapRulesText(string text)
    {
        TextUI.text = "";
        char[] charCat = text.ToCharArray();
        foreach (char ch in charCat)
        {
            switch (ch)
            {
                case ' ':
                    yield return new WaitForSeconds(0.03f);
                    TextUI.text += ch;
                    break;
                case '.':
                    yield return new WaitForSeconds(0.15f);
                    TextUI.text += ch;
                    break;
                case ',':
                    yield return new WaitForSeconds(0.1f);
                    TextUI.text += ch;
                    break;
                case '\t':
                    yield return new WaitForSeconds(1.8f);
                    break;
                default:
                    yield return new WaitForSeconds(0.02f);
                    TextUI.text += ch;
                    break;
            }
        }
    }

    IEnumerator tapCatText(string text)
    {
        CatText.text = "";
        char[] charCat = text.ToCharArray();
        foreach (char ch in charCat)
        {
            switch (ch)
            {
                case ' ':
                    yield return new WaitForSeconds(0.11f);
                    CatText.text += ch;
                    break;
                case '.':
                    yield return new WaitForSeconds(0.4f);
                    CatText.text += ch;
                    break;
                case ',':
                    yield return new WaitForSeconds(0.4f);
                    CatText.text += ch;
                    break;
                case '\t':
                    yield return new WaitForSeconds(1.5f);
                    break;
                default:
                    yield return new WaitForSeconds(0.09f);
                    CatText.text += ch;
                    break;
            }
        }
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

    public void clickOnNext()
    {
        nextButtonClick = true;
    }
    public void clickOnBack()
    {
        backButtonClick = true;
    }
    public void clickOnSkip()
    {
        skipButtonClick = true;
    }
}
