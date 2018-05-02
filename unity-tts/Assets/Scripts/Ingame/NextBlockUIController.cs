using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBlockUIController : MonoBehaviour
{
    public RectTransform targetTransform;

    private BlockGroupInfo Info;

    private int grid = 4;

    private Image[,] blocks;

    private Image prefab;

    private void Awake()
    {
        blocks = new Image[grid, grid];
        Initialize();
    }

    private void Initialize()
    {
        prefab = Resources.Load<Image>("Prefab/Ingame/CanvasBlock");
        var width = prefab.rectTransform.rect.width;
        var height = prefab.rectTransform.rect.height;

        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                var block = Instantiate<Image>(prefab);
                block.transform.SetParent(targetTransform);
                block.transform.localPosition = Vector3.zero;
                block.transform.localRotation = Quaternion.identity;
                block.transform.localScale = Vector3.one;

                var pos = new Vector2((y - 2) * width, (x - 2) * height);
                pos += new Vector2(width / 2, height / 2);
                block.rectTransform.anchoredPosition = pos;

                blocks[y, x] = block;
            }
        }

        Reset();
    }

    private void Reset()
    {
        for (int y = 0; y < grid; y++)
        {
            for (int x = 0; x < grid; x++)
            {
                blocks[y, x].color = blocks[y, x].color.ToTransparent();
            }
        }
    }

    public void ChangeNextBlockGroup(BlockGroupInfo info)
    {
        Reset();

        Info = info;
        var posList = BlockCreationService.GetBlockLocalPosList(Info.GroupType);
        foreach (var pos in posList)
        {
            var x = pos.GetIntX() + 1;
            var y = pos.GetIntY() + 3;

            blocks[x, y].color = info.ColorType.ToColor().ToOpacity();
        }

        Centering(info);
    }

    private void Centering(BlockGroupInfo info)
    {
        var width = prefab.rectTransform.rect.width;
        var height = prefab.rectTransform.rect.height;

        if (info.GroupType == BlockShapeType.S ||
            info.GroupType == BlockShapeType.Z ||
            info.GroupType == BlockShapeType.T)
        {
            targetTransform.anchoredPosition = new Vector2(width / 2.0f, -height);
        }
        else if (info.GroupType == BlockShapeType.I)
        {
            targetTransform.anchoredPosition = new Vector2(width / 2.0f, -height / 4.0f);
        }
        else
        {
            targetTransform.anchoredPosition = new Vector2(0.0f, -height / 2.0f);
        }
    }
}