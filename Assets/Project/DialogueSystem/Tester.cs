using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public Conversation convo;

    private void OnEnable()
    {
        StartConvo();
    }
    public void StartConvo()
    {
        DialogueManager.Instance.StartConversation(convo);
    }
}
