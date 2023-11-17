using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col){
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            Debug.Log("Hit Player!");
        }else{
            Debug.Log("Hit Something!");
        }
        Destroy(gameObject);
    }
}
