using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunny : EnemyDumb
{
    [SerializeField] float chaseSpeed = 1f;
    [SerializeField] float chaseTimeBeforeShooting = 10f;
    [SerializeField] float shootDelay = 3f;
    [SerializeField] int totalShoots = 2;
    [SerializeField] float fireForce = 10f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform nozzle;


    float chaseTimer = 0;
    float shootTimer = 0;
    int shootedTimes = 0;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        switch (state) {
            case State.Chasing: {
                UpdateChasing();
                if (chaseTimer < chaseTimeBeforeShooting) {
                    chaseTimer += Time.deltaTime;
                } else {
                    chaseTimer = 0;
                    ChangeState(State.Shoot);
                }
                break;
            }
            case State.FollowPath: {
                UpdateFollowPath();
                break;
            }
            case State.Shoot: {
                Vector2 diff = player.transform.position - transform.position;
                rb.SetRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg);
                if (shootTimer < shootDelay) {
                    shootTimer += Time.deltaTime;
                } else {
                    shootTimer = 0;
                    if (shootedTimes < totalShoots) {
                        shootedTimes++;
                        var bullet = Instantiate(bulletPrefab, nozzle.position, transform.rotation);
                        bullet.GetComponent<Rigidbody2D>().AddForce(nozzle.right * fireForce, ForceMode2D.Impulse);
                        Debug.Log("aaaaa");
                    } else {
                        shootedTimes = 0;
                        ChangeState(State.FollowPath);
                    }
                }
                break;
            }
        }
    }

    public override void FixedUpdate()
    {
        switch (state) {
           case State.Chasing:
                Vector2 diff = player.transform.position - transform.position;
                rb.velocity = diff.normalized * chaseSpeed;
                break;
            case State.FollowPath:
                MoveFollowPath();
                break;
            case State.Shoot:
                break;
        }

    }

    public override void ChangeState(State newState) {
        if (state == newState) return;
        if (newState == State.Shoot) rb.velocity = Vector2.zero;
        base.ChangeState(newState);
    }
}
