using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource forestSong;
    [SerializeField] private AudioSource caveSong;
    [SerializeField] private AudioSource castleSong;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "CaveLevel")
        {
            forestSong.Pause();
        }
        if (SceneManager.GetActiveScene().name == "CastleLevel")
        {
            caveSong.Pause();
        }
    }
}
