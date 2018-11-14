using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine;

public class SpeakInField  : MonoBehaviour{


    public Text TextRules, TextTitle, CatText;
    EducationController controller;
    public InputField input;
    string str1, str2, str3, res, heap;
    


    //// Use this for initialization
    void Start () {
        controller = gameObject.GetComponent<EducationController>();
        input = GameObject.Find("InputField").GetComponent<InputField>();
        heap = "";
       
    }

    //// Update is called once per frame
    void Update () {
        
    }
    
        

    public IEnumerator catSpeek(int catSay)
    {
        string text = "";
        switch (catSay)
        {
            case -1:
                text = "Привет.\t Давай попробуем сиграть в режиме обучающей игры. Я буду помогать " +
                    "тебе подсказками.\t";
                break;
            case 1:
                text = "Данная позиция выигрышная!\t";
                break;
            case 2:
                text = "Позиция проигрышная.\t Тут можно ходить по-разному,\t на твой выбор.";
                break;
            case 3:
                text = "Так как XOR-сумма ненулевая.\t";
                break;
            case 4:
                text = "А значит можно сходить в нулевую позицию, чтобы оставить " +
                    "противника в проигрышной позиции.";
                break;
            case 5:
                text = "Так как XOR-сумма нулевая.\t ";
                break;
            case 6:
                text = "Значит сходить в нулевую позицию, чтобы оставить " +
                    "противника в проигрышной, не удастся.   Ходи..";
                break;
            case 7:
                text = "Находим XOR-сумму всех кучек. Назовем ее S.\t\t";
                break;
            case 8:
                CatText.lineSpacing = 0.5f;
                str1 = Convert.ToString(controller.getNumberRocksInHeap(0), 2);
                str2 = Convert.ToString(controller.getNumberRocksInHeap(1), 2);
                str3 = Convert.ToString(controller.getNumberRocksInHeap(2), 2);
                str1 = convertThreeDigit(str1);
                str2 = convertThreeDigit(str2);
                str3 = convertThreeDigit(str3);

                text = "Посчитай сумму: " + str1 + "\n" +
                       "                             " + str2 + "\n" +
                       "                             " + str3 + "\n" +
                       "                            ------" + "\n" +
                       "Введи в поле:";
                break;
            case 9:
                CatText.lineSpacing = 1f;
                text = "Неверно.\t Считай внимательнее или посмотри как считается xor-сумма.\t\t";
                break;
            case 10:
                CatText.lineSpacing = 0.5f;
                str1 = Convert.ToString(controller.getNumberRocksInHeap(0), 2);
                str2 = Convert.ToString(controller.getNumberRocksInHeap(1), 2);
                str3 = Convert.ToString(controller.getNumberRocksInHeap(2), 2);
                res = Convert.ToString(controller.xorSum, 2);
                str1 = convertThreeDigit(str1);
                str2 = convertThreeDigit(str2);
                str3 = convertThreeDigit(str3);
                res = convertThreeDigit(res);
                text = " Правильный ответ: \n" +
                       "   " + str1 + "\n" +
                       "   " + str2 + "\n" +
                       "   " + str3 + "\n" +
                       "  ------" + "\n" +
                       "   " + res + "\n\t\t";
                break;
            case 11:
                CatText.lineSpacing = 1f;
                res = Convert.ToString(controller.xorSum, 2);
                res = convertThreeDigit(res);
                text =
                    "Теперь находим в этой сумме, в двоичном коде (" + res + "), " +
                    "место первой единицы.\t\t";
                break;
            case 12:
                CatText.lineSpacing = 1f;
                res = Convert.ToString(controller.xorSum, 2);
                res = convertThreeDigit(res);
                text = "Правильно.\t Теперь находим в этой сумме, в двоичном коде (" + res + "), " +
                    "место первой единицы.\t\t";
                break;
            case 13:
                text = "Находим кучку, в которой есть единица в этой же позиции.\t";
                break;
            case 14:
                text = "Такая хотя бы одна кучка есть, иначе бы и в XOR-сумме не было бы единицы.\t\t";
                break;
            case 15:
                int numHeap = controller.rightHeap + 1;
                str1 = Convert.ToString(controller.getNumberRocksInHeap(0), 2);
                str2 = Convert.ToString(controller.getNumberRocksInHeap(1), 2);
                str3 = Convert.ToString(controller.getNumberRocksInHeap(2), 2);
                str1 = convertThreeDigit(str1);
                str2 = convertThreeDigit(str2);
                str3 = convertThreeDigit(str3);
                switch (numHeap)
                {
                    case 1:
                        text = "Это первая кучка - " + str1;
                        heap = "первой";
                        break;
                    case 2:
                        text = "Это вторая кучка - " + str2;
                        heap = "второй";
                        break;
                    case 3:
                        text = "Это третья кучка - " + str3;
                        heap = "третьей";
                        break;
                }
                text += ".\t\t";
                break;

            case 16:
                text = "Находим XOR-сумму S и найденной кучки \t– получаем количество камушков, " +
                    "которое нужно оставить в найденной кучке.\t\t";
                break;
            case 17:
                CatText.lineSpacing = 0.5f;
                str1 = Convert.ToString(controller.getNumberRocksInHeap(controller.rightHeap), 2);
                str2 = Convert.ToString(controller.xorSum, 2);
                str1 = convertThreeDigit(str1);
                str2 = convertThreeDigit(str2);
                input.text = "";

                text = "Посчитай сумму: \n" + 
                       "                             "+str1 + "\n" +
                       "                             " + str2 + "\n" +
                       "                            ------" + "\n" +
                       "Введи в поле:";
                break;
            case 18:
                CatText.lineSpacing = 1f;
                text = "Неверно.\t Считай внимательнее или посмотри как считается xor-сумма.\t " +
                    "Ответ: " + convertThreeDigit(Convert.ToString(controller.leaveRocks, 2)) + ".\t";
                break;
            case 19:
                CatText.lineSpacing = 1f;
                text = "Да.\t";
                break;
            case 20:
                if (heap == "второй")
                    text += "Значит нам нужно оставить во ";
                else
                    text += "Значит нам нужно оставить в ";
                switch (controller.leaveRocks) {
                    case 1:
                        text += heap + " кучке " + controller.leaveRocks + " камень, " +
                            "то есть забрать необходимо - " + controller.rightRockTake + ".";
                        break;
                    case 2:
                        text += heap + " кучке " + controller.leaveRocks + " камня, " +
                        "то есть забрать необходимо - " + controller.rightRockTake + ".";
                        break;
                    case 3:
                        text += heap + " кучке " + controller.leaveRocks + " камня, " +
                        "то есть забрать необходимо - " + controller.rightRockTake + ".";
                        break;
                    case 4:
                        text += heap + " кучке " + controller.leaveRocks + " камня, " +
                        "то есть забрать необходимо - " + controller.rightRockTake + ".";
                        break;
                    default:
                        text += heap + " кучке " + controller.leaveRocks + " камней, " +
                        "то есть забрать необходимо - " + controller.rightRockTake + ".";
                        break;
                }
                text += "\nХоди..";
                break;

            case 21:
                text = "Ты уверен что хочешь так походить?\t Это может привести к проигрышу!";
                break;
            case 22:
                text = "Отлично! Все правильно.\t";
                break;
            case 23:
                text = "Ладно. Но смотри, этот выбор мог быть критичен.";
                break;
            case 24:
                text = "Продолжим.\t";
                break;
            case 25:
                text = "Все, теперь ты знаешь как играть в НИМ используя математическую теорию находя " +
                    "выигрышную стратегию.";
                break;
        }
        yield return StartCoroutine(tapCatText(text));
    }


    public string convertThreeDigit(string str)
    {
        switch (str.Length)
        {
            case 1:
                str = "00" + str;
                break;
            case 2:
                str = "0" + str;
                break;
        }
        return str;
    }


    public IEnumerator tapCatText( string text)
    {
        CatText.text = "";
        char[] charCat = text.ToCharArray();
        foreach (char ch in charCat)
        {
            switch (ch)
            {
                case ' ':
                    yield return new WaitForSeconds(0.03f);
                    CatText.text += ch;
                    break;
                case '.':
                    yield return new WaitForSeconds(0.5f);
                    CatText.text += ch;
                    break;
                case ',':
                    yield return new WaitForSeconds(0.5f);
                    CatText.text += ch;
                    break;
                case '\t':
                    yield return new WaitForSeconds(1.5f);
                    break;
                default:
                    yield return new WaitForSeconds(0.07f);//0.07
                    CatText.text += ch;
                    break;
            }
        }
    }

    public IEnumerator tapRulesText(Text TextUI, string text)
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
    public void clearCatText()
    {
        CatText.text = "";
    }
}
