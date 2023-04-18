using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsEnded;

    public GameObject gameOverUI;
    public GameObject levelWonUI;
    public TextMeshProUGUI levelWonText;

    public string nextLevel = "Level02";
    public string menuScene = "MainMenu";
    public int levelToUnlock = 2;

    public int currentLevel = 0; 
    private int maxLevels = 2;

    private void Start()
    {
        GameIsEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsEnded)
            return;

        //if (Input.GetKeyDown(KeyCode.E) && PauseMenu.isPaused == false)
        //{
        //    EndGame();
        //}

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsEnded = true;
        gameOverUI.SetActive(true);
    }

    public IEnumerator OnWinLevel()
    {
        levelWonText.text = $"LEVEL {currentLevel} COMPLETED!";
        levelWonUI.SetActive(true);

        yield return new WaitForSeconds(3);

        levelWonUI.SetActive(false);

        if (maxLevels > currentLevel)
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            SceneManager.LoadScene(menuScene);
        }
    }
}
