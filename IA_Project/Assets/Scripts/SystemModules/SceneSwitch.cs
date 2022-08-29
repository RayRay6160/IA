using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    public void LoadGuestScene()
    {
        SceneLoader.Instance.LoadGuestScene();
    }

    public void LoadHostScene()
    {
        SceneLoader.Instance.LoadHostScene();
    }

    public void LoadMainMenuScene()
    {
        SceneLoader.Instance.LoadMainMenuScene();
    }

    public void LoadScoreScene()
    {
        SceneLoader.Instance.LoadScoreScene();
    }

    public void LoadRoomEnterScene()
    {
        SceneLoader.Instance.LoadRoomEnterScene();
    }

    public void LoadRoomEnterHostScene()
    {
        SceneLoader.Instance.LoadRoomEnterHostScene();
    }

    public void LoadInGameScene()
    {
        SceneLoader.Instance.LoadInGameScene();
    }
}
