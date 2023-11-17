using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            EnemyDumb enemy = transform.parent.gameObject.GetComponent<EnemyDumb>();
            enemy.OnPlayerDetected();
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")) {
            EnemyDumb enemy = transform.parent.gameObject.GetComponent<EnemyDumb>();
            enemy.OnPlayerExited();
        }
    }
}
