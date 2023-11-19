using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Guppy : MonoBehaviour
{
    public enum State {
        Disabled,
        FollowPath,
        Shoot,
    }

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform nozzle;
    [SerializeField] Rigidbody2D hand;
    [SerializeField] float fireForce = 10f;
    [SerializeField] float shootDelay = 0.75f;
    [SerializeField] Light2D light2D;

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int startingIndex = 0;
    [SerializeField] bool loopForPath = true;
    [SerializeField] float speed = 3f;

    GameObject player;
    float shootTimer = 0;
    State state = State.FollowPath;
    Vector2 currentTarget = Vector2.zero;
    int currentIndex = 0;
    Vector2[] path;
    Rigidbody2D rb;
    bool active = false;

    void Awake() {
        shootTimer = shootDelay * 2;

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
        if (!GameManager.Instance.IsSecurityBreached) Disable();
    }

    void NextTarget() {
        if (lineRenderer == null) return;
        if (path.Length == 0) return;
        if (currentIndex != path.Length) {
            currentTarget = path[currentIndex];
            currentIndex++;
            Vector2 diff = new Vector3(currentTarget.x, currentTarget.y) - transform.position;
            hand.DORotate(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90, 1f);
        } else if (loopForPath) {
            currentIndex = 0;
            NextTarget();
        }
    }

    void Update() {
        if (!active && GameManager.Instance.IsSecurityBreached) {
            active = true;
            Enable();
        }

        switch (state)
        {
            case State.FollowPath: 
                if (Vector2.Distance(transform.position, currentTarget) < 0.1f) {
                    NextTarget();
                }
                break;
            case State.Shoot:
                Vector2 diff = player.transform.position - hand.transform.position;
                hand.MoveRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);

                shootTimer -= Time.deltaTime;
                if (shootTimer < 0) {
                    Shoot();
                    shootTimer = shootDelay;
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
            case State.Disabled:
                rb.velocity = Vector2.zero;
                break;
            default: break;
        }
    }

    void Shoot() {
        var bullet = Instantiate(bulletPrefab, nozzle.position, hand.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(nozzle.up * fireForce, ForceMode2D.Impulse);
    }

    void OnEnable() {
        transform.position = currentTarget;
    }

    public State GetState() {
        return state;
    }

    public void OnPlayerEnter() {
        if (state == State.Disabled) return;
        state = State.Shoot;
        Vector2 diff = player.transform.position - hand.transform.position;
        hand.MoveRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90);
        light2D.color = Color.red;
    }

    public void OnPlayerExit() {
        if (state == State.Disabled) return;
        shootTimer = shootDelay * 2;
        state = State.FollowPath;
        Vector2 diff = new Vector3(currentTarget.x, currentTarget.y) - transform.position;
        hand.DORotate(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90, 1f);
        light2D.color = Color.green;
    }

    public void Disable() {
        rb.velocity = Vector2.zero;
        state = State.Disabled;
        light2D.enabled = false;
    }

    public void Enable() {
        state = State.FollowPath;
        light2D.enabled = true;
    }

    public bool Active() {
        return active || state != State.Disabled;
    }

    public void OnDeath() {
        Disable();
    }
}
