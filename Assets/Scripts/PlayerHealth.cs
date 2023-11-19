using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] UnityEvent onDeath;
    [SerializeField] float maxHealth = 100f;
    PlayerMovement playerMovement;
    float health;

    void Start() {
        health = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Recover() {
        health = maxHealth;
    }

    public void Damage(float damage, Vector2 pushVector) {
        playerMovement.DisableMovement();
        playerMovement.animator.SetBool("Hit", true);
        playerMovement._rigidbody.velocity = pushVector*1.2f;

        StartCoroutine("DamageCoroutine");
    }

    IEnumerator DamageCoroutine() {
        yield return new WaitForSeconds(0.7f);
        playerMovement.animator.SetBool("Hit", false);
        playerMovement.EnableMovement();
    }
}
