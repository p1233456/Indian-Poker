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

    Dictionary<string, Player> players;
    public Dictionary<string,Player> Players { get { return players; } }

    public bool isnewplayer = false;
    public void SetPlayer(Dictionary<string, string> playerName, bool isme = false)
    {
        players = new Dictionary<string, Player>();
        foreach (KeyValuePair<string,string> player in playerName)
        {
            Player tmp = new Player(player.Value);
            players.Add(player.Key, tmp);
        }
        isnewplayer = true;
    }

    public void SetCard(Dictionary<string, int> cards)
    {
        foreach (KeyValuePair<string,int> card in cards)
        {
            players[card.Key].haveCard = card.Value;
            InGameManager.Instace.UpdateUser(card.Key, players[card.Key]);
        }
    }
}
