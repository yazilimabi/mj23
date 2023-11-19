using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum Phase {
        Follow,
        Rush,
        Spawn,
        LAST,
    }

    [SerializeField] float speed = 3f;
    [SerializeField] float changePhaseTime = 4f;
    [SerializeField] float changeFromRush = 1f;
    [SerializeField] int maxMatadors = 3;
    [SerializeField] GameObject matadorPrefab;
    [SerializeField] GameObject barrack;

    Phase phase;
    Rigidbody2D rb;
    GameObject player;
    Vector2 savedVelocity = Vector2.zero;
    float phaseTimer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        phaseTimer = changePhaseTime;
        player = GameManager.Instance.player;
        SetPhase(Phase.Follow);
    }

    void Update() {
        phaseTimer -= Time.deltaTime;
        if (phaseTimer < 0) {
            phaseTimer = changePhaseTime;
            ChangePhase();
        }
    }

    void FixedUpdate()
    {
        switch (phase)
        {
            case Phase.Follow:
                rb.MovePosition(Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.fixedDeltaTime));
                break;
            case Phase.Rush:
                rb.velocity = savedVelocity * speed;
                break;
            default: break;
        }
    }

    public void ChangePhase() {
        int newPhase = Random.Range(0, (int)Phase.LAST);
        SetPhase((Phase)newPhase);
    }

    public void SetPhase(Phase newPhase) {
        phaseTimer = changePhaseTime;
        phase = newPhase;
    
        switch (phase) {
            case Phase.Follow:
                break;
            case Phase.Rush:
                phaseTimer = changeFromRush;
                Vector2 diff = player.transform.position - transform.position;
                savedVelocity = diff.normalized;
                break;
            case Phase.Spawn:
                if (barrack.transform.childCount >= maxMatadors) {
                    SetPhase(Phase.Follow);
                    return;
                }
                Vector2 midPoint = Vector2.Lerp(transform.position, player.transform.position, 0.5f);
                var matador = Instantiate(matadorPrefab, midPoint, transform.rotation);
                matador.transform.SetParent(barrack.transform);
                SetPhase(Phase.Rush);
                break;
            default: break;
        }
    }
}
