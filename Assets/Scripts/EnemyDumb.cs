using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDumb : MonoBehaviour
{
    public enum State {
        Chasing,
        FollowPath,
        Shoot,
    }

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] bool loopForPath = true;
    [SerializeField] public float speed = 1f;
    [SerializeField] public State state = State.FollowPath;

    public GameObject player;

    Vector2 direction = Vector2.zero;
    Vector2[] path;
    int currentIndex = 0;
    Vector2 currentTarget = Vector2.zero;

    public Rigidbody2D rb;

    public virtual void Init(LineRenderer line)
    {
        path = new Vector2[line.positionCount];
        for (int i = 0; i < path.Length; i++) {
            path[i] = line.GetPosition(i);
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

    public virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        Init(lineRenderer);
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

    public virtual void Update()
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
        }
    }

    public virtual void FixedUpdate()
    {
        switch (state) {
           case State.Chasing:
                MoveChasing();
                break;
            case State.FollowPath:
                MoveFollowPath();
                break;
        }
    }

    public void MoveChasing() {
        rb.velocity = direction * speed;
    }
    public void MoveFollowPath() {
        rb.MovePosition(Vector2.MoveTowards(rb.position, currentTarget, speed * Time.fixedDeltaTime));
    }
    public void UpdateChasing() {
        Vector2 diff = player.transform.position - transform.position;
        rb.SetRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg);
        direction = diff.normalized;

    }
    public void UpdateFollowPath() {
        Vector2 diff = new Vector3(currentTarget.x, currentTarget.y) - transform.position;
        rb.SetRotation(Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg);
        if (Vector2.Distance(transform.position, currentTarget) < 0.1f) {
            NextTarget();
        }
    }
    public virtual void ChangeState(State newState) {
        state = newState;
    }
}
