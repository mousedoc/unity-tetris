using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour
{
    private SpriteRenderer Renderer { get; set; }

    private bool isActive = false;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            UpdateBlock();
        }
    }

    private bool isGuide = false;
    public bool IsGuide
    {
        get
        {
            return isGuide;
        }
        set
        {
            isGuide = value;
            UpdateBlock();
        }
    }

    public Color Color
    {
        get
        {
            return Renderer.color;
        }
        set
        {
            if (Color != Color.white)
                Debug.Log("??");
            if (Color == Color.white)
                Debug.Log("!!");
            Renderer.color = value;
        }
    }

    private void UpdateBlock()
    {
        if (IsActive)
        {
            Color = Color.ToOpacity();
        }
        else
        {
            if (IsGuide)
            {
                Color = Color.ToHalfTransparent();
            }
            else
            {
                Color = Color.ToTransparent();
            }
        }
    }

    public void Initialize()
    {
        Renderer = GetComponent<SpriteRenderer>();
        IsActive = false;
    }

    public void Inherit(Block other)
    {
        var tempColor = Color;
        Color = other.Color;
        other.Color = tempColor;

        var tempIsGuide = IsGuide;
        IsGuide = other.IsGuide;
        other.IsGuide = IsGuide;

        var tempActive = IsActive;
        IsActive = other.IsActive;
        other.IsActive = IsActive;
    }
}
