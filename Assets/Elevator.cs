using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    [SerializeField] PowerGenerator powerGenerator;
    [SerializeField] GameObject guvenlikMesaji;

    void Update() {
        if(powerGenerator.state||true){
            guvenlikMesaji.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        //if(powerGenerator.state)
            if(col.CompareTag("Player")){
                ChangeScene();
            }
    }

    void ChangeScene() {
        SceneManager.LoadScene("Boss");
    }
}
