using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private string Light3 = "Light3";
    [SerializeField] private string Credits = "Credits";

    public void StartGame()
    {
        LightOrDark.light = true;
        SceneManager.LoadScene(Light3);
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
