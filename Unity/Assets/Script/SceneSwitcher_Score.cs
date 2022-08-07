using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher_Score : MonoBehaviour
{
    public void playgame()
    {
        SceneManager.LoadScene("Score");
    }
}
