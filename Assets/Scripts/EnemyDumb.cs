using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDumb : MonoBehaviour
{
    enum State {
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
            Vector2 now = path[currentIndex];
            currentIndex++;
            Vector2 distance = rb.position - currentTarget;
            rb.DORotate(rb.rotation - 90, 1f);
        } else if (loopForPath) {
            currentIndex = 0;
            NextTarget();
        }
    }

    void Update()
    {
        switch (state) {
            case State.Chasing:
                Vector2 dir = player.transform.position - gameObject.transform.position;
                direction = dir.normalized;
                break;
            case State.FollowPath:
                if (Vector2.Distance(transform.position, currentTarget) < 0.1f) {
                    NextTarget();
                }
                break;
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
}
