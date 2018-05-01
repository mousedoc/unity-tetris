using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : Singleton<GameContext>
{
    public int Score;

    public float DownTerm;      // Seconds

    private bool isGameover;

    public bool IsGameover
    {
        get { return isGameover; }
        set
        {
            isGameover = value;

            if (isGameover)
            {
                var restart = new DialogDataConfirm("Game Over", Score.ToString(), "Restart", () =>
                {
                    IngameController.Instance.StartGame(true);
                });
                DialogManager.Instance.Push(restart);
            }
        }
    }

    public void Reset()
    {
        IsGameover = false;
        DownTerm = 1;
        Score = 0;
    }
}
