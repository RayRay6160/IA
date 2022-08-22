using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileKeyBoard : MonoBehaviour
{
    TouchScreenKeyboard Type;
    public Text PIN;
    public Text Name;

    public void OpenKeyBoard()
    {
        Type = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

     void Update()
    {
       
    }
}
