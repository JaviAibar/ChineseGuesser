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
   // public Text title;
    public float timer;
    public float speed = 2;
    private Material blurImage;
    private Material blurMask;
    public Text solution;
    public LocalizeStringEvent title;
    private void Awake()
    {
        CheckLevelNull();
        print(title);
    }
    void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        solution.transform.parent.gameObject.SetActive(false);
        print(LocalizationSettings.SelectedLocale.Formatter);
        print(currentLevel);
        print(currentLevel.description);
        description.text = currentLevel.description;
        picture.sprite = currentLevel.picture;
        solution.text = currentLevel.solution;
        timer = 60;
        SetRadius();
    }

    public void NextLevel()
    {
        CheckLevelNull();
        currentLevel = (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter + "/Levels/Level " + (currentLevel.levelId + 1).ToString("D3"));
        print("2" + title);
        title.StringReference.RefreshString();
        print("Trying to load " + LocalizationSettings.SelectedLocale.Formatter + "/Levels/Level " + (currentLevel.levelId + 1).ToString("D3"));
        LoadLevel();
    }

    public void Solve()
    {
        if (timer % 2 >= 0 && timer % 2 < 0.3)
        {
            SetRadius();
        }
        if (timer >= 0 && timer <= 0.1)
        {
            solution.transform.parent.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime * speed;
        Solve();


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

    void CheckLevelNull()
    {
        if (currentLevel == null) currentLevel = (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter + "/Levels/Level 001");
    }
}
