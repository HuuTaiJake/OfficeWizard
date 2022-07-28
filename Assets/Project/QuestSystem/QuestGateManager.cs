using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class QuestGateManager : MonoBehaviour
{
    public string sceneToLoad;
    public Conversation convo;
    public Vector3 playerPosition;
    public Transform playerReturnPosition;
    public VectorValue playerStorage;

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (QuestManager.Instance._isComplete)
            {
                //playerStorage.initialValue = playerPosition;
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player").transform.DOMove(playerReturnPosition.position, 1f);
                StartConvo();
            }
            

        }
    }

    public void StartConvo()
    {
        DialogueManager.Instance.StartConversation(convo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
