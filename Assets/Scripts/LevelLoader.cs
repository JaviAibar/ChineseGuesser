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
    public Text description;
    public static int totalLevels;
    // public Text title;
    public float timer;
    public float speed;
    private Material blurImage;
    private Material blurMask;
    public Text solution;
    public GameObject solveButton;
    public LocalizeStringEvent title;
    private void Awake()
    {
        CheckLevelNull();
        totalLevels = Resources.LoadAll<ScriptableObjectLevel>(LocalizationSettings.SelectedLocale.Formatter + "/Levels").Length;
    }
    void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        solution.transform.parent.gameObject.SetActive(false);
        description.text = currentLevel.description;
        picture.sprite = currentLevel.picture;
        solution.text = currentLevel.solution;
        timer = 60;
        SetRadius();
    }

    public void NextLevel()
    {
        CheckLevelNull();
        if (currentLevel.levelId + 1 <= totalLevels) {
            currentLevel = (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter + "/Levels/Level " + (currentLevel.levelId + 1).ToString("D3"));
        } else
        {
            GoLevels();
            return;
        }
        title.StringReference.RefreshString();
        LoadLevel();
        speed = 3;
        solveButton.SetActive(true);
    }

    public void CheckSolved()
    {
        if (timer % 3 >= 0 && timer % 3 < 0.3)
        {
            SetRadius();
        }
        if (timer >= 0 && timer <= 0.1)
        {
            solution.transform.parent.gameObject.SetActive(true);
            solveButton.SetActive(false);
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
            speed = 5;
        }

        if (Input.GetKey(KeyCode.Escape) && Application.platform == RuntimePlatform.Android)
        {
            SceneManager.LoadScene("MainMenu");

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
        if (currentLevel == null) currentLevel = (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter + "/Levels/Level 001");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");

        return;
    }

    public void GoLevels()
    {
        SceneManager.LoadScene("Levels");

        return;
    }
}
