using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatadorAttack : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField] float damageToPlayer = 20f;

    void Start(){
        _rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!transform.parent.GetComponent<Matador>().Active()) return;
        if(col.CompareTag("Player") && !GameManager.Instance.player.GetComponent<PlayerMovement>().isInvincible()) {
            col.transform.parent.GetComponent<PlayerHealth>().Damage(damageToPlayer, _rigidbody.velocity);
        }
    }
}
