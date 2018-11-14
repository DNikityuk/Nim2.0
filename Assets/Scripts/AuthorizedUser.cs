using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorizedUser {

    string login;
    string id;
    ScoreData[] score = new ScoreData[0];

    public AuthorizedUser(string _id, string _login) {
        id = _id;
        login = _login;
    }

    public ScoreData getScore(int game, int complexity) {
        for(int i = 0; i < score.Length; i++) {
            if (score[i].getGame() == game && score[i].getComplexity() == complexity) {
                return score[i];
            }
        }
        return null;
    }

    public string getLogin() {
        return login;
    }

    public string getId() {
        return id;
    }

    public void createScoreData(int lenght) {
        score = new ScoreData[lenght];
    }

    public void addScore(int num, int _game, int _comp, string _gamePl, string _win, string _lose, string _winrate, string _time) {
        score[num] = new ScoreData(_game, _comp, _gamePl, _win, _lose, _winrate, _time);
    }

    public void setZeroStats(int game, int level) {
        ScoreData bufScore = getScore(game, level);
        if (bufScore != null) {
            bufScore.setGamePlayed("0");
            bufScore.setAvarageTime("00:00");
            bufScore.setLose("0");
            bufScore.setWin("0");
            bufScore.setWinRate("0.0");
        }
    }
}
