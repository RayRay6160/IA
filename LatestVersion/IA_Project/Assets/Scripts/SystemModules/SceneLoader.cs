using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : PersistentSingleton<SceneLoader> {

    [SerializeField] Image blackMask;
    [SerializeField] Image tutorialImage;
    [SerializeField] float fadeTime = 3.5f;

    Color blackColor = new Color(0, 0, 0, 0);
    Color clearColor = new Color(1, 1, 1, 0);
    Color color;

    #region SceneName

    const string MainMenu = "MainMenu";
    const string PlayAsHost = "PlayAsHost";
    const string PlayAsClient = "PlayAsClient";
    const string GuestRoomPage = "GuestRoom";
    const string HostRoomPage = "HostRoom";
    const string ScorePage = "Score";
    const string Gameplay = "Gameplay";

    #endregion

    void Load(string sceneName) {
        Debug.Log("load " + sceneName);
        SceneManager.LoadScene(sceneName);
        if (sceneName == "HostRoom") {
            Debug.Log("111");
            SceneLoader.Instance.LoadHostScene();
        }
    }

    IEnumerator LoadStartCoroutine(string sceneName) {

        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;

        blackMask.gameObject.SetActive(true);

        color = blackColor;

        while (color.a < 1f) {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            blackMask.color = color;

            yield return null;
        }

        tutorialImage.gameObject.SetActive(true);
        color = clearColor;

        Debug.Log("StartFadeInTurImage");

        while (color.a < 1f) {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            tutorialImage.color = color;

            yield return null;
        }

        Load(sceneName);
        loadingOperation.allowSceneActivation = true;

        LoadSceneDone();
    }

    public void LoadSceneDone() {
        AudioManager.Instance.PlayMusic(false);
        StartCoroutine(LoadDoneCoroutine());
    }


    IEnumerator LoadDoneCoroutine() {
        //Remove after having scriptiDoneLoad


        while (color.a > 0f) {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            tutorialImage.color = color;

            yield return null;

        }

        color = blackColor;
        color.a = 1;

        tutorialImage.gameObject.SetActive(false);

        while (color.a > 0f) {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            blackMask.color = color;

            yield return null;
        }
        blackMask.gameObject.SetActive(false);
    }

    #region Button

    public void LoadGuestScene() {
        Load(GuestRoomPage);
    }

    public void LoadHostScene() {
        Load(HostRoomPage);
    }

    public void LoadMainMenuScene() {
        Load(MainMenu);
    }

    public void LoadScoreScene() {
        Load(ScorePage);
    }

    public void LoadPlayHostScene() {
        Load(PlayAsHost);
    }

    public void LoadPlayClientScene() {
        Load(PlayAsClient);
    }

    public void LoadGameplayScene() {
        StartCoroutine(LoadStartCoroutine(Gameplay));
    }

    #endregion

}