using UnityEngine;
using WebSocketSharp;

public class WS_Client : MonoBehaviour {

    int pinCode;

    WebSocket ws;

    void Start() {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();

        ws.OnMessage += OnMessage;
    }

    void Update() {
        if (ws == null) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            ws.Send("123456");
        }
    }

    void OnMessage(object sender, MessageEventArgs e) {
        Debug.Log("Message received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
    }
}