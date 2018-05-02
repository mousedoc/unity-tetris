using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviourSingleton<UIController>
{
    public ScoreUIController ScoreUI { get; private set; }
    public NextBlockUIController NextBlockUI { get; private set; }

    private void Awake()
    {
        ScoreUI = GetComponentInChildren<ScoreUIController>();
        NextBlockUI = GetComponentInChildren<NextBlockUIController>();
    }
}
