using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/New Level")]
public class ScriptableObjectLevel : ScriptableObject
{
    public int levelId;
    public string levelName;
    public Sprite picture;
    [MultilineAttribute]
    public string descrition;
    [MultilineAttribute]
    public string solution;
}