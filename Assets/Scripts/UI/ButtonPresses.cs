using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPresses : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject gameplayOptionsMenu;
    public GameObject volumeOptionsMenu;
    public GameObject videoOptionsMenu;
    public GameObject gameSettingsMenu;

    public void Start()
    {
        OpenMainMenuScreen();
    }

    public void ChangeToGameplay()
    {
        GameManager.instance.ActivateGameplay();
    }

    public void ChangeToMainMenu()
    {
        GameManager.instance.ActivateMainMenu();
    }

    // TODO: Create a title screen and 
    public void OpenTitleScreen()
    {
        
    }

    public void OpenMainMenuScreen()
    {
        mainMenu.SetActive(true);
        gameSettingsMenu.SetActive(false);
        CloseOptions();
    }

    public void OpenGameSettings()
    {
        mainMenu.SetActive(false);
        gameSettingsMenu.SetActive(true);
    }

    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        gameplayOptionsMenu.SetActive(true);
        volumeOptionsMenu.SetActive(false);
        videoOptionsMenu.SetActive(false);
    }

    public void ChangeToGamePlayOptions()
    {
        gameplayOptionsMenu.SetActive(true);
        volumeOptionsMenu.SetActive(false);
        videoOptionsMenu.SetActive(false);
    }

    public void ChangeToVolumeOptions()
    {
        gameplayOptionsMenu.SetActive(false);
        volumeOptionsMenu.SetActive(true);
        videoOptionsMenu.SetActive(false);
    }

    public void ChangeToVideoOptions()
    {
        gameplayOptionsMenu.SetActive(false);
        volumeOptionsMenu.SetActive(false);
        videoOptionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
