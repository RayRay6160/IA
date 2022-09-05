using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPage : MonoBehaviour {

    [SerializeField] Canvas infoCanvas;

    public void OpenInfo() {
        infoCanvas.enabled = true;
    }

    public void CloseInfo() {
        infoCanvas.enabled = false;
    }

}
