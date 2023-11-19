using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBox : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite On, Off; 
    [SerializeField] PowerGenerator powerGenerator;
    
    void OnEnable() {
        if(powerGenerator){
            if(powerGenerator.state){
                spriteRenderer.sprite = On;
            } else {
                spriteRenderer.sprite = Off;
            }
            return;
        }
        if(GameManager.Instance.IsPowerOn){
            spriteRenderer.sprite = On;
        } else {
            spriteRenderer.sprite = Off;
        }
    }
}
