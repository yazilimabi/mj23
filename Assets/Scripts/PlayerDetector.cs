using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
            if (hit.collider != null) {
                EnemyDumb enemy = transform.parent.gameObject.GetComponent<EnemyDumb>();
                enemy.ChangeState(EnemyDumb.State.Chasing);
            }
        }
    }
}
