using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : Singleton<GameContext>
{
    public int Score { get; private set; }

    public BlockGroupInfo NextBlockGroup { get; private set; }

    public float DownTerm { get; private set; }

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
        ChangeScore(0);
        NextBlockGroup = BlockGroupInfo.GetRandomBlock();
    }

    public void ChangeNextBlock()
    {
        GameContext.Instance.NextBlockGroup = BlockGroupInfo.GetRandomBlock();
        UIController.Instance.NextBlockUI.ChangeNextBlockGroup(GameContext.Instance.NextBlockGroup);
    }

    private void ChangeScore(int score)
    {
        UIController.Instance.ScoreUI.ChangeScore(Score = score);

        var speed = GameContext.Instance.Score / 10000 * 0.05f;
        GameContext.Instance.ChangeDownterm(1.0f - speed);

        if (GameContext.Instance.DownTerm < 0.1f)
            GameContext.Instance.ChangeDownterm(0.1f);
    }

    public void AddScore(int score)
    {
        ChangeScore(Score + score);
    }

    public void ChangeDownterm(float term)
    {
        DownTerm = term;
    }
}
