using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatadorSpawner : MonoBehaviour
{
    float timer = 0;
    
    [SerializeField] GameObject matador;
    [SerializeField] Transform[] spawnPoints;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5) {
            Instantiate(matador, spawnPoints[Random.Range(0, spawnPoints.Length)]);
            timer = 0;
        }
    }
}
