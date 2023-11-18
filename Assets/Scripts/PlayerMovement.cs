using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float rollMultiplier = 1.5f;
    [SerializeField] float rollInvincibilityTime = 0.5f;
    [SerializeField] bool ableToWalk = true;
    [SerializeField] bool _isRolling = false;
    [SerializeField] Collider2D hitCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D hand;
    
    float rollTimer = 0f;
    float currentRollMultiplier = 0f;
    float invincibilityTime = 0f;
    Vector2 _direction = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;
    Vector2 _rollDirection = Vector2.zero;

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(Input.GetMouseButton(1) && !_isRolling){
            _rollDirection = _direction;

            if(_rollDirection == Vector2.zero) return;
            
            _isRolling = true;
            currentRollMultiplier = 0;
            SetInvincibility(rollInvincibilityTime, false);
            DOTween.To(()=> currentRollMultiplier, x => currentRollMultiplier = x, rollMultiplier, 0.7f).SetEase(Ease.OutQuad).OnComplete(()=>_isRolling = false).Play();
        }
    }
    
    void FixedUpdate(){
        if(invincibilityTime > 0){
            hitCollider.enabled = false;
            invincibilityTime -= Time.fixedDeltaTime;
            spriteRenderer.color = Color.gray;
        }else{
            hitCollider.enabled = true;
            spriteRenderer.color = Color.white;
        }

        if(!ableToWalk) return;

        if(_isRolling){
            _rigidbody.velocity = _rollDirection * walkSpeed * (rollMultiplier - currentRollMultiplier);
            return;
        }

        _rigidbody.velocity = _direction * walkSpeed;

        Vector2 aimDirection = mousePosition - _rigidbody.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

        hand.transform.rotation = Quaternion.Euler(0,0,aimAngle);
    }

    public bool isRolling() {
        return _isRolling;
    }

    public void DisableMovement(bool continueMoving = false) {
        ableToWalk = false;
        _isRolling = false;
        hitCollider.enabled = true;

        if(!continueMoving) _rigidbody.velocity = Vector2.zero;
    }

    public void EnableMovement() {
        ableToWalk = true;
        _rigidbody.velocity = Vector2.zero;
    }

    public void SetInvincibility(float newTime, bool hardSet = true){
        if(hardSet) invincibilityTime = newTime;
        else if(invincibilityTime < newTime) invincibilityTime = newTime;
    }

    public void MoveToPos(Vector3 pos){
        transform.position = pos;
    }
}
