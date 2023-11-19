using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PowerBox : PowerGenerator
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Light2D globalLight;
    public override void UpdateState(){
        
    }
    
    public void Explode(){
        if(state) return;
        AudioManager.Instance.stopAudio(AudioManager.AudioTypes.AlarmFadeIn);
        AudioManager.Instance.stopAudio(AudioManager.AudioTypes.AlarmContinous);
        AudioManager.Instance.triggerAudio(AudioManager.AudioTypes.PowerboxBoom);
        SetState(true);
        globalLight.intensity = 0.2f;
    }
}
