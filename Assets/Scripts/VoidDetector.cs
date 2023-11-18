using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDetector : MonoBehaviour
{
    public List<Transform> checkPoints = new List<Transform>();
    bool isFalling = false;
    public PlayerMovement movement;
    public Vector3 lastPos;
    void Awake(){
        movement = transform.parent.GetComponent<PlayerMovement>();
    }
    void FixedUpdate(){
        if(isFalling || movement.isRolling()) return;

        bool verdict = true;
        bool reverseVerdict = false;
        foreach(Transform point in checkPoints){
            Collider2D[] hits = Physics2D.OverlapCircleAll(point.position, 0);
            bool isHit = false;
            foreach (Collider2D hit in hits) {
                if (hit.gameObject.CompareTag("Void")) {
                    isHit = true;
                }
            }

            verdict = verdict && isHit;
            reverseVerdict = reverseVerdict || isHit;
        }

        if(verdict){
            movement.DisableMovement();
            movement.SetInvincibility(2f);
            isFalling = true;
            //düşme anim
            StartCoroutine(SpawnAfter(1f, lastPos));
        }else{
            if(!reverseVerdict && !movement.isRolling())
                lastPos = transform.position;
        }
    }

    IEnumerator SpawnAfter(float t, Vector3 spawnPos){
        yield return new WaitForSeconds(t);
        movement.EnableMovement();
        movement.MoveToPos(spawnPos);
        isFalling = false;
    }
}
