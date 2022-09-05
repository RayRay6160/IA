using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour {

    public void LoadGuestScene() {
        SceneLoader.Instance.LoadGuestScene();
    }

    public void LoadHostScene() {
        SceneLoader.Instance.LoadHostScene();
    }

    public void MainMenu() {
        SceneLoader.Instance.LoadMainMenuScene();
    }

    public void LoadScoreScene() {
        SceneLoader.Instance.LoadScoreScene();
    }

    public void PlayAsHost() {
        SceneLoader.Instance.LoadPlayHostScene();
        Debug.Log("123");
    }

    public void PlayAsClient() {
        SceneLoader.Instance.LoadPlayClientScene();
        Debug.Log("12354");
    }

    public void Gameplay() {
        SceneLoader.Instance.LoadGameplayScene();
    }
}
