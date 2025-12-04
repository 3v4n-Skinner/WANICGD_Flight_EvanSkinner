using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Score : MonoBehaviour
{
    public static UI_Score Instance;

    [SerializeField] TextMeshProUGUI ScoreDisplay;
    int Score;


    public int GetScore => Score;
    public int AddScore(int value)
    {
        Score += value;
        UpdateScore(Score);
        return Score;
    }
    public void UpdateScore(int value)
    {
        ScoreDisplay.text = "Score: "+ value.ToString();
    }

    private void Awake()
    {
        Instance = this;
    }
}
