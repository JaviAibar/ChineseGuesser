using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;

public class LevelMenuGenerator : MonoBehaviour
{
    public GameObject levelsGrid;
    public GameObject levelButtonPrefab;
    public LocalizedStringTable table;
    private Sprite[] checkSprites;
    public int totalLevels;
    public int totalLevelsSolved;
    public LocalizeStringEvent levelsSolvedText;

    private void Awake()
    {
        totalLevelsSolved = 0;
    }

    void Start()
    {
        checkSprites = Resources.LoadAll<Sprite>("Icons/right-wrong-icon");
        totalLevels = LevelLoader.TotalLevels;

        for (int i = 0; i < totalLevels; i++)
        {
            InstantiateButton((i + 1).ToString("D3"));
        }
        levelsSolvedText.RefreshString();
    }

    public void InstantiateButton(string levelId)
    {
        var button = Instantiate(levelButtonPrefab, levelsGrid.transform);
        button.name = LocalizationSettings.StringDatabase.GetLocalizedString("Menus", "Level") + " " + levelId;
        button.GetComponentInChildren<TMPro.TMP_Text>().text = levelId;

        ScriptableObjectLevel level = LevelLoader.GetLevel(levelId);

        int solved = PlayerPrefs.GetInt(levelId, 0);
        totalLevelsSolved += solved == 2 ? 1 : 0;

        Image checkedImage = button.transform.GetChild(0).GetComponent<Image>();
        // if level already played, set the corresponding sprite, otherwise, disable Image
        if (solved == 0)
            checkedImage.gameObject.SetActive(false);
        else
            checkedImage.sprite = checkSprites[solved - 1];

        button.GetComponent<Button>().onClick.AddListener(
            delegate {
                LevelLoader.LoadLevel("Level "+button.name.Split(" ")[1]);
                GoLevel();
            });
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                GoMainMenu();

                return;
            }
        }
    }

    public void GoLevel()
    {
        SceneManager.LoadScene("Level");
    }
 
    public void GoBack()
    {
        GoMainMenu();

        return;
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
