using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameController : MonoBehaviourSingleton<IngameController>
{
    #region inspector

    public Transform GridTransform;

    public float currentDownTerm = 0.0f;

    #endregion

    public Block[,] Grid { get; private set; }

    private void Initialize()
    {
        CreateBackBoard();

        Grid = new Block[GridInfo.Height, GridInfo.Width];
        for (int y = 0; y < GridInfo.Height; y++)
        {
            for (int x = 0; x < GridInfo.Width; x++)
            {
                Grid[y, x] = CreateBlock(x, y);
            }
        }
    }

    private void Reset()
    {
        for (int y = 0; y < GridInfo.Height; y++)
        {
            for (int x = 0; x < GridInfo.Width; x++)
            {
                Grid[y, x].IsActive = false;
            }
        }
    }

    private void CreateBackBoard()
    {
        for (int y = -1; y <= GridInfo.Height; y++)
        {
            for (int x = -1; x <= GridInfo.Width; x++)
            {
                if (y == -1 || x == -1 || x == GridInfo.Width)
                {
                    CreateBlock(x, y, true);
                }
            }
        }
    }

    private Block CreateBlock(int x, int y, bool isWall = false)
    {
        var prefab = Resources.Load<Block>("Prefab/Ingame/Block");
        var block = Instantiate<Block>(prefab);
        block.transform.SetParent(GridTransform.transform);
        block.transform.localPosition = new Vector3(x, y, 0);
        block.transform.localRotation = Quaternion.identity;
        block.transform.localScale = Vector3.one;
        block.Initialize(isWall);

        return block;
    }

    private void Awake()
    {
        Initialize();

        StartGame();
    }

    public void StartGame(bool isRestart = false)
    {
        GameContext.Instance.Reset();

        if (isRestart)
            Reset();

        StartCoroutine(BlockGroupRoutine());
    }

    public IEnumerator BlockGroupRoutine()
    {
        var group = BlockCreationService.GetNextBlock ();
        currentDownTerm = 0.0f;

        if (GameContext.Instance.IsGameover || group == null)
            yield break;

        group.Initialize();
        
        while (group.IsFixed == false)
        {
            currentDownTerm += Time.deltaTime;
            if (currentDownTerm > GameContext.Instance.DownTerm)
            {
                currentDownTerm = 0.0f;
                group.Move(Vector2.down);
            }
            else
                UpdateBlockGroup(group);
            
            yield return null;
        }

        Debug.Log("End routine");
        CheckEraseLine();

        StartCoroutine(BlockGroupRoutine());
    }
    
    private void UpdateBlockGroup(BlockGroup group)
    {
        #region Move
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            group.MoveToLowest();
            group.Fix();
            return;
        }

        var direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction += Vector3.left;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            direction += Vector3.right;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            direction += Vector3.down;

        direction.Normalize();
        group.Move(direction);

        #endregion

        #region Rotation

        if (Input.GetKeyDown(KeyCode.Space))
            group.Rotate();

        #endregion
    }

    private void CheckEraseLine()
    {
        var finishedLine = 0;

        for (int y = 0; y < GridInfo.Height; y++)
        {
            var finished = true;

            for (int x = 0; x < GridInfo.Width; x++)
            {
                if (Grid[y, x].IsActive == false)
                {
                    finished = false;
                    break;
                }
            }

            if (finished)
            {
                for (int i = y + 1; i < GridInfo.Height; i++)
                {
                    for (int x = 0; x < GridInfo.Width; x++)
                    {
                        Grid[i - 1, x].Inherit(Grid[i, x]);
                    }
                }

                finishedLine++;
                y--;
            }
        }
    }
}

