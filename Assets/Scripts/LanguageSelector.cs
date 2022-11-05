using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }
    IEnumerator Start()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        // Old way with DropDown (no TMP)
        // Generate list of available Locales
        /* var options = new List<Dropdown.OptionData>();
         int selected = 0;
         for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
         {
             var locale = LocalizationSettings.AvailableLocales.Locales[i];
             if (LocalizationSettings.SelectedLocale == locale)
                 selected = i;

             options.Add(new Dropdown.OptionData(locale.name.Split(' ')[0]));
         }

         dropdown.options = options;
         dropdown.value = selected;
         dropdown.onValueChanged.AddListener(LocaleSelected);*/

        // New TMPDropDown
        var options = new List<TMP_Dropdown.OptionData>();
        int selected = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selected = i;
            string localeName = locale.name.Split(' ')[0];

            options.Add(new TMP_Dropdown.OptionData(locale.name.Split(' ')[0] + " - " + LocalizationSettings.StringDatabase.GetLocalizedString("Menus", localeName)));
        }

        dropdown.options = options;
        dropdown.value = selected;
        dropdown.onValueChanged.AddListener(LocaleSelected);
    }



    static void LocaleSelected(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}