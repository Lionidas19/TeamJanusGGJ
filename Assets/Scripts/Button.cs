using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    [SerializeField] private string scene = "Light";
    [SerializeField] private string Credits = "Credits";

    public void StartGame()
    {
        LightOrDark.light = true;
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string light)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(light);
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
