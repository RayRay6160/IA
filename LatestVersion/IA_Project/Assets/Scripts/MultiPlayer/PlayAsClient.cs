using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class PlayAsClient : MonoBehaviour {

    [SerializeField] Text inputPIN;
    [SerializeField] Text inputName;

    RoomRequest roomRequest;

    void Awake() {

        InitializeRoomData();

    }

    void Start() {

        SocketManager.Instance.socketMessage += OnMessage;

    }

    void InitializeRoomData() {

        roomRequest = new RoomRequest();
        roomRequest.requestType = RequestType.JoinRoom;

    }

    public void TryJoinRoom() {

        //Connect to server if not connecting
        if (!SocketManager.Instance.IsConnecting)
            SocketManager.Instance.ConnectServer();

        //Detect the input pin code
        if (inputPIN.text != "") {
            roomRequest.pinCode = int.Parse(inputPIN.text);
            roomRequest.playerName = inputName.text;
        }

        //Convert join room request to json
        string jRoomData = JsonUtility.ToJson(roomRequest);

        SocketManager.Instance.Send(jRoomData);

    }

    void OnMessage(string data) {

        JObject jsonObj = JObject.Parse(data);

        bool result = jsonObj.SelectToken("result").Value<bool>();

        string joinRoomPIN = jsonObj.SelectToken("PIN").Value<string>();

        if (result == true) {

            Debug.Log("Join room successful : " + joinRoomPIN);

            //Load room scene
            SceneLoader.Instance.LoadGuestScene();

        } else {

            Debug.Log("Undefined room : " + joinRoomPIN);

        }
    }
}