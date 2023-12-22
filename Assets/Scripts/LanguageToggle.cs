using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;
using UnityEngine;

public class LanguageToggle : MonoBehaviour
{

    private bool active = false;

    public void ChangeLocale(bool toggleSpanish)
    {
        if (active == true)
            return;

        StartCoroutine(SetLocale((toggleSpanish) ? 1 : 0));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
    }
}
