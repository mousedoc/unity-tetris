using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour
{
    public Text Text
    {
        get { return GetComponent<Text>(); }
    }

    private int Score { set { Text.text = value.ToString(); } }

    public void ChangeScore(int score)
    {
        Score = score;
    }
}
