using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject youWonPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void YouWon()
    {
        youWonPanel.gameObject.SetActive(true);
            Debug.Log("You won! You have saved the cat! :)");
             Time.timeScale = 0;
    }
    public void GameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
            Debug.Log("Game Over! Zombie has got the cat! :(");
             Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
