using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoom : MonoBehaviour {


    [SerializeField] Text pINText;

    [SerializeField] RoomData roomData;

    void Awake() {
        pINText.text = roomData.currentRoomPIN.ToString();
    }


}