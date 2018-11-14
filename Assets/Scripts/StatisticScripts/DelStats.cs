using UnityEngine;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

public class DelStats : MonoBehaviour {

    StreamWriter writer;
    StreamReader reader;
    NetworkStream stream;
    TcpClient client;

    public int deleteStatistic(int complexity, string id) {
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
            writer.WriteLine("DeleteStatisticInit");
            writer.WriteLine(complexity + "$" + id);
            writer.Flush();
        }
        return 0;
    }

    public void noClick() {
        unblockElements();
    }

    public void yesClick() {
        deleteStatistic(GameObject.Find("gameLevelListStat").GetComponent<Dropdown>().value, Authorization.getAuthUser().getId());
        GameObject.Find("Canvas").GetComponent<Statistic>().setStatisticDelete(true);
        GameObject.Find("Canvas").GetComponent<Statistic>().updateStatistic();
        unblockElements();
    }

    void unblockElements() {
        GameObject.Find("rightButton").GetComponent<Button>().enabled = true;
        GameObject.Find("backToMenuButton").GetComponent<Button>().enabled = true;
        GameObject.Find("deleteStatsButton").GetComponent<Button>().enabled = true;
        Destroy(GameObject.Find("delStatsView(Clone)"));
    }
}
