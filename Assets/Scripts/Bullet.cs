using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float enemyDamage = 50f;
    [SerializeField] float playerDamage = 50f;

    void OnCollisionEnter2D(Collision2D col){
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            col.transform.parent.GetComponent<PlayerHealth>().Damage(playerDamage);
            Destroy(gameObject);
        } else if (col.CompareTag("Enemy")) {
            col.transform.parent.GetComponent<EnemyHealth>().Damage(enemyDamage);
            Destroy(gameObject);
        }
        else if(col.CompareTag("Lever")){
            col.GetComponent<PowerGenerator>().SetState(!col.GetComponent<PowerGenerator>().state);
            Destroy(gameObject);
        }else if(col.CompareTag("Powerbox")){
            col.GetComponent<PowerBox>().Explode();
            Destroy(gameObject);
        }
    }
}
