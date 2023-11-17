using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float fireForce = 10f;
    [SerializeField] float shootTime = 0.5f;
    [SerializeField] Transform nozzle;
    [SerializeField] GameObject bulletPrefab;
    float shootTimer = 0f;
    void Start()
    {
        shootTimer = shootTime;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        if(shootTimer < 0){
            shootTimer = shootTime;
            Shoot();
        }
    }

    public void Shoot(){
        var bullet = Instantiate(bulletPrefab, nozzle.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(nozzle.up * fireForce, ForceMode2D.Impulse);
    }
}
