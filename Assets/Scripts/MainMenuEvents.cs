using UnityEngine;
using SocketIO;
using System.Collections.Generic;

public class MainMenuEvents : MonoBehaviour {
    GameObject gameObj;
    SocketIOComponent socket;
    BaseCharacter character;

    void Start()
    {
        gameObj = GameObject.Find("SocketIO");
        socket = gameObj.GetComponent<SocketIOComponent>();

        socket.On("signin", SetCharacter);
    }

    public void Signin()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["email"] = "harryandws@gmail.com";
        data["pwd"] = "1111";
        socket.Emit("signin", new JSONObject(data));
    }

    void SetCharacter(SocketIOEvent e)
    {
        Debug.Log(e);
    }

}

