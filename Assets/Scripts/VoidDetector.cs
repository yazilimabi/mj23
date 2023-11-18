using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDetector : MonoBehaviour
{
    public List<Transform> checkPoints = new List<Transform>();
    void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("Void") && !transform.parent.GetComponent<PlayerMovement>().Rolling()){
            Debug.Log("stay");
            bool verdict = true;
            foreach(Transform point in checkPoints){
                Collider2D[] hits = Physics2D.OverlapCircleAll(point.position, 0);
                bool isHit = false;
                foreach (Collider2D hit in hits) {
                    if (hit.gameObject.CompareTag("Void")) {
                        isHit = true;
                    }
                }

                verdict = verdict && isHit;
            }

            if(verdict){
                transform.parent.GetComponent<PlayerMovement>().DisableMovement();
            }
        }
    }
}
