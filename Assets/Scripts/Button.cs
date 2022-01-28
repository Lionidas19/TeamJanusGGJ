using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private string Game = "Game";
    [SerializeField] private string Credits = "Credits";

    public void StartGame()
    {
        SceneManager.LoadScene(Game);
    }

    public void StartCredits()
    {
        SceneManager.LoadScene(Credits);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
