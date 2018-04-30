using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockCreationService
{
    private static IngameController IngameController { get { return IngameController.Instance; } }

    public static BlockGroup GetRandomBlcokGroup()
    {
        var type = EnumExtension.GetRandom<BlockGroupType>();
        var group = CreateBlockGroup(type);

        return group;
    }

    public static BlockGroup CreateBlockGroup(BlockGroupType type)
    {
        BlockGroup group = null;
        var posList = new List<Vector2>();
        var pivotIndex = 0;

        switch (type)
        {
            case BlockGroupType.I:
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(0, -2));
                posList.Add(new Vector2(0, -3));
                pivotIndex = 1;
                break;

            case BlockGroupType.J:
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(0, -2));
                posList.Add(new Vector2(-1, -2));
                pivotIndex = 1;
                break;

            case BlockGroupType.L:
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(0, -2));
                posList.Add(new Vector2(1, -2));
                pivotIndex = 1;
                break;

            case BlockGroupType.O:
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(1, 0));
                posList.Add(new Vector2(1, -1));
                pivotIndex = 1;
                break;

            case BlockGroupType.S:
                posList.Add(new Vector2(-1, -1));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(1, 0));
                pivotIndex = 2;
                break;

            case BlockGroupType.Z:
                posList.Add(new Vector2(1, 0));
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(0, -1));
                posList.Add(new Vector2(1, -1));
                pivotIndex = 1;
                break;

            case BlockGroupType.T:
                posList.Add(new Vector2(-1, 0));
                posList.Add(new Vector2(0, 0));
                posList.Add(new Vector2(1, 0));
                posList.Add(new Vector2(0, -1));
                pivotIndex = 1;

                break;
        }

        if (CheckAbleCreateBlock(posList))
        {
            group = new BlockGroup(type, posList.LocalToWorld(), pivotIndex);
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

