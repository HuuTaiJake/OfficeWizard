using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public bool _isComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("QuestFindDiamond") == 4 && _isComplete == false)
        {
            _isComplete = true;
            QuestManager.Instance.enabled = false;
        }
    }
}
