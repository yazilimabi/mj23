using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatadorPlayerDetector : MonoBehaviour
{
    [SerializeField] Matador matador;
    [SerializeField] float maxRadius;
    [SerializeField] float minRadius;

    CircleCollider2D cc;

    void Start() {
        cc = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            var player = col.transform.parent.GetComponent<PlayerMovement>();
            if (player.Rolling() && matador.GetState() != Matador.State.FollowPath) return;
            matador.OnPlayerEnter();
            cc.radius = maxRadius;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")) {
            var player = col.transform.parent.GetComponent<PlayerMovement>();
            if (player.Rolling() && matador.GetState() != Matador.State.FollowPath) return;
            matador.OnPlayerExit();
            cc.radius = minRadius;
        }
    }

}
