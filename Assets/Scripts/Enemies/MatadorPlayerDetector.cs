using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MatadorPlayerDetector : MonoBehaviour
{
    [SerializeField] Matador matador;
    [SerializeField] Light2D light2D;
    [SerializeField] float maxRadius;
    [SerializeField] float minRadius;

    CircleCollider2D cc;

    void Start() {
        cc = GetComponent<CircleCollider2D>();
        cc.radius = minRadius;
        light2D.pointLightInnerRadius = minRadius * 0.8f;
        light2D.pointLightOuterRadius = minRadius;
        light2D.color = Color.green;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            var player = col.transform.parent.GetComponent<PlayerMovement>();
            if (player.isRolling() && matador.GetState() != Matador.State.FollowPath) return;
            matador.OnPlayerEnter();
            cc.radius = maxRadius;
            light2D.color = Color.red;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.CompareTag("Player")) {
            var player = col.transform.parent.GetComponent<PlayerMovement>();
            if (player.isRolling() && matador.GetState() != Matador.State.FollowPath) return;
            matador.OnPlayerExit();
            cc.radius = minRadius;
            light2D.color = Color.green;
        }
    }

}
