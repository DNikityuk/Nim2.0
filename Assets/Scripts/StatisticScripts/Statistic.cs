using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Statistic : MonoBehaviour {
	public Sprite statisticBg;
	public Sprite topBg;
    public GameObject gameList;
    public GameObject levelList;
    public GameObject delButton;
    StreamWriter writer;
    StreamReader reader;
    NetworkStream stream;
    TcpClient client;
    AuthorizedUser user;
    TopPlayers top;
    GameObject[] topTexts;
    bool statisticDelete = false;

    void Start() {
        GameObject.Find("leftButton").GetComponent<Button>().interactable = false;
        user = Authorization.getAuthUser();
        top = new TopPlayers();
        getUserScore(user.getId());
        fillStatisticView(0, 0);
    }

    public int getUserScore(string id) { 
        try {
            client = new TcpClient("127.0.0.1", 9000);
        }
        catch {
            return -1;
        }
        stream = client.GetStream();

        if (stream.CanRead) {
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.WriteLine("StatisticInit");
            writer.WriteLine(id);
            writer.Flush();
        }
        string result = reader.ReadLine();
        if (!result.Equals("noStatistic")) {
            String[] bufferScore = result.Split('$');
            user.createScoreData(bufferScore.Length / 7);

            for (int i = 0; i < bufferScore.Length; i += 7) {
                user.addScore(i / 7, Int32.Parse(bufferScore[i]), Int32.Parse(bufferScore[i + 1]), bufferScore[i + 2], bufferScore[i + 3], bufferScore[i + 4],
                   bufferScore[i + 5], bufferScore[i + 6]);
            }
        }
        return 0;
    }

    public void fillStatisticView(int nim, int complexity) {
        ScoreData score = user.getScore(nim, complexity);
        if(score == null) {
            fillTextFields("0", "0", "0", "0.0", "00:00");
        }
        else {
            fillTextFields(
                score.getGamePlayed(),
                score.getWin(),
                score.getLose(),
                score.getWinRate(),
                score.getAvarageTime()
                );
        }
    }

    void fillTextFields(string _gamePl, string _win, string _lose, string _winrate, string _time) {
        GameObject.Find("gamePlayedText").GetComponent<Text>().text = _gamePl;
        GameObject.Find("winText").GetComponent<Text>().text = _win;
        GameObject.Find("loseText").GetComponent<Text>().text = _lose;
        GameObject.Find("winRateText").GetComponent<Text>().text = _winrate + "%";
        GameObject.Find("gameTimeText").GetComponent<Text>().text = _time;
    }

    public void onValueChangedGameList() {
        int game = GameObject.Find("gameListStat").GetComponent<Dropdown>().value;
        int level = GameObject.Find("gameLevelListStat").GetComponent<Dropdown>().value;
        fillStatisticView(game, level);
    }

    public void toMenuClick() {
        SceneManager.LoadScene("MenuWindow");
    }

    public void rightButtonClick() {
        showStatisticFields(false);
        GameObject.Find("background").GetComponent<Image>().sprite = topBg;
        GameObject.Find("leftButton").GetComponent<Button>().interactable = true;
        GameObject.Find("rightButton").GetComponent<Button>().interactable = false;
        GameObject.Find("logoText").GetComponent<Text>().text = "топ игроков";
        if (top.listLength() == 0 || statisticDelete) {
            top.getTopPlayers("0");
            setStatisticDelete(false);
        }
        fillTopUsers();
    }

    void fillTopUsers() {
        topTexts = new GameObject[top.listLength() * 2];
        for (int i = 0; i < top.listLength(); i ++) {
            float y;
            y = 355f - (i * 62f);
            topTexts[i * 2] = (GameObject)Instantiate(Resources.Load("loginText"), new Vector3(100.0f, y, 0.0f), Quaternion.identity);
            topTexts[i * 2 + 1] = (GameObject)Instantiate(Resources.Load("winRateText"), new Vector3(723.0f, y, 0.0f), Quaternion.identity);
            topTexts[i * 2].transform.parent = GameObject.Find("Canvas").transform;
            topTexts[i * 2 + 1].transform.parent = GameObject.Find("Canvas").transform;
            topTexts[i * 2].GetComponent<Text>().text = (i + 1) + ". " + top.getLoginUser(i);
            topTexts[i * 2 + 1].GetComponent<Text>().text = top.getWinrateUser(i) + "%";
        }
    }

    public void leftButtonClick() {
        for(int i = 0; i < topTexts.Length; i++) {
            Destroy(topTexts[i]);
        }
        showStatisticFields(true);
        GameObject.Find("background").GetComponent<Image>().sprite = statisticBg;
        GameObject.Find("leftButton").GetComponent<Button>().interactable = false;
        GameObject.Find("rightButton").GetComponent<Button>().interactable = true;
        GameObject.Find("logoText").GetComponent<Text>().text = "моя статистика";
    }

    void showStatisticFields(bool state) {
        gameList.SetActive(state);
        levelList.SetActive(state);
        delButton.SetActive(state);
        GameObject.Find("gamePlayedText").GetComponent<Text>().enabled = state;
        GameObject.Find("winText").GetComponent<Text>().enabled = state;
        GameObject.Find("loseText").GetComponent<Text>().enabled = state;
        GameObject.Find("winRateText").GetComponent<Text>().enabled = state;
        GameObject.Find("gameTimeText").GetComponent<Text>().enabled = state;
    }

    public void showDelStatsView() {
        GameObject.Find("rightButton").GetComponent<Button>().enabled = false;
        GameObject.Find("backToMenuButton").GetComponent<Button>().enabled = false;
        GameObject.Find("deleteStatsButton").GetComponent<Button>().enabled = false;
        Instantiate(Resources.Load("delStatsView"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }

    public void updateStatistic() {
        int game = GameObject.Find("gameListStat").GetComponent<Dropdown>().value;
        int level = GameObject.Find("gameLevelListStat").GetComponent<Dropdown>().value;
        user.setZeroStats(game, level);
        fillStatisticView(game, level);
    }

    public void setStatisticDelete(bool value) {
        statisticDelete = value;
    }
}
