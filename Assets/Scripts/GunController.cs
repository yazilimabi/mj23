using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] float gunShootTime = 0.75f;
    [SerializeField] float gunReloadTime = 0f;
    [SerializeField] float fireForce = 10f;

    [SerializeField] Transform gun, nozzle, hand;
    [SerializeField] GameObject bulletPrefab;
    float gunShootTimer = 0f;
    float gunReloadTimer = 0f;
    
    DOTween shaketween = null;
    void Start(){
        gunShootTimer = gunShootTime;
        gunReloadTimer = gunReloadTime;
    }

    void Update()
    {
        if(gunReloadTimer > 0){
            gunReloadTimer-=Time.deltaTime;
            return;
        }

        if(Input.GetMouseButton(0)){
            gunShootTimer-=Time.deltaTime;


            if(gunShootTimer < 0){
                Shoot();
                gunShootTimer = gunShootTime;
                gunReloadTimer = gunReloadTime;
            }
        }else{
            gunShootTimer = gunShootTime;//Mathf.Max(gunReloadTime, gunReloadTimer + Time.deltaTime * 3);
        }
    }

    public void Shoot(){
        var bullet = Instantiate(bulletPrefab, nozzle.position, hand.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(nozzle.up * fireForce, ForceMode2D.Impulse);
    }
}
