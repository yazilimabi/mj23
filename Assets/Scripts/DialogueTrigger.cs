using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : PowerGenerator
{
    bool isTriggered = false;

    public override void UpdateState()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !isTriggered){
            isTriggered = true;
            SetState(true);
        }
    }
}
