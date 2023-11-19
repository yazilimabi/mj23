using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] float damageToPlayer = 20f;

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player") && !GameManager.Instance.player.GetComponent<PlayerMovement>().isInvincible()) {
            col.transform.parent.GetComponent<PlayerHealth>().Damage(damageToPlayer);
        }
    }
}
