using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Door : PowerConsumer
{
    [SerializeField] Collider2D col;
    [SerializeField] ShadowCaster2D shadowCaster2D;
    [SerializeField] Animator animator;

    bool firstTime = true;

    void OnEnable() {
        animator.SetBool("Open", !col.enabled);
    }

    public override void TurnOn()
    {
        col.enabled = false;
        shadowCaster2D.castsShadows = false;
        animator.SetBool("Open", true);
        if(!firstTime)
            AudioManager.Instance.triggerAudio(AudioManager.AudioTypes.DoorOpen);
        else 
            firstTime = false;
    }

    public override void TurnOff()
    {
        col.enabled = true;
        shadowCaster2D.castsShadows = true;
        animator.SetBool("Open", false);
        if(!firstTime)
            AudioManager.Instance.triggerAudio(AudioManager.AudioTypes.DoorOpen);
        else
            firstTime = false;
    }
}
