using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float enemyDamage = 50f;
    [SerializeField] float playerDamage = 50f;

    void OnCollisionEnter2D(Collision2D col){
        if(!col.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player") && !GameManager.Instance.player.GetComponent<PlayerMovement>().isInvincible()){
            col.transform.parent.GetComponent<PlayerHealth>().Damage(playerDamage);
            Destroy(gameObject);
        } else if (col.CompareTag("Enemy")) {
            var enemy = col.transform.parent;
            var possibleGuppy = enemy.GetComponent<Guppy>();
            var possibleMatador = enemy.GetComponent<Matador>();
            if (possibleGuppy) {
                possibleGuppy.OnPlayerEnter();
                if (possibleGuppy.Active()) enemy.GetComponent<EnemyHealth>().Damage(enemyDamage);
            } else if (possibleMatador) {
                if (possibleMatador.Active()) enemy.GetComponent<EnemyHealth>().Damage(enemyDamage);
                possibleMatador.OnPlayerEnter();
            }
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
