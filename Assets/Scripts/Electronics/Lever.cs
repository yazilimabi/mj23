using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : PowerGenerator
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float interactTimer = 0.75f;
    float timer = 0f;
    public override void UpdateState(){
        if(state){
            spriteRenderer.color = Color.green;
        }else{
            spriteRenderer.color = Color.red;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && timer <= 0){
            SetState(!state);
            timer = interactTimer;
        }
    }
    
    void FixedUpdate(){
        if(timer > 0) timer-=Time.fixedDeltaTime;
    }
}
