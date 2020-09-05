using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Line
{
    public bool leftChar;
    public Sprite portrait;
    public string name;
    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public Line[] lines;
}
