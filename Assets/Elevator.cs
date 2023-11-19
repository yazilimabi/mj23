using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] PowerGenerator powerGenerator;
    [SerializeField] GameObject guvenlikMesaji;

    void Update() {
        if(powerGenerator.state){
            guvenlikMesaji.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(powerGenerator.state)
            if(col.CompareTag("Player")){
                col.GetComponent<PlayerMovement>().DisableMovement();
            }
    }
}
