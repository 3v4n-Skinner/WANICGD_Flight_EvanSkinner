using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class: J_GameManager
 * Original Author: Zev S.
 * Contributers: [Your Name]
 * Created: 11/27/24
 * Last Modified: 12/2/2024
 * 
 * Purpose: Manages Jumper game. Predominently handles scrore and endstates.
 */


public delegate void Notify();
public class J_GameManager : MonoBehaviour
{
    public static J_GameManager instance;

    /// <summary>
    /// Event triggered when player loses
    /// </summary>
    public static event Notify OnLose;
    /// <summary>
    /// Event triggered when player wins
    /// </summary>
    public static event Notify OnWin;


    //Score Code
    [Tooltip("The Key for HighScore within Playerprefs")]
    [SerializeField] string HighScoreKey;
    /// <summary>
    /// Highest score achived, utilizes player pref to save between sessions.
    /// </summary>
    public static int HighScore;
    /// <summary>
    /// Event for every time score changes
    /// </summary>
    public static event Notify OnScoreUpdate;

    /// <summary>
    /// Current score of run
    /// </summary>
    public static int Score
    {
        get { return instance._score; }
        set { instance._score = value; OnScoreUpdate?.Invoke(); }
    }
    private int _score;


    [Tooltip("Object refrence for Lose LoseCanvas")]
    public WinCanvas LoseCanvas;

    [Tooltip("Object refrence for Win LoseCanvas")]
    public WinCanvas WinCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //Loads HighScore from playerprefs
        HighScore = PlayerPrefs.GetInt(HighScoreKey);
        Debug.Log($"Loading High Score: {HighScore}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Triggers endstate of game.
    /// </summary>
    /// <returns>Returns true on win and false on loss</returns>
    public static bool GameOver()
    {
        Debug.Log($"Triggering Enstate.\nScore: {Score}\nHigh Score:{HighScore}");

        //Pauses game
        Time.timeScale = 0f;


        if (Score > HighScore)
        {
            Win();
            return true;
        }
        else 
        {
            Lose();
            return false;
        }
    }

    /// <summary>
    /// Called on GameOver when a new high score is not achieved
    /// </summary>
    public static void Lose()
    {
        instance.LoseCanvas.gameObject.SetActive(true);
        instance.LoseCanvas.SetText(HighScore, Score);

        OnLose?.Invoke();
    }

    /// <summary>
    /// Called on game over when a new Highscore is achived
    /// </summary>
    public static void Win()
    {
        instance.WinCanvas.gameObject.SetActive(true);
        instance.WinCanvas.SetText(HighScore, Score);

        HighScore = Score;
        PlayerPrefs.SetInt(instance.HighScoreKey, HighScore);
        OnWin?.Invoke();
    }

}
