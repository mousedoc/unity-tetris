using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockCreationService
{
    private static IngameController IngameController { get { return IngameController.Instance; } }

    public static BlockGroupInfo NextBlockGroup = null;

    static BlockCreationService()
    {
        NextBlockGroup = BlockGroupInfo.GetRandomBlock();
    }

    public static BlockGroup GetNextBlock()
    {
        var type = EnumExtension.GetRandom<BlockShapeType>();
        var group = CreateBlockGroup(NextBlockGroup.GroupType, NextBlockGroup.ColorType);
        NextBlockGroup = BlockGroupInfo.GetRandomBlock();

        return group;
    }

    public static BlockGroup CreateBlockGroup(BlockShapeType shape, BlockColorType color)
    {
        BlockGroup group = null;
        var posList = new List<Vector2>();
        var pivotIndex = 0;

        switch (shape)
        {
            case BlockShapeType.I:
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(0, -2));
                posList.Add(new Vector2(0, -3));
                pivotIndex = 1;
                break;

            case BlockShapeType.J:
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(0, -2));
                posList.Add(new Vector2(-1, -2));
                pivotIndex = 1;
                break;

            case BlockShapeType.L:
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(0, -2));
                posList.Add(new Vector2(1, -2));
                pivotIndex = 1;
                break;

            case BlockShapeType.O:
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(1, 0));
                posList.Add(new Vector2(1, -1));
                pivotIndex = 1;
                break;

            case BlockShapeType.S:
                posList.Add(new Vector2(-1, -1));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(1, 0));
                pivotIndex = 2;
                break;

            case BlockShapeType.Z:
                posList.Add(new Vector2(-1, 0));
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(1, -1));
                pivotIndex = 1;
                break;

            case BlockShapeType.T:
                posList.Add(new Vector2(-1, 0));
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(1, 0));
                posList.Add(new Vector2(0, -1));
                pivotIndex = 1;
                break;
        }

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

