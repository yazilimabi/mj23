using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tureight : MonoBehaviour
{
    [SerializeField] float fireForce = 10f;
    [SerializeField] float shootTime = 0.5f;
    [SerializeField] Transform[] nozzles;
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

    public void Shoot() {
        foreach (Transform nozzle in nozzles) {
            var bullet = Instantiate(bulletPrefab, nozzle.position, nozzle.parent.transform.parent.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(nozzle.up * fireForce, ForceMode2D.Impulse);
        }
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 45), shootTime);
    }
}
