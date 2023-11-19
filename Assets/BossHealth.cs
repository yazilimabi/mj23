using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour
{
    [SerializeField] UnityEvent onDeath;
    [SerializeField] float maxHealth = 100f;
    float health;

    [SerializeField] GameObject win;

    void Start() {
        health = maxHealth;
    }

    public void Recover() {
        health = maxHealth;
    }

    public void Damage(float damage) {
        if(health <= 0f)
            WinGame();
        health -= damage;
        if(health <= 0f){
            onDeath.Invoke();
        }
    }

    public void WinGame(){
        win.SetActive(true);
        Destroy(gameObject);
    }
}
