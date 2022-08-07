using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher_InGame : MonoBehaviour
{
    public void playgame()
    {
        SceneManager.LoadScene("InGame");
    }
}
