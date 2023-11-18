using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActivator : PowerConsumer
{
    [SerializeField] GameObject textObject;

    [SerializeField] bool canReplay = false;

    public override void TurnOn()
    {
        textObject.SetActive(true);
    }

    public override void TurnOff()
    {
        if(canReplay)
            textObject.SetActive(false);
    }
}
