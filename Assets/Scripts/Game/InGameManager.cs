using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instace
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (InGameManager.Instace != null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (GameManager.Instace.isnewplayer)
        {
            SetUser();
        }
    }

    [SerializeField] Transform canvas;
    [SerializeField] GameObject userPanel;

    public void SetUser()
    {
        var child = canvas.GetComponentsInChildren<Transform>();

        foreach (var iter in child)
        {
            if (iter != canvas.transform)
            {
                Destroy(iter.gameObject);
            }
        }

        foreach (Player user in GameManager.Instace.Players)
        {
            GameManager.Instace.isnewplayer = false;
            UsrePanel panel = Instantiate(userPanel, canvas).GetComponent<UsrePanel>();
            panel.UpdateData(user.playerName, user.money.ToString());
        }
    }
}
