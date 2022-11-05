using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static ScriptableObjectLevel currentLevel;
    public Image picture;
    public Image mask;
    public static LevelLoader instance;
    public Image checkSolved;
    private Sprite[] checkSprites;
    public Text description;
    private static int totalLevels;
    public static int TotalLevels { get { totalLevels = Resources.LoadAll<ScriptableObjectLevel>(LocalizationSettings.SelectedLocale.Formatter + "/Levels").Length; return totalLevels; } }

    public float timer;
    public float ratioUnblur;
    public float speed;
    /*private Material blurImage;
    private Material blurMask;*/
    public Text solution;
    public GameObject solveButton;
    public GameObject nextButtons;
    public LocalizeStringEvent title;
    private void Awake()
    {
        if (!instance)
            instance = this;
        totalLevels = TotalLevels;
        CheckLevelNull();
        checkSprites = Resources.LoadAll<Sprite>("Icons/right-wrong-icon");
    }
    void Start()
    {
        InitLevel();
    }

    internal static void LoadLevel(int levelId)
    {
        currentLevel = GetLevel(levelId);
    }

    internal static void LoadLevel(string levelId)
    {
        currentLevel = GetLevel(levelId);
    }
    public void InitLevel()
    {
        solution.transform.parent.gameObject.SetActive(false);
        description.text = currentLevel.description;
        picture.sprite = currentLevel.picture;
        solution.text = currentLevel.solution;
        timer = 60;
        ratioUnblur = 3;
        SetRadius();
        SetCheckSolved();
        solveButton.SetActive(true);
        nextButtons.SetActive(false);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", currentLevel.levelId);
        CheckLevelNull();
        if (currentLevel.levelId + 1 <= totalLevels)
        {
            currentLevel = GetLevel(currentLevel.levelId + 1);
        }
        else
        {
            GoLevels();
            return;
        }
        title.StringReference.RefreshString();
        InitLevel();
        speed = 3;
    }

    public void GoMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void CheckSolved()
    {
        if (timer % ratioUnblur >= 0 && timer % ratioUnblur < 0.3)
        {
            SetRadius();
        }
        if (timer >= 0 && timer <= 0.1)
        {
            solution.transform.parent.gameObject.SetActive(true);
            solveButton.SetActive(false);
            nextButtons.SetActive(true);
        }
    }

    public void Solve()
    {
        timer = 0;
    }

    private void Update()
    {
        CheckSolved();
        timer -= Time.deltaTime * speed;
        if (timer <= 15)
        {
            ratioUnblur = 0.01f;
            speed = 5;
        }

        if (Input.GetKey(KeyCode.Escape) && Application.platform == RuntimePlatform.Android)
        {
            GoMainMenu();
            return;
        }

    }

    public void SetRadius()
    {
        picture.material.SetFloat("_Radius", timer);
        mask.material.SetFloat("_Radius", timer);
    }

    // Only to be safe something ever loads
    void CheckLevelNull()
    {
        if (currentLevel == null)
        {
            Debug.LogWarning("Level was not loaded, then loading level 1");
            currentLevel = GetLevel(1);
        }
    }

    public void GoBack()
    {
        GoMainMenu();

        return;
    }

    public void GoLevels()
    {
        SceneManager.LoadScene("Levels");

        return;
    }

    public void Guessed()
    {
        LevelResolution(2);
    }

    public void NotGuessed()
    {
        LevelResolution(1);
    }

    public void LevelResolution(int result)
    {
        PlayerPrefs.SetInt(currentLevel.levelId.ToString("D3"), result);
        NextLevel();
    }

    public void SetCheckSolved()
    {
        int solved = PlayerPrefs.GetInt(currentLevel.levelId.ToString("D3"), 0);
        if (solved == 0)
            checkSolved.gameObject.SetActive(false);
        else
        {
            checkSolved.gameObject.SetActive(true);
            checkSolved.sprite = checkSprites[solved - 1];
        }
    }

    public static void ResetGame()
    {
        for (int i = 1; i <= TotalLevels; i++)
        {
            PlayerPrefs.SetInt(i.ToString("D3"), 0);
        }
        PlayerPrefs.SetInt("LastLevelPlayed", -1);
    }

    public static ScriptableObjectLevel GetLevel(int levelId)
    {
        return GetLevel("Level " + levelId.ToString("D3"));
    }
    public static ScriptableObjectLevel GetLevel(string levelId)
    {
        return (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter + "/Levels/" + levelId);
    }


}
