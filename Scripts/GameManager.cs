using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UI_Script uiScript;
    public GameObject player;
    public bool pauseGame;
    public bool hasPressedContinue;
    public bool isTouchingGoal;

    private void Start()
    {
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
        pauseGame = false;
        hasPressedContinue = false;
        isTouchingGoal = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isTouchingGoal || Input.GetKeyDown(KeyCode.E) && !isTouchingGoal)
        {
            PauseMenu();
        }
        if (pauseGame == true)
        {
            Time.timeScale = 0;
        }
        else if (pauseGame == false)
        {
            Time.timeScale = 1;
        }
    }

    public void PauseMenu()
    {
        pauseGame = true;
        hasPressedContinue = false;
        uiScript.PauseMenu();
    }

    public void Victory()
    {
        pauseGame = true;
        uiScript.Victory();
    }

    public void GameOver()
    {
        player.SetActive(false);
        pauseGame = true;
        uiScript.GameOver();
    }

    public void ContinueButton()
    {
        hasPressedContinue = true;
        pauseGame = false;
        uiScript.ContinueButton();
    }
    public void RestartButton()
    {
        player.SetActive(true);
        uiScript.RestartButton();
    }

    public void QuitButton()
    {
        uiScript.QuitToMenu();
    }
}
