using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public static AudioClip themeSong;
    static AudioSource audioSource;

    void Start()
    {
        themeSong = Resources.Load<AudioClip>("digdug_theme.mp3");
        audioSource = GetComponent<AudioSource>();
        playTheme();

    }

    public static void playTheme()
    {
        audioSource.PlayOneShot(themeSong);
    }

    public static void startGame()
    {
        SceneManager.LoadScene(1);
        PlayerController.gameReset();
    }

    public static void endGame()
    {
        SceneManager.LoadScene(0);
    }

    public static void gameOver()
    {
        SceneManager.LoadScene(3);
    }

    public static void youWin()
    {
        SceneManager.LoadScene(2);
    }

}
