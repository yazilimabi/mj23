using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunny : EnemyDumb
{
    [SerializeField] float chaseSpeed = 1f;
    [SerializeField] float shootDelay = 0.3f;
    [SerializeField] float fireForce = 10f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform nozzle;
    [SerializeField] CircleCollider2D playerDetector;
    [SerializeField] Rigidbody2D hand;

    float shootTimer = 0;

    public override void Start()
    {
        shootTimer = 0;
        base.Start();
    }

    public override void Update()
    {
        switch (state) {
            case State.Chasing: {
                UpdateChasing();
                break;
            }
            case State.FollowPath: {
                UpdateFollowPath();
                break;
            }
            case State.Shoot: {
                Vector2 diff = player.transform.position - transform.position;
                hand.SetRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);

                Debug.Log(diff);
                if (shootTimer < shootDelay) {
                    shootTimer += Time.deltaTime;
                } else {
                    shootTimer = 0;
                    var bullet = Instantiate(bulletPrefab, nozzle.position, hand.transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(nozzle.up * fireForce, ForceMode2D.Impulse);
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

    public override void OnPlayerDetected() {
        playerDetector.radius *= 2;
        ChangeState(State.Shoot);
    }
    public override void OnPlayerExited() {
        playerDetector.radius /= 2;
        ChangeState(State.FollowPath);
        shootTimer = 0;
    }

    public override void ChangeState(State newState) {
        if (state == newState) return;
        if (newState == State.Shoot) rb.velocity = Vector2.zero;
        base.ChangeState(newState);
    }
}
