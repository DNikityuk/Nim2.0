using UnityEngine;
using System.Net.Sockets;
using System;
using System.IO;

public class Authorization : MonoBehaviour {

    StreamWriter writer;
    StreamReader reader;
    NetworkStream stream;
	TcpClient client;
    static AuthorizedUser user = null;

    public int authorizedUser(string login, string password) {
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
            writer.WriteLine("AuthorizationInit");
            writer.WriteLine(login + "$" + password);
            writer.Flush();
        }
        string result = reader.ReadLine();
        if(result.Equals("false")) {
            return 0;
        }
        else {
            String[] buffer = result.Split('$');
            user = new AuthorizedUser(buffer[0], buffer[1]);
            return 1;
        }
    }

    public static AuthorizedUser getAuthUser() {
        return user;
    }

    public static bool isAuth() {
        if (user == null) {
            return false;
        }
        return true;
    }

}
