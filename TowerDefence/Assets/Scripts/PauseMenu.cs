using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public string sceneToLoad = "MainMenu";
    public static bool isPaused = false;

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && GameManager.GameIsEnded == false)
        {
            ToogleMenu();
        }
    }

    public void ToogleMenu()
    {
        menu.SetActive(!menu.activeSelf);
        isPaused = !isPaused;

        if (menu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Restart()
    {
        ToogleMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }
}
