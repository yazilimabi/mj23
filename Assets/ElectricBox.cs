using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBox : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite On, Off; 
    
    void OnEnable() {
        if(GameManager.Instance.IsPowerOn){
            spriteRenderer.sprite = On;
        }
    }
}
