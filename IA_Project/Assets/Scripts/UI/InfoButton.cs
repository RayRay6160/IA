using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
   public GameObject MainCanvas;
   public GameObject InfoCanvas;

     public void infoButtonPressed()
     {
        MainCanvas.SetActive(false);
        InfoCanvas.SetActive(true);
     }
    public void returnButtonPressed()
    {
        InfoCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }
}
