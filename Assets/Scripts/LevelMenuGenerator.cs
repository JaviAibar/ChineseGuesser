using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

public class LevelMenuGenerator : MonoBehaviour
{
    public GameObject levelsGrid;
    public GameObject levelButtonPrefab;
    public int totalLevels;
    public LocalizedStringTable table;
    private void Start()
    {
        //DirectoryInfo info = new DirectoryInfo("Resources/en/Levels");
        //var fileInfo = info.GetFiles("*.asset");
        /*foreach (FileInfo file in fileInfo)
        {
            var button = Instantiate(levelButtonPrefab, levelsGrid.transform);
            var name = Path.GetFileNameWithoutExtension(file.Name);
            button.name = name;
            button.GetComponentInChildren<Text>().text = name;
            button.GetComponent<Button>().onClick.AddListener(delegate { LevelLoader.currentLevel = (ScriptableObjectLevel)Resources.Load("Levels/"+name); SceneManager.LoadScene("Level"); });
        }*/
        for (int i = 0; i < totalLevels; i++)
        {
            var button = Instantiate(levelButtonPrefab, levelsGrid.transform);
            //button.name = LocalizationSettings.StringDatabase.GetLocalizedString("Menus", "Level");
            button.GetComponentInChildren<Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("Menus", "Level")+" " + (i + 1).ToString("D3") ;
            button.GetComponent<Button>().onClick.AddListener(delegate { LevelLoader.currentLevel = (ScriptableObjectLevel)Resources.Load("Levels/" + name); SceneManager.LoadScene("Level"); });
        }
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");

                return;
            }
        }
    }
}
