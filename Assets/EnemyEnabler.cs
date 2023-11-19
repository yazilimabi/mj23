using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnabler : PowerConsumer
{
    public override void TurnOn()
    {
        GameManager.Instance.IsSecurityBreached = true;
    }

    public override void TurnOff()
    {
        
    }
}
