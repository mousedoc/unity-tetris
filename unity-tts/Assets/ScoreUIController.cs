using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour
{
    private Text text;

    private int Score { set { text.text = value.ToString(); } }

    private void Awake()
    {
        text = GetComponent<Text>();
        Score = 0;
    }

    public void ChangeScore(int score)
    {
        Score = score;
    }
}
