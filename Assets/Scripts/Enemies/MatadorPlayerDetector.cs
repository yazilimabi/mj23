using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatadorPlayerDetector : MonoBehaviour
{
    [SerializeField] Matador matador;
    
    CircleCollider2D cc;

    void Start() {
        cc = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            matador.OnPlayerEnter();
            cc.radius *= 2;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")) {
            matador.OnPlayerExit();
            cc.radius /= 2;
        }
    }

}
