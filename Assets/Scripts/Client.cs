using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Client : MonoBehaviour
{
    private static Client instance;
    public static Client Instace
    {
        get{return instance;}
    }

    private void Awake() {
        if(Client.Instace != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    TcpClient tc;
    NetworkStream stream;
    StreamReader sr;
    StreamWriter sw;
    private Thread m_ThrdClientReceive;
    public bool login = false;

    private void Start() {
        try
        {
            tc = new TcpClient("127.0.0.1", 7000);
            stream = tc.GetStream();
            sr = new StreamReader(stream);
            sw = new StreamWriter(stream); 
            m_ThrdClientReceive = new Thread(new ThreadStart(ListenForData));
            m_ThrdClientReceive.IsBackground = true;
            m_ThrdClientReceive.Start();
        }
        catch
        {
            Debug.Log("Connect Error!");
        }
    }

    private void ListenForData()
    {
        int length;
        Byte[] bytes = new Byte[1024];
        while (true)
        {
            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                var incommingData = new byte[length];
                Array.Copy(bytes, 0, incommingData, 0, length);

                string serverMessage = Encoding.Default.GetString(incommingData);
                Debug.Log(serverMessage); // 받은 값
                ReadProtocol(serverMessage);
            }
        }
    }


    public string Communication(string code, object data)
    {
        string strdata = JsonUtility.ToJson(data);
        string msg = "{\"code\":\"" + code + "\",\"data\":" + strdata + "}";
        Debug.Log(msg);
        byte[] buff = Encoding.ASCII.GetBytes(msg);
        stream.Write(buff, 0, buff.Length);
        return "oo";
    }

    public void ReadProtocol(string msg)
    {
        try
        {
            Protocol data = JsonUtility.FromJson<Protocol>(msg);
            switch (data.code)
            {
                //유저 접속
                case "200":
                    Debug.Log(data.code);
                    Debug.Log(data.data);
                    string[] users =  data.data.Replace("\"", "").Replace("\'","").Replace("[","").Replace("]","").Replace("{", "").Replace("}", "").Replace(",", "\t").Split('\t');
                    Dictionary<string, string> keyValuePairs2 = new Dictionary<string, string>();
                    foreach (string card in users)
                    {
                        string userid = card.Split(':')[0];
                        string username = card.Split(':')[1];
                        keyValuePairs2.Add(userid, username);
                    }
                    GameManager.Instace.SetPlayer(keyValuePairs2);
                    login = true;
                    break;
                case "202":
                    Debug.Log(data.code);
                    Debug.Log(data.data);
                    string[] cards = data.data.Replace("\"", "").Replace("'", "").Replace("[", "").Replace("]", "").Replace("{","").Replace("}","").Replace(",", "\t").Split('\t');
                    Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
                    foreach (string card in cards)
                    {
                        string user = card.Split(':')[0];
                        Debug.Log(card.Split(':')[1]);
                        int n = int.Parse(card.Split(':')[1].Trim());
                        keyValuePairs.Add(user, n);
                    }
                    GameManager.Instace.SetCard(keyValuePairs);
                    break;
            }
        }
        catch
        {
            Debug.Log("잘못된 프로토콜");  
            return;
        }
    }

    private void OnApplicationQuit()
    {
        tc.Close();
        stream.Close();
    }
}
