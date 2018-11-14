using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;

public class Registration : MonoBehaviour {

    StreamWriter writer;
    StreamReader reader;
    NetworkStream stream;
    TcpClient client;

    public string registerUser(string login, string password) {
        try {
            client = new TcpClient("127.0.0.1", 9000);
        }
        catch {
            Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate(Resources.Load("ServerError"), spawnPosition, spawnRotation);
            return "ServerError";
        }

        stream = client.GetStream();
        if (stream.CanRead) {
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.WriteLine("RegistrationInit");
            writer.WriteLine(login + "$" + password);
            writer.Flush();
        }
        return reader.ReadLine();
    }
}
