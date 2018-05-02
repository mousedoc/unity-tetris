using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroup
{
    public bool IsFixed { get; private set; }

    public BlockShapeType Type { get; private set; }
    public List<Vector2> BlockList { get; private set; }
    public List<Vector2> GuideList { get; private set; }
    public int DefaultPivotIndex { get; private set; }
    public Color Color { get; private set; }
    public Block[,] Grid 
    {
        get { return IngameController.Instance.Grid; }
    }

    public BlockGroup(BlockShapeType type, List<Vector2> worldPosList, int pivotIndex, Color color)
    {
        Type = type;
        BlockList = worldPosList;
        GuideList = new List<Vector2>();
        DefaultPivotIndex = pivotIndex;
        Color = color;

    }

    public void Initialize()
    {
        UpdateColor();
        UpdateGuide();
        SetActiveGroup(true);
    }

    private void UpdateColor()
    {
        foreach(var pos in BlockList)
        {
            Grid[pos.GetIntY(), pos.GetIntX()].Color = Color;
        }
    }

    public void Fix()
    {
        IsFixed = true;
        UpdateGuideState(false);
    }

    private void SetActiveGroup(bool active)
    {
        foreach (var pos in BlockList)
        {
            Grid[(int)pos.y, (int)pos.x].IsActive = active;
        }

        //var log = "";
        //for (int y = GridInfo.Height - 1; y >= 0; y--)
        //{
        //    for (int x = 0; x < GridInfo.Width; x++)
        //    {
        //        log += string.Format("{0},",Grid[y,x].IsActive ? "1" : "0");
        //    }
        //    log += "\n";
        //}
        //Debug.Log(log);
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

    public class RotateInfo
    {
        public int Angle { get; private set; }

        public int PivotIndex { get; private set; }

        public RotateInfo(int angle, int pivotIndex)
        {
            Angle = angle;
            PivotIndex = pivotIndex;
        }
    }

    public RotateInfo GetRotationInfo()
    {
        SetActiveGroup(false);

        var angle = 0;
        var able = true;
        RotateInfo info = null;

        for (int i = DefaultPivotIndex; i >= 0; i--)
        {
            var pivot = BlockList[i];
            for (angle = 90; angle < 360; angle += 180)
            {
                able = true;
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
                {
                    info = new RotateInfo(angle, i);
                    break;
                }
            }

            if (able)
                break;
        }

        SetActiveGroup(true);

        return info;
    }

    public void Move(Vector2 offset)
    {
        if (offset == Vector2.zero)
            return;

        if (IsAbleMove(offset) == false)
        {
            if (offset == Vector2.down)
                Fix();

            return;
        }

        SetActiveGroup(false);

        var newPos = new List<Vector2>();
        for (int i = 0; i < BlockList.Count; i++)
            newPos.Add(BlockList[i] + offset);

        ApplyBlockPosition(newPos);

        SetActiveGroup(true);
    }

    public void MoveToLowest()
    {
        Move(GetLowestMoveOffset());
    }

    private Vector2 GetLowestMoveOffset()
    {
        var offset = Vector2.down;

        while (IsAbleMove(offset))
            offset += Vector2.down;

        return offset - Vector2.down;
    }

    public void Rotate()
    {
        var rotateInfo = GetRotationInfo();

        if (rotateInfo == null)
            return;

        SetActiveGroup(false);

        if (Type != BlockShapeType.O)
        {
            var newPos = new List<Vector2>();
            var pivot = BlockList[rotateInfo.PivotIndex];

            for (int i = 0; i < BlockList.Count; i++)
                newPos.Add(BlockList[i].RotateByPivot(pivot, rotateInfo.Angle));

            ApplyBlockPosition(newPos);
        }

        SetActiveGroup(true);
    }

    private void ApplyBlockPosition(List<Vector2> newGroup)
    {
        for (int i = 0; i < newGroup.Count; i++)
        {
            var oldX = (int)BlockList[i].x;
            var oldY = (int)BlockList[i].y;

            var newX = (int)newGroup[i].x;
            var newY = (int)newGroup[i].y;

            var oldBlock = IngameController.Instance.Grid[oldY, oldX];
            var newBlock = IngameController.Instance.Grid[newY, newX];
            

            newBlock.Inherit(oldBlock);
        }

        BlockList = newGroup;
        EffectSoundManager.Instance.Play(EffectSoundType.BlockAction);

        UpdateGuide();
    }

    private void UpdateGuide()
    {
        UpdateGuideState(false);

        GuideList.Clear();
        var offset = GetLowestMoveOffset();
       
        foreach(var block in BlockList)
            GuideList.Add(block + offset);

        UpdateGuideState(true);
    }

    private void UpdateGuideState(bool active)
    {
        for (int i = 0; i < GuideList.Count; i++)
        {
            var guideX = GuideList[i].GetIntX();
            var guideY = GuideList[i].GetIntY();

            var blockX = BlockList[i].GetIntX();
            var blockY = BlockList[i].GetIntY();

            Grid[guideY, guideX].Color = Grid[blockY, blockX].Color;
            Grid[guideY, guideX].IsGuide = active;
        }
    }
}

