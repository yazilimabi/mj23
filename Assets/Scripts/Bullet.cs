using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col){
        if (col.collider.CompareTag("SearchCollider")) return;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("SearchCollider")) return;
        if(col.CompareTag("Player")){
            //Debug.Log("Hit Player!");
        }else if(col.CompareTag("Lever")){
            col.GetComponent<PowerGenerator>().SetState(!col.GetComponent<PowerGenerator>().state);
            //Debug.Log("Hit Lever!");
        }else{
            //Debug.Log("Hit Something!");
        }
        
        Destroy(gameObject);
    }
}
