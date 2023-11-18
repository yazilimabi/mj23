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

    bool audioTriggered = false;
    [SerializeField] bool gunEnabled = true;
    
    Tweener shaketween = null;
    void Start(){
        gunShootTimer = gunShootTime;
        gunReloadTimer = gunReloadTime;
    }

    void Update()
    {
        if(GetComponent<PlayerMovement>().isRolling() || GetComponentInChildren<VoidDetector>().isFalling || !gunEnabled) return;

        if(gunReloadTimer > 0){
            gunReloadTimer-=Time.deltaTime;
            return;
        }

        if(Input.GetMouseButton(0)){
            gunShootTimer-=Time.deltaTime;

            if(!audioTriggered){
                AudioManager.Instance.triggerAudio(0);
                shaketween = gun.DOShakePosition(gunShootTime, 0.1f, 100, 90, false, false, ShakeRandomnessMode.Full);
                audioTriggered = true;
            }

            if(gunShootTimer < 0){
                Shoot();
                gunShootTimer = gunShootTime;
                gunReloadTimer = gunReloadTime;
            }
        }else{
            gunShootTimer = gunShootTime;//Mathf.Max(gunReloadTime, gunReloadTimer + Time.deltaTime * 3);
            if(audioTriggered)
                AudioManager.Instance.stopAudio(0);
            audioTriggered = false;
            shaketween.Kill();
        }
    }

    public void Shoot(){
        var bullet = Instantiate(bulletPrefab, nozzle.position, hand.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(nozzle.up * fireForce, ForceMode2D.Impulse);
        audioTriggered = false;
    }

    public void EnableGun(){
        gunEnabled = true;
        hand.gameObject.SetActive(true);
    }

    public void ActivateGun(){
        if(gunEnabled)
            hand.gameObject.SetActive(true);
    }

    public void DeactivateGun(){
        hand.gameObject.SetActive(false);
    }
}
