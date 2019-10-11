using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game_Timer : MonoBehaviour {

    [SerializeField]
    private Text uiText;
    private int timer = 0;
    private bool canCount = true;
    private bool doOnce = false;
    public Text highscore;
    public Text playerScore;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("Highscore") == true)
        {
            highscore.text = PlayerPrefs.GetInt("Highscore" + SceneManager.GetActiveScene().name).ToString();
        }
        else
        {
            highscore.text = "No Best Time Yet";
        }
        timer = 0;
        InvokeRepeating("IncrementTimer", 1, 1);
    }

    public void StopTimer()
    {
        CancelInvoke();
        if(playerScore !=null)
        playerScore.text = "Your time: "+timer;
        if (PlayerPrefs.GetInt("Highscore" + SceneManager.GetActiveScene().name, 0) < timer)
        {
            SetHighScore();
        }
    }
    public void SetHighScore()
    {
        Debug.Log("working");
        PlayerPrefs.SetInt("Highscore" + SceneManager.GetActiveScene().name, timer);
        highscore.text = PlayerPrefs.GetInt("Highscore" + SceneManager.GetActiveScene().name).ToString();
    }

    void IncrementTimer()
    {
            timer += 1;
            uiText.text = "Timer: " + timer;
    }
}
