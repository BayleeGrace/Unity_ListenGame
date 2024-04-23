using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressesPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject gameplayOptionsMenu;
    public GameObject volumeOptionsMenu;
    public GameObject videoOptionsMenu;

    public void Start()
    {
        CloseOptions();
    }

    public void ChangeToPauseMenu()
    {
        GameManager.instance.TogglePauseMenu();
    }

    public void ChangeToMainMenu()
    {
        GameManager.instance.ActivateMainMenu();
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
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
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
