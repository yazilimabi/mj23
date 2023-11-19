using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lever : PowerGenerator
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float interactTimer = 0.75f;
    [SerializeField] Sprite turnedOn,turnedOff;
    [SerializeField] Light2D light2d;
    float timer = 0f;
    public override void UpdateState(){
        if(state){
            spriteRenderer.sprite = turnedOn;
            light2d.color = Color.green;
        }else{
            spriteRenderer.sprite = turnedOff;
            light2d.color = Color.red;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && timer <= 0){
            SetState(!state);
            timer = interactTimer;
            AudioManager.Instance.triggerAudio(AudioManager.AudioTypes.Button);
        }/*else if(other.GetComponent<Matador>()){
            if(other.GetComponent<Matador>().GetState() == Matador.State.GoCrazy){
                SetState(!state);
                timer = interactTimer;
            }
        }*/
    }
    
    void FixedUpdate(){
        if(timer > 0) timer-=Time.fixedDeltaTime;
    }
}
