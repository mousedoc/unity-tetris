using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour
{
    private SpriteRenderer Renderer { get; set; }

    public Sprite on, off;

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

            if (isActive)
            {
                Renderer.sprite = on;
            }
            else
            {
                Renderer.sprite = off;
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
        IsActive = other.IsActive;
    }
}
