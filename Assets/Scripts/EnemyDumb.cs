using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDumb : MonoBehaviour
{
    public enum State {
        Chasing,
        FollowPath
    }

    [SerializeField] float walkSpeed = 10f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] State state = State.FollowPath;
    [SerializeField] bool loopForPath = true;

    GameObject player;
    Vector2 direction = Vector2.zero;
    Vector2[] path;
    int currentIndex = 0;
    Vector2 currentTarget = Vector2.zero;
    float nextRotation = 0;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        path = new Vector2[lineRenderer.positionCount];
        for (int i = 0; i < path.Length; i++) {
            path[i] = lineRenderer.GetPosition(i);
        }
        NextTarget();
        if (path.Length >= 1) {
            transform.position = currentTarget;
        }

        GameObject[] possiblePlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject possiblePlayer in possiblePlayers)
        {
            if (possiblePlayer.name == "Player") {
                player = possiblePlayer;
            }
        }
    }

    void NextTarget() {
        if (path.Length == 0) return;
        if (currentIndex != path.Length) {
            currentTarget = path[currentIndex];
            currentIndex++;
            Vector2 diff = new Vector3(currentTarget.x, currentTarget.y) - transform.position;
            rb.MoveRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg);
        } else if (loopForPath) {
            currentIndex = 0;
            NextTarget();
        }
    }

    void Update()
    {
        switch (state) {
            case State.Chasing: {
                Vector2 diff = player.transform.position - transform.position;
                rb.SetRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg);
                direction = diff.normalized;
                break;
            }
            case State.FollowPath: {
                if (Vector2.Distance(transform.position, currentTarget) < 0.1f) {
                    NextTarget();
                }
                break;
            }
        }
    }

    void FixedUpdate()
    {
        switch (state) {
           case State.Chasing:
                rb.velocity = direction * walkSpeed;
                break;
            case State.FollowPath:
                rb.MovePosition(Vector2.MoveTowards(rb.position, currentTarget, walkSpeed * Time.fixedDeltaTime));
                break;
        }

    }

    public void ChangeState(State state) {
        this.state = state;
    }
}
