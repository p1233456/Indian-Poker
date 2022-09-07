using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

struct User{
    public string id;
}

struct Protocol
{
    public string code;
    public string data;

    public Protocol (string code, string data)
    {
        this.code = code;
        this.data = data;
    }
}

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    InputField userName;
    
    public void TryLogin()
    {
        User user;
        user.id = userName.text;
        Client.Instace.Communication("100", user);
    }

    private void Update()
    {
        if (Client.Instace.login)
        {
            Client.Instace.login = false;
            SceneManager.LoadScene("GameScene");
        }
    }
}
