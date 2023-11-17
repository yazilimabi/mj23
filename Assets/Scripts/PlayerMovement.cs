using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float rollMultiplier = 1.5f;
    [SerializeField] float rollInvincibilityTime = 0.5f;
    [SerializeField] float currentRollMultiplier = 0f;
    [SerializeField] bool ableToWalk = true;
    [SerializeField] bool isRolling = false;
    [SerializeField] Collider2D hitCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D hand;
    
    float rollTimer = 0f;
    Vector2 _direction = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;
    Vector2 _rollDirection = Vector2.zero;

    void Start(){
        rollTimer = rollInvincibilityTime;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(Input.GetMouseButton(1) && !isRolling){
            _rollDirection = _direction;

            if(_rollDirection == Vector2.zero) return;
            
            isRolling = true;
            currentRollMultiplier = 0;
            rollTimer = rollInvincibilityTime;
            
            DOTween.To(()=> currentRollMultiplier, x => currentRollMultiplier = x, rollMultiplier, 0.7f).SetEase(Ease.OutQuad).OnComplete(()=>isRolling = false).Play();
        }
    }
    
    void FixedUpdate(){
        if(!ableToWalk) return;

        if(isRolling){
            if(rollTimer > 0){
                rollTimer -= Time.fixedDeltaTime;
                hitCollider.enabled = false;
                spriteRenderer.color = spriteRenderer.color.WithAlpha(0.5f);
            }else{
                hitCollider.enabled = true;
                spriteRenderer.color = spriteRenderer.color.WithAlpha(1f);
            }

            _rigidbody.velocity = _rollDirection * walkSpeed * (rollMultiplier - currentRollMultiplier);
            return;
        }

        _rigidbody.velocity = _direction * walkSpeed;

        Vector2 aimDirection = mousePosition - _rigidbody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

        hand.transform.rotation = Quaternion.Euler(0,0,aimAngle);
    }
}
