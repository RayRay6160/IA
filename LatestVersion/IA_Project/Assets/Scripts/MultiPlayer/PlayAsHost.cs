using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class PlayAsHost : MonoBehaviour {

    [SerializeField] Text inputName;

    [SerializeField] RoomData roomData;

    RoomRequest roomRequest;

    public int RandomPINgenerate() => Random.Range(1000, 9999);

    void Awake() {

        InitializeRoomData();

    }

    void Start() {

        SocketManager.Instance.socketMessage += OnMessage;

    }

    void InitializeRoomData() {

        roomRequest = new RoomRequest();
        roomRequest.requestType = RequestType.CreateRoom;

    }

    public void TryCreateRoom() {

        //Connect to server
        if (!SocketManager.Instance.IsConnecting)
            SocketManager.Instance.ConnectServer();

        //Generate the pin code
        roomRequest.pinCode = RandomPINgenerate();
        roomRequest.playerName = inputName.text;

        string jRoomData = JsonUtility.ToJson(roomRequest);

        SocketManager.Instance.Send(jRoomData);

    }

    //The result of create new room
    void OnMessage(string data) {

        JObject jsonObj = JObject.Parse(data);

        bool result = jsonObj.SelectToken("result").Value<bool>();

        string roomPINStr = jsonObj.SelectToken("PIN").Value<string>();

        int roomPIN = int.Parse(roomPINStr);

        if (result == true) {

            Debug.Log("Create room successful : " + roomPIN);

            roomData.currentRoomPIN = roomPIN;
            Debug.Log("Create room successful : " + roomPIN);
            //Load room scene
            try {
                //  TryCreateRoom();
                SceneLoader.Instance.LoadGuestScene();
            } catch (UnityException e) {
                Debug.Log(e);
            }

        } else {

            //Debug.Log("Fail to create room");

            //Try create room again if fail
            TryCreateRoom();

        }
    }
}

public struct RoomRequest {

    public RequestType requestType;

    public int pinCode;

    public string playerName;

}