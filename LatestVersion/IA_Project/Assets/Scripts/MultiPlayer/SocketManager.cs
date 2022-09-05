using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json.Linq;


public class SocketManager : PersistentSingleton<SocketManager> {

    WebSocket socket; //Connect, send, and receive messages from our server

    public bool IsConnecting => socket.ReadyState == WebSocketState.Open; //Return connect state of client

    public delegate void SocketMessage(string messages);
    public SocketMessage socketMessage;

    #region UnityEvent

    protected override void Awake() {
        base.Awake();

        //specifies the location of our websocket game server and where to connect
        socket = new WebSocket("ws://localhost:8080");
        //socket = new WebSocket("ws://websocket-starter-code-multiplayer-websocket-app.bsh-serverconnect-b3c-4x1-162e406f043e20da9b0ef0731954a894-0000.us-south.containers.appdomain.cloud/");
    }

    private void OnDestroy() {
        //Close socket when exiting application
        socket.Close();
    }

    #endregion

    #region  WebSocketEvent

    /// <summary>
    /// Connect to the server
    /// </summary>
    public void ConnectServer() {

        Initialize();

        socket.Connect();

    }

    /// <summary>
    /// Send a json data to server
    /// </summary>
    /// <param name="data"></param>
    public void Send(string data) {

        socket.Send(data);

    }


    /// <summary>
    /// Initialization of websocket event
    /// </summary>
    void Initialize() {
        socket.OnOpen += OnOpen;
        socket.OnMessage += OnMessageReceived;
        socket.OnError += OnError;
        socket.OnClose += OnClosed;
    }

    void AntiInit() {
        socket.OnOpen -= OnOpen;
        socket.OnMessage -= OnMessageReceived;
        socket.OnError -= OnError;
        socket.OnClose -= OnClosed;
    }

    void OnOpen(object sender, EventArgs e) {
    }

    void OnMessageReceived(object sender, MessageEventArgs e) {
        if (e.IsText)
            socketMessage(e.Data); //e.Data is a json string
    }

    void OnError(object sender, ErrorEventArgs e) {

    }

    void OnClosed(object sender, CloseEventArgs e) {

    }

    #endregion

}

/// <summary>
/// Type of request when send message to server
/// </summary>
public enum RequestType {

    CreateRoom,
    JoinRoom,
    Gameplay,
    EndGame

}