using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : Singleton<GameContext>
{
    public int Score;

    public float DownTerm;      // Seconds

    public bool IsGameover;

    public void Reset()
    {
        IsGameover = false;

        DownTerm = 1;

        Score = 0;
    }
}
