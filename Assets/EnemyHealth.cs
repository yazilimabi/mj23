using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] UnityEvent onDeath;
    [SerializeField] float maxHealth = 100f;
    float health;

    void Start() {
        health = maxHealth;
    }

    public void Recover() {
        health = maxHealth;
    }

    public void Damage(float damage) {
        if(health <= 0f)
            return;
        health -= damage;
        if(health <= 0f){
            onDeath.Invoke();
        }
    }
}
