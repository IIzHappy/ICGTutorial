using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rb;
    public InputAction _movement;

    [SerializeField] float _moveSpeed = 8;
    [SerializeField] float _jumpHeight = 12;

    [SerializeField] float _groundedThres = 0.2f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 moveDir = _movement.ReadValue<Vector2>();
        Vector2 move = moveDir.normalized * _moveSpeed;
        _rb.linearVelocity = move;
    }

    public void OnJump(InputValue input)
    {
        if (IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Vector3 _rayStart = gameObject.transform.position - (gameObject.transform.localScale.z / 2) * Vector3.down;
        RaycastHit2D raycastHit = Physics2D.Raycast(_rayStart, Vector3.down, _groundedThres, LayerMask.GetMask("Ground"));
        return raycastHit.collider != null;
    }
}
