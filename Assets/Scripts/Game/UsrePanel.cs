using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsrePanel : MonoBehaviour
{
    [SerializeField] Text userName;
    [SerializeField] Text money;
    [SerializeField] Text card;

    public void UpdateData(string userName, string money, string card = "None")
    {
        this.userName.text = userName;
        this.money.text = money;
        this.card.text = card;
    }
}
