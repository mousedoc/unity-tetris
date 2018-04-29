using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroup
{
    public bool IsFixed = false;

    public List<Vector2> BlockList { get; private set; }
    public int PivotIndex { get; private set; }

    public BlockGroup(List<Vector2> worldPosList, int pivotIndex)
    {
        BlockList = worldPosList;
        PivotIndex = pivotIndex;

        SetActiveGroup(true);
    }

    private void SetActiveGroup(bool active)
    {
        var grid = IngameController.Instance.Grid;

        foreach (var pos in BlockList)
        {
            int a = (int)pos.x;
            int b = (int)pos.y;
            grid[(int)pos.y, (int)pos.x].IsActive = active;
        }

        var log = "";
        for (int y = GridInfo.Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < GridInfo.Width; x++)
            {
                log += string.Format("{0},",grid[y,x].IsActive ? "1" : "0");
            }
            log += "\n";
        }
        Debug.Log(log);
    }

    public bool IsAbleMove(Vector2 offset)
    {
        SetActiveGroup(false);

        var able = true;
        foreach (var pos in BlockList)
        {
            var newPos = pos + offset;

            if (newPos.IsAvailiable() == false)
            {
                able = false;
                break;
            }
        }

        SetActiveGroup(true);

        return able;
    }

    public int GetDegreeToRotate()
    {
        SetActiveGroup(false);

        var pivot = BlockList[PivotIndex];
        var angle = 0;
        for (angle = 90; angle < 360; angle += 90)
        {
            var able = true;
            foreach (var pos in BlockList)
            {
                var newPos = pos.RotateByPivot(pivot, angle);

                if (newPos.IsAvailiable() == false)
                {
                    able = false;
                    break;
                }
            }

            if (able)
                break;
        }

        SetActiveGroup(true);

        return angle == 360 ? 0 : angle;
    }

    public void Move(Vector2 offset)
    {
        if (offset == Vector2.zero)
            return;

        if (IsAbleMove(offset) == false)
        {
            if(offset == Vector2.down)
                IsFixed = true;

            return;
        }

        SetActiveGroup(false);

        for (int i = 0; i < BlockList.Count; i++)
            BlockList[i] += offset;

        SetActiveGroup(true);
    }

    public void MoveToLowest()
    {
        var offset = Vector2.down;

        while (IsAbleMove(offset))
            offset += Vector2.down;

        offset -= Vector2.down;

        Move(offset);
    }

    public void Rotate()
    {
        var pivot = BlockList[PivotIndex];
        var degree = GetDegreeToRotate();

        if (degree == 0)
            return;

        SetActiveGroup(false);

        for (int i = 0; i < BlockList.Count; i++)
            BlockList[i] = BlockList[i].RotateByPivot(pivot, degree);

        SetActiveGroup(true);
    }
}

