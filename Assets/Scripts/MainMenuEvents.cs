using UnityEngine;
using SocketIO;
using System.Collections.Generic;
using System.IO;

public class MainMenuEvents : MonoBehaviour {
    GameObject gameObj;
    SocketIOComponent socket;
    UserInfo userInfo;

    void Start()
    {
        gameObj = GameObject.Find("SocketIO");
        socket = gameObj.GetComponent<SocketIOComponent>();

        socket.On("signin", CreateCharacter);
        socket.On("loadItem", LoadItem);
        socket.On("loadComplete", LoadComplete);
    }

    public void Signin()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["email"] = "harryandws@gmail.com";
        data["pwd"] = "1111";
        socket.Emit("signin", new JSONObject(data));
    }

    void CreateCharacter(SocketIOEvent e)
    {
        //유저 골드, 닉네임 세팅
        Debug.Log("set user info");

        string jsonString = e.data.ToString();

        Debug.Log(jsonString);

        userInfo = JsonUtility.FromJson<UserInfo>(jsonString);
    }

 
    void LoadItem(SocketIOEvent e)
    {
        //유저 아이템 저장
        Debug.Log("load item");

        string jsonString = e.data.ToString();

        Item item = JsonUtility.FromJson<Item>(jsonString);

        userInfo.LoadItem(item);
    }

    void LoadComplete(SocketIOEvent e)
    {

    }
}

