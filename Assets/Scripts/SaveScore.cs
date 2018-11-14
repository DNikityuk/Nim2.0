using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;

public class SaveScore : MonoBehaviour {

    StreamWriter writer;
    StreamReader reader;
    NetworkStream stream;
    TcpClient client;

    public void saveScore(string time, int result, int complexity, int game) {
        try {
            client = new TcpClient("127.0.0.1", 9000);
        }
        catch {
            Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Resources.Load("ServerError"), spawnPosition, spawnRotation);
        }
        stream = client.GetStream();

        if (stream.CanRead) {
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.WriteLine("SaveScoreInit");
            writer.WriteLine("00:" + time + "$" + game + "$" + result + "$" + complexity + "$" + Authorization.getAuthUser().getId());
            writer.Flush();
        }
    }

}
