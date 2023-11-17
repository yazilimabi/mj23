using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : PowerConsumer
{
    [SerializeField] Collider2D col;
    [SerializeField] SpriteRenderer spriteRenderer;
    public override void TurnOn()
    {
        col.enabled = false;
        spriteRenderer.enabled = false;
    }

    public override void TurnOff()
    {
        col.enabled = true;
        spriteRenderer.enabled = true;
    }
}
