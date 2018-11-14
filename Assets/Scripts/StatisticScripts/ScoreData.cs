public class ScoreData{

    int game;
    int complexity;
    string gamePlayed;
    string win;
    string lose;
    string winrate;
    string avarageTime;

    public ScoreData (int _game, int _comp, string _gamePl, string _win, string _lose, string _winrate, string _time) {
        game = _game;
        complexity = _comp;
        gamePlayed = _gamePl;
        win = _win;
        lose = _lose;
        winrate = _winrate;
        avarageTime = _time;
    }

    public int getGame() {
        return game;
    }

    public int getComplexity() {
        return complexity;
    }

    public string getGamePlayed() {
        return gamePlayed;
    }

    public string getWin() {
        return win;
    }

    public string getLose() {
        return lose;
    }

    public string getWinRate() {
        return winrate;
    }

    public string getAvarageTime() {
        return avarageTime;
    }

    public void setGamePlayed(string value) {
        gamePlayed = value;
    }

    public void setWin(string value) {
        win = value;
    }

    public void setLose(string value) {
        lose = value;
    }

    public void setWinRate(string value) {
        winrate = value;
    }

    public void setAvarageTime(string value) {
        avarageTime = value;
    }
}
