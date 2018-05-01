using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBoxController : MonoBehaviour
{
    public float Speed;

    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        var offset = renderer.material.mainTextureOffset;
        offset += Vector2.right * Time.deltaTime * Speed;
        renderer.material.mainTextureOffset = offset;
    }
}
