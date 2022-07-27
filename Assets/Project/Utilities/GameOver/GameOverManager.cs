using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public bool gameHasEnded = false;
    public GameObject player;
    public GameObject gameOverUI;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        if (player.GetComponent<CreatureAttribute>().currentHealth == 0 || Input.GetKeyDown(KeyCode.Space))
        {
            EndGame();
        }
        
    }
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            PlayerPrefs.SetInt("QuestFindDiamond", 0);
            gameOverUI.SetActive(true);
            gameHasEnded = true;
            Debug.Log("Game Over");
        }
        
    }
    

    public void RestartMapButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
