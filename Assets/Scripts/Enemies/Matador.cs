using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Matador : MonoBehaviour
{
    public enum State {
        FollowPath,
        GoCrazy,
        LookForPlayer,
    }

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] bool loopForPath = true;
    [SerializeField] float speed = 3f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float runTime = 4f;
    [SerializeField] float rotateTime = 1f;
    
    GameObject player;
    float runTimer = 0;
    float rotateTimer = 0;
    State state = State.FollowPath;
    Vector2 currentTarget = Vector2.zero;
    int currentIndex = 0;
    Vector2[] path;
    Rigidbody2D rb;
    Vector2 savedVelocity = Vector2.zero;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform.parent.gameObject;

        runTimer = runTime;
        rotateTimer = rotateTime;

        rb = GetComponent<Rigidbody2D>();

        path = new Vector2[lineRenderer.positionCount];
        for (int i = 0; i < path.Length; i++) {
            path[i] = lineRenderer.GetPosition(i);
        }
        NextTarget();
        if (path.Length >= 1) {
            transform.position = currentTarget;
        }
    }

    void NextTarget() {
        if (path.Length == 0) return;
        if (currentIndex != path.Length) {
            currentTarget = path[currentIndex];
            currentIndex++;
        } else if (loopForPath) {
            currentIndex = 0;
            NextTarget();
        }
    }

    void Update() {
        switch (state)
        {
            case State.FollowPath: 
                if (Vector2.Distance(transform.position, currentTarget) < 0.1f) {
                    NextTarget();
                }
                break;
            case State.GoCrazy:
                runTimer -= Time.deltaTime;
                if (runTimer < 0) {
                    LookForPlayer();
                    runTimer = runTime;
                }
                break;
            case State.LookForPlayer:
                rotateTimer -= Time.deltaTime;
                if (rotateTimer < 0) {
                    GoCrazy();
                    rotateTimer = rotateTime;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.FollowPath: 
                rb.MovePosition(Vector2.MoveTowards(rb.position, currentTarget, speed * Time.fixedDeltaTime));
                break;
            case State.GoCrazy:
                rb.velocity = savedVelocity;
                break;
            case State.LookForPlayer:
                rb.velocity = Vector2.zero;
                break;
        }
    }

    void LookForPlayer() {
        state = State.LookForPlayer;
    }

    void GoCrazy() {
        state = State.GoCrazy;
        Vector2 diff = player.transform.position - transform.position;
        rb.velocity = runSpeed * diff.normalized;
        savedVelocity = rb.velocity;
    }

    public State GetState() {
        return state;
    }

    public void OnPlayerEnter() {
        LookForPlayer();
    }

    public void OnPlayerExit() {
        runTimer = runTime;
        rotateTimer = rotateTime;
        state = State.FollowPath;
    }
}
