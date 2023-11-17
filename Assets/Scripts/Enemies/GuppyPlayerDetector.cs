using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuppyPlayerDetector : MonoBehaviour
{
    [SerializeField] Guppy guppy;
    
    CircleCollider2D cc;

    void Start() {
        cc = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            guppy.OnPlayerEnter();
            cc.radius *= 2;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")) {
            guppy.OnPlayerExit();
            cc.radius /= 2;
        }
    }

}
