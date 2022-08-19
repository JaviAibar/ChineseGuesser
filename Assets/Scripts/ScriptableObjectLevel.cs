using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/New Level")]
public class ScriptableObjectLevel : ScriptableObject
{
    public int levelId;
    public Sprite picture;
    [MultilineAttribute]
    public string description;
    [MultilineAttribute]
    public string solution;
}