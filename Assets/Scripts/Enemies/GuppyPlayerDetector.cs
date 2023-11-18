using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuppyPlayerDetector : MonoBehaviour
{
    [SerializeField] Guppy guppy;
    [SerializeField] float maxRadius;
    [SerializeField] float minRadius;

    CircleCollider2D cc;

    void Start() {
        cc = GetComponent<CircleCollider2D>();
        cc.radius = minRadius;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            var player = col.transform.parent.GetComponent<PlayerMovement>();
            if (player.Rolling() && guppy.GetState() != Guppy.State.FollowPath) return;
            guppy.OnPlayerEnter();
            cc.radius = maxRadius;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")) {
            var player = col.transform.parent.GetComponent<PlayerMovement>();
            if (player.Rolling() && guppy.GetState() != Guppy.State.FollowPath) return;
            guppy.OnPlayerExit();
            cc.radius = minRadius;
        }
    }

}
