using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject exitButton;
    public GameObject returnButton;
    public GameObject volumeButton;
    public GameObject slider;
    
    public void GameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    

    public void OptionsMenu()
    {
        playButton.SetActive(false);
        optionsButton.SetActive(false);
        exitButton.SetActive(false);
        returnButton.SetActive(true);
        volumeButton.SetActive(true);
        slider.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Return()
    {
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        exitButton.SetActive(true);
        returnButton.SetActive(false);
        volumeButton.SetActive(false);
        slider.SetActive(false);
    }

}
    