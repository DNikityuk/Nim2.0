using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;
using UnityEngine.UI;

public class TopPlayers : MonoBehaviour {

    StreamWriter writer;
    StreamReader reader;
    NetworkStream stream;
    TcpClient client;
    AuthorizedUser user;
    TopUser[] topUserList = new TopUser[0];

    public void getTopPlayers(string game) {
        try {
            client = new TcpClient("127.0.0.1", 9000);
        }
        catch {
            return;
        }
        stream = client.GetStream();

        if (stream.CanRead) {
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.WriteLine("TopPlayerInit");
            writer.WriteLine(game);
            writer.Flush();
        }
        string result = reader.ReadLine();
        if (!result.Equals("noStatistic")) {
            String[] bufferTop = result.Split('$');
            createUserList(bufferTop.Length / 2);

            for (int i = 0; i < bufferTop.Length; i += 2) {
                addTopUser(i / 2, bufferTop[i + 1], bufferTop[i]);
            }
        }
        return;
    }

    public int listLength() {
        return topUserList.Length;
    }

    void createUserList(int length) {
        topUserList = new TopUser[length];
    }

    void addTopUser(int num, string login, string winrate) {
        topUserList[num] = new TopUser(login, winrate);
    }

    public string getLoginUser(int num) {
        return topUserList[num].getLogin();
    }

    public string getWinrateUser(int num) {
        return topUserList[num].getWinrate();
    }

    class TopUser {
        string login;
        string winrate;

        public TopUser(string _login, string _winrate) {
            login = _login;
            winrate = _winrate;
        }

        public string getLogin() {
            return login;
        }

        public string getWinrate() {
            return winrate;
        }
    }
}
