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
            Destroy(gameObject);
        }else if(col.CompareTag("Lever")){
            col.GetComponent<PowerGenerator>().SetState(!col.GetComponent<PowerGenerator>().state);
            Destroy(gameObject);
        }else if(col.CompareTag("Powerbox")){
            col.GetComponent<PowerBox>().Explode();
            Destroy(gameObject);
        }
    }
}
