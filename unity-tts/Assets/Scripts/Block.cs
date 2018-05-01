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
            UpdateColor();
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
            UpdateColor();
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
            Renderer.color = value;
        }
    }

    private void UpdateColor()
    {
        Color color;
        if (IsActive)
            color = Color.ToOpacity();

        else
        {
            if (IsGuide)
                color = Color.ToHalfTransparent();

            else
                color = Color.ToTransparent();
        }

        Color = color;
    }

    public void Initialize(bool isWall)
    {
        Renderer = GetComponent<SpriteRenderer>();
        IsActive = false;

        if (isWall == true)
        {
            Color = "#4860FBFF".ToColorByHex();
            IsActive = true;
            gameObject.name = "Wall";
        }
    }

    public void Inherit(Block other)
    {
        Color = other.Color;
        IsActive = other.IsActive;
    }
}

