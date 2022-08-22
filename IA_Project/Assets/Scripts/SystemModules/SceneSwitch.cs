using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{

    const string GuestRoomPage = "Guest";
    const string HostRoomPage = "Host";
    const string MainMenu = "MainMenu";
    const string ScorePage = "Score";
    const string Gameplay = "Gameplay";
    const string RoomEnter = "RoomEnter";


    public void LoadGuestScene()
    {
        SceneManager.LoadScene(GuestRoomPage);
    }

    public void LoadHostScene()
    {
        SceneManager.LoadScene(HostRoomPage);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void LoadScoreScene()
    {
        SceneManager.LoadScene(ScorePage);
    }

    public void LoadRoomEnterScene()
    {
        SceneManager.LoadScene(RoomEnter);
    }

    public void LoadInGameScene()
    {
        SceneManager.LoadScene(Gameplay);
    }
}
