using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject blackout;
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

    private void Start()
    {
        ClearScreen();
    }

    public async void ClearScreen()
    {
        AsyncOperationHandle<LocalizationSettings> handle = LocalizationSettings.InitializationOperation;
        await handle.Task;
        LocalizationSettings localizationSettings = handle.Result;
        blackout.SetActive(false);
    }
}
