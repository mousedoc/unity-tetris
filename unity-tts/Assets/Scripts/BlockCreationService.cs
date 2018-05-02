using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockCreationService
{
    private static IngameController IngameController { get { return IngameController.Instance; } }

    public static BlockGroup GetNextBlock()
    {
        var group = CreateBlockGroup(GameContext.Instance.NextBlockGroup.GroupType, GameContext.Instance.NextBlockGroup.ColorType);
        GameContext.Instance.ChangeNextBlock();

        return group;
    }

    public static BlockGroup CreateBlockGroup(BlockShapeType shape, BlockColorType color)
    {
        BlockGroup group = null;
        var posList = GetBlockLocalPosList(shape);
        var pivotIndex = GetDefaultPivotIndex(shape);

        if (CheckAbleCreateBlock(posList) == false)
        {
            GameContext.Instance.IsGameover = true;

            var grid = IngameController.Instance.Grid;
            foreach (var pos in posList.LocalToWorld())
            {
                var block = grid[pos.GetIntY(), pos.GetIntX()];

                if (block.IsActive)
                    continue;

                block.Color = color.ToColor();
                block.IsActive = true;
            }
        }
        else
        {
            group = new BlockGroup(shape,
               posList.LocalToWorld(),
               pivotIndex,
               color.ToColor());
        }

        return group;
    }

    public static List<Vector2> GetBlockLocalPosList(BlockShapeType shape)
    {
        var list = new List<Vector2>();

        switch (shape)
        {
            case BlockShapeType.I:
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(0, -1));
                list.Add(new Vector2(0, -2));
                list.Add(new Vector2(0, -3));
                break;

            case BlockShapeType.J:
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(1, -1));
                list.Add(new Vector2(1, -2));
                list.Add(new Vector2(0, -2));
                break;

            case BlockShapeType.L:
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(0, -1));
                list.Add(new Vector2(0, -2));
                list.Add(new Vector2(1, -2));
                break;

            case BlockShapeType.O:
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(0, -1));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(1, -1));
                break;

            case BlockShapeType.S:
                list.Add(new Vector2(-1, -1));
                list.Add(new Vector2(0, -1));
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(1, 0));
                break;

            case BlockShapeType.Z:
                list.Add(new Vector2(-1, 0));
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(0, -1));
                list.Add(new Vector2(1, -1));
                break;

            case BlockShapeType.T:
                list.Add(new Vector2(-1, 0));
                list.Add(new Vector2(0, 0));
                list.Add(new Vector2(1, 0));
                list.Add(new Vector2(0, -1));
                break;
        }

        return list;
    }

    public static int GetDefaultPivotIndex(BlockShapeType shape)
    {
        switch (shape)
        {
            case BlockShapeType.I:
                return 1;

            case BlockShapeType.J:
                return 1;

            case BlockShapeType.L:
                return 1;

            case BlockShapeType.O:
                return 1;

            case BlockShapeType.S:
                return 2;

            case BlockShapeType.Z:
                return 1;

            case BlockShapeType.T:
                return 1;
        }

        Debug.LogError("GetDefaultPivotIndex() - Argument exception");
        return 0;
    }

    private static bool CheckAbleCreateBlock(List<Vector2> posList)
    {
        if (posList.Count <= 0)
            return false;

        foreach (var pos in posList)
        {
            var worldPos = pos.LocalToWorld();

            if (IngameController.Grid[(int)worldPos.y, (int)worldPos.x].IsActive)
                return false;
        }

        return true;
    }
}

