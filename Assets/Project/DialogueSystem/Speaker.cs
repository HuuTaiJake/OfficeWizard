#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New speaker", menuName = "Dialogue/New Speaker")]
public class Speaker : ScriptableObject
{
    [SerializeField] private string _speakerName;
    [SerializeField] private Sprite _speakerSprite;


    public string GetName()
    {
        return _speakerName;
    }

    public Sprite GetSprite()
    {
        return _speakerSprite;
    }
}
