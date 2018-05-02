using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviourSingleton<UIController>
{
    private ScoreUIController scoreUI;
    private NextBlockUIController nextBlockUI;

    public ScoreUIController ScoreUI
    {
        get
        {
            if (scoreUI == null)
                scoreUI = GetComponentInChildren<ScoreUIController>();

            return scoreUI;
        }
    }

    public NextBlockUIController NextBlockUI
    {
        get
        {
            if (nextBlockUI == null)
                nextBlockUI = GetComponentInChildren<NextBlockUIController>();

            return nextBlockUI;
        }
    }
}
