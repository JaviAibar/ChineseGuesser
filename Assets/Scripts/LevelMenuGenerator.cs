using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LevelMenuGenerator : MonoBehaviour
{
    public GameObject levelsGrid;
    public GameObject levelButtonPrefab;
    public LocalizedStringTable table;
    void Start()
    {
        if (LevelLoader.totalLevels == 0)
        {
            LevelLoader.totalLevels = Resources.LoadAll<ScriptableObjectLevel>(LocalizationSettings.SelectedLocale.Formatter + "/Levels").Length;

        }
        for (int i = 0; i < LevelLoader.totalLevels; i++)
        {
            var button = Instantiate(levelButtonPrefab, levelsGrid.transform);
            button.name = LocalizationSettings.StringDatabase.GetLocalizedString("Menus", "Level") + " " + (i + 1).ToString("D3");
            button.GetComponentInChildren<Text>().text = button.name;
            button.GetComponent<Button>().onClick.AddListener(
                delegate {
                    LevelLoader.currentLevel = (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter+"/Levels/Level " + button.name.Split(" ")[1]); 
                    SceneManager.LoadScene("Level"); 
            });
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

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");

        return;
    }
}
