using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : PowerGenerator
{
    [SerializeField] SpriteRenderer spriteRenderer;

    // Update is called once per frame
    public override void UpdateState(){
        if(state){
            spriteRenderer.color = Color.green;
        }else{
            spriteRenderer.color = Color.red;
        }
    }
}
