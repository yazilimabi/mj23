using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Matador : MonoBehaviour
{
    public enum State {
        Disabled,
        FollowPath,
        GoCrazy,
        LookForPlayer,
    }

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int startingIndex = 0;
    [SerializeField] bool loopForPath = true;
    [SerializeField] float speed = 3f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float runTime = 4f;
    [SerializeField] float rotateTime = 1f;
    [SerializeField] Light2D light2D;

    GameObject player;
    float runTimer = 0;
    float rotateTimer = 0;
    State state = State.FollowPath;
    Vector2 currentTarget = Vector2.zero;
    int currentIndex = 0;
    Vector2[] path;
    Rigidbody2D rb;
    Vector2 savedVelocity = Vector2.zero;

    void Awake() {
        runTimer = runTime;
        rotateTimer = rotateTime;

        rb = GetComponent<Rigidbody2D>();

        if (lineRenderer == null) {
            currentTarget = transform.position;
            return;
        }

        path = new Vector2[lineRenderer.positionCount];
        for (int i = 0; i < path.Length; i++) {
            int j = i + startingIndex;
            if (j >= path.Length) {
                j -= path.Length;
            }
            path[j] = lineRenderer.GetPosition(i) + lineRenderer.transform.position;
        }
        NextTarget();
        if (path.Length >= 1) {
            transform.position = currentTarget;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform.parent.gameObject;
    }

    void NextTarget() {
        if (lineRenderer == null) return;
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
            case State.Disabled:
                rb.velocity = Vector2.zero;
                break;
        }
    }


    void OnEnable() {
        transform.position = currentTarget;
    }

    void LookForPlayer() {
        state = State.LookForPlayer;
    }

    void GoCrazy() {
        state = State.GoCrazy;
        float random = Random.Range(0f, 260f);
        Vector2 diff = player.transform.position - transform.position - new Vector3(Mathf.Cos(random), Mathf.Sin(random), 0);
        rb.velocity = runSpeed * diff.normalized;
        savedVelocity = rb.velocity;
    }

    public State GetState() {
        return state;
    }

    public void OnPlayerEnter() {
        if (state == State.Disabled) return;
        LookForPlayer();
        light2D.color = Color.red;
    }

    public void OnPlayerExit() {
        if (state == State.Disabled) return;
        runTimer = runTime;
        rotateTimer = rotateTime;
        state = State.FollowPath;
        light2D.color = Color.green;
    }

    public void Disable() {
        rb.velocity = Vector2.zero;
        state = State.Disabled;
        light2D.enabled = false;
    }

    public void OnDeath() {
        Disable();
    }
}
