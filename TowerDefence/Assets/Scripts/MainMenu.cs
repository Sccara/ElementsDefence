using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Level01";
    public string levelSelectionScene = "LevelSelect";

    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene(levelSelectionScene);
    }

    public void Quit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
