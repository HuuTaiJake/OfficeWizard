using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] private DialogueLine[] _allLines;

    public DialogueLine GetLineByIndex(int index)
    {
        return _allLines[index];
    }

    public int GetLength()
    {
        return _allLines.Length - 1;
    }
}
