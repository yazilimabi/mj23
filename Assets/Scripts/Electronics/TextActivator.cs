using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActivator : PowerConsumer
{
    [SerializeField] GameObject textObject;

    public override void TurnOn()
    {
        textObject.SetActive(true);
    }

    public override void TurnOff()
    {
        textObject.SetActive(false);
    }
}
