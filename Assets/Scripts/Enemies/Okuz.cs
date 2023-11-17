using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Okuz : EnemyDumb
{
    [SerializeField] float runSpeed = 10f;

    bool lookPlayer = true;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        switch (state) {
            case State.Chasing: {
                if (lookPlayer){
                    UpdateChasing();
                }
                break;
            }
            case State.FollowPath: {
                UpdateFollowPath();
                break;
            }
        }
    }

    public override void FixedUpdate()
    {
        switch (state) {
           case State.Chasing:
                break;
            case State.FollowPath:
                MoveFollowPath();
                break;
        }

    }

    public override void ChangeState(State newState) {
        if (state == newState) return;
        if (newState == State.Chasing) {
            StartCoroutine(GetDirection());
        }
        base.ChangeState(newState);
    }

    IEnumerator GetDirection() {
        yield return new WaitForSeconds(1f);
        lookPlayer = false;
        Vector2 diff = player.transform.position - transform.position;
        rb.velocity = diff.normalized * runSpeed;

        rb.SetRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg);
    }
}
