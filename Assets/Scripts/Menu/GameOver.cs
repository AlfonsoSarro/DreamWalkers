using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu;
    public LifeSO lifes;

    // Update is called once per frame
    void Update()
    {
        if(lifes.Value == 0)
        {
            Time.timeScale = 0f;
            gameOverMenu.SetActive(true);
        }
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
