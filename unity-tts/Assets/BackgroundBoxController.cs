using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBoxController : MonoBehaviour
{
    public float Speed;

    private Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        var offset = render.material.mainTextureOffset;
        offset += Vector2.right * Time.deltaTime * Speed;
        render.material.mainTextureOffset = offset;
    }
}
