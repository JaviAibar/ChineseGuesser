using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static ScriptableObjectLevel currentLevel;
    public Image picture;
    public Image mask;
    public Text description;
    public Text title;
    public float timer;
    public float speed = 2;
    private Material blurImage;
    private Material blurMask;
    public Text solution;

    void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        solution.transform.parent.gameObject.SetActive(false);
        print(LocalizationSettings.SelectedLocale.Formatter);
        if (currentLevel == null) currentLevel = (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter+"/Levels/Level 001");
        print(currentLevel);
        print(currentLevel.description);
        description.text = currentLevel.description;
        picture.sprite = currentLevel.picture;
        title.text = currentLevel.levelName;
        solution.text = currentLevel.solution;
        timer = 30;
        SetRadius();
    }

    public void NextLevel()
    {
        currentLevel = (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter+"/Levels/Level " + (currentLevel.levelId + 1));
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
}
