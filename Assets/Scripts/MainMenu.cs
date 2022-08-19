using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        LevelLoader.currentLevel = (ScriptableObjectLevel) Resources.Load(LocalizationSettings.SelectedLocale.Formatter + "/Levels/Level 001");
        SceneManager.LoadScene("Level");
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("Levels");
    }
}
