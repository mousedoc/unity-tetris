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
        Grid = new Block[GridInfo.Height, GridInfo.Width];

        for (int y = 0; y < GridInfo.Height; y++)
        {
            for (int x = 0; x < GridInfo.Width; x++)
            {
                InitializeBlock(x, y);
            }
        }
    }

    private void InitializeBlock(int x, int y)
    {
        var prefab = Resources.Load<Block>("Prefab/Ingame/Block");
        var block = Instantiate<Block>(prefab);
        block.transform.SetParent(GridTransform.transform);
        block.transform.localPosition = new Vector3(x, y, 0);
        block.transform.localRotation = Quaternion.identity;
        block.transform.localScale = Vector3.one;
        block.Initialize();

        Grid[y, x] = block;
    }

    private void Awake()
    {
        Initialize();

        StartGame();
    }

    private void Update()
    {

    }

    public void StartGame()
    {
        GameContext.Instance.Reset();

        StartCoroutine(BlockGroupRoutine());
    }

    public IEnumerator BlockGroupRoutine()
    {
        var group = BlockCreationService.GetRandomBlcokGroup ();
        currentDownTerm = 0.0f;

        if (group == null)
        {
            GameContext.Instance.IsGameover = true;
            yield break;
        }

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

        RemoveFinshedLine();

        StartCoroutine(BlockGroupRoutine());
    }
    
    private void UpdateBlockGroup(BlockGroup group)
    {
        #region Move
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            group.MoveToLowest();
            group.IsFixed = true;
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

    private void RemoveFinshedLine()
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

