using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instace
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (GameManager.Instace != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    List<Player> players;
    public List<Player> Players { get { return players; } }

    public bool isnewplayer = false;
    public void SetPlayer(string[] playerName, bool isme = false)
    {
        players = new List<Player>();
        foreach (string player in playerName)
        {
            Player tmp = new Player(player);
            players.Add(tmp);
        }
        isnewplayer = true;
    }
}
