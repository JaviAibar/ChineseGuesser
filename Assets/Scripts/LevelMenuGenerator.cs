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
    private Sprite[] checkSprites;
    public int totalLevels;
    private void Awake()
    {
        totalLevels = LevelLoader.totalLevels;

    }
    void Start()
    {
        checkSprites = Resources.LoadAll<Sprite>("Icons/right-wrong-icon");
        if (LevelLoader.totalLevels == 0)
        {
            LevelLoader.totalLevels = Resources.LoadAll<ScriptableObjectLevel>(LocalizationSettings.SelectedLocale.Formatter + "/Levels").Length;

        }
        for (int i = 0; i < LevelLoader.totalLevels; i++)
        {
            var button = Instantiate(levelButtonPrefab, levelsGrid.transform);
            button.name = LocalizationSettings.StringDatabase.GetLocalizedString("Menus", "Level") + " " + (i + 1).ToString("D3");
            button.GetComponentInChildren<Text>().text = (i + 1).ToString("D3");
            ScriptableObjectLevel level = (ScriptableObjectLevel)Resources.Load(LocalizationSettings.SelectedLocale.Formatter + "/Levels/Level " + (i + 1).ToString("D3"));
            Image checkedImage = button.transform.GetChild(1).GetComponent<Image>();
            int solved = PlayerPrefs.GetInt((i + 1).ToString("D3"), 0);

            if (solved == 0) {
                checkedImage.gameObject.SetActive(false);
            } else {
                checkedImage.sprite = checkSprites[solved - 1];
            }

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
