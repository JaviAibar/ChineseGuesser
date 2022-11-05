using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject blackout;
    public GameObject continueButton;
    public TMPro.TMP_Dropdown languages;
    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += LanguagesChange;
    }


    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= LanguagesChange;
    }

    void Start()
    {
        CheckIfContinue();
        ClearScreen();
    }

    public void LanguagesChange(UnityEngine.Localization.Locale value)
    {
        CheckIfContinue();
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        LevelLoader.LoadLevel(1);
        LevelLoader.ResetGame();
        GoLevel();
    }

    public void Continue()
    {
        if (LevelLoader.currentLevel != null)
            GoLevel();
        else
            StartGame();
    }

    public void GoLevel()
    {
        SceneManager.LoadScene("Level");
    }


    public void CheckIfContinue()
    {
        int lastPlayed = PlayerPrefs.GetInt("LastLevelPlayed", -1);
        if (lastPlayed != -1)
        {
            string levelId = "Level " + ((lastPlayed < LevelLoader.TotalLevels) ? (lastPlayed + 1).ToString("D3") : "001");
            LevelLoader.LoadLevel(levelId);

            continueButton.SetActive(true);
        }
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("Levels");
    }

    public async void ClearScreen()
    {
        AsyncOperationHandle<LocalizationSettings> handle = LocalizationSettings.InitializationOperation;
        await handle.Task;
        //LocalizationSettings localizationSettings = handle.Result;
        blackout.SetActive(false);
    }

    
}
