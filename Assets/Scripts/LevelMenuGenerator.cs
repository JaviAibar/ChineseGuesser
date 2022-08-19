using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public class LevelMenuGenerator : MonoBehaviour
{
    public GameObject levelsGrid;
    public GameObject levelButtonPrefab;
    private void Start()
    {
        DirectoryInfo info = new DirectoryInfo("Assets/Resources/en/Levels");
        var fileInfo = info.GetFiles("*.asset");
        foreach (FileInfo file in fileInfo)
        {
            var button = Instantiate(levelButtonPrefab, levelsGrid.transform);
            var name = Path.GetFileNameWithoutExtension(file.Name);
            button.name = name;
            button.GetComponentInChildren<Text>().text = name;
            button.GetComponent<Button>().onClick.AddListener(delegate { LevelLoader.currentLevel = (ScriptableObjectLevel)Resources.Load("Levels/"+name); SceneManager.LoadScene("Level"); });
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
