using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher_Guest : MonoBehaviour
{
    public void playgame()
    {
        SceneManager.LoadScene("Guest");
    }
}
