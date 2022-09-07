using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int? haveCard;
    public bool isme;
    public int money;

    public Player(string playerName, bool isme=false)
    {
        haveCard = null;
        this.playerName = playerName;
        this.isme = isme;
    }

    public void SetCard(int card)
    {
        haveCard = card;
    }
}
