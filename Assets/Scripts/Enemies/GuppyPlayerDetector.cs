using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GuppyPlayerDetector : MonoBehaviour
{
    [SerializeField] Guppy guppy;
    [SerializeField] Light2D light2D;
    [SerializeField] float maxRadius;
    [SerializeField] float minRadius;

    CircleCollider2D cc;

    void Start() {
        cc = GetComponent<CircleCollider2D>();
        cc.radius = minRadius;
        light2D.pointLightInnerRadius = minRadius * 0.9f;
        light2D.pointLightOuterRadius = minRadius;
        light2D.color = Color.green;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            var player = col.transform.parent.GetComponent<PlayerMovement>();
            var state = guppy.GetState(); 
            if (player.isRolling() && state != Guppy.State.FollowPath) return;
            if (state == Guppy.State.Disabled) return;
            guppy.OnPlayerEnter();
            cc.radius = maxRadius;
            light2D.color = Color.red;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")) {
            var player = col.transform.parent.GetComponent<PlayerMovement>();
            var state = guppy.GetState(); 
            if (player.isRolling() && state != Guppy.State.FollowPath) return;
            if (state == Guppy.State.Disabled) return;
            guppy.OnPlayerExit();
            cc.radius = minRadius;
            light2D.color = Color.green;
        }
    }

}
