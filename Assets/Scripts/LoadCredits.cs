using LogicUI.FancyTextRendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCredits : MonoBehaviour
{
    public MarkdownRenderer markdownRenderer;
    // Start is called before the first frame update
   
    void Start()
    {
        markdownRenderer.Source = LoadCreditsText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string LoadCreditsText()
    {
        TextAsset mytxtData = (TextAsset)Resources.Load("CREDITS");
        return mytxtData.text;
    }
}
