using LogicUI.FancyTextRendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCredits : MonoBehaviour
{
    public MarkdownRenderer markdownRenderer;

    void Start()
    {
        markdownRenderer.Source = LoadCreditsText();
    }

    public string LoadCreditsText()
    {
        TextAsset mytxtData = (TextAsset)Resources.Load("CREDITS");
        return mytxtData.text;
    }
}
