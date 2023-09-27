using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveMaxSpeed = 3f;
    [SerializeField] private float _rotateMoveSpeed = 80f;
    [SerializeField] private float _turnSpeed = 80f;
    [SerializeField] private float _jumpPower = 50f;
                                   
    [Header("감속, 가속")]          
    [SerializeField] private float _acceleration = 50f;
    [SerializeField] private float _deAcceleration = 50f;


    [Space]
    [SerializeField] private LayerMask _groundMask;
    private Rigidbody _rigid;
    private Collider _collider;

    private Vector3 _currentDir = Vector3.zero;
    private float _currentVelocity = 3f;

    private bool _isStop = false;
    private bool _isJump = false;

    public UnityEvent<float> OnChangeVelocity;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (_isStop) return;

        OnChangeVelocity?.Invoke(_currentVelocity);

        Vector3 velocity = _currentDir;
        velocity.x *= _currentVelocity;
        velocity.y = _rigid.velocity.y;
        velocity.z *= _currentVelocity;

        _rigid.velocity = velocity;
        ChangeBody();

        if (_isJump && IsGround())
        {
            _isJump = false;
        }

    }

    private void ChangeBody()
    {
        if (_currentVelocity > 0f)
        {
            Vector3 newForward = _rigid.velocity; 
            newForward.y = 0f;

            transform.forward = Vector3.Lerp(transform.forward, newForward, _turnSpeed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (_isJump) return;

        if (IsGround())
        {
            _rigid.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _isJump = true;
        }
    }

    private Vector3 GetForward()
    {
        return Define.MainCam.transform.TransformDirection(Vector3.forward);
    }



    private bool IsGround()
    {
        Vector3 pos = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
        Vector3 size = _collider.bounds.size * 0.5f;
        size.y = 0.1f;
        return Physics.OverlapBox(pos, size, Quaternion.identity, _groundMask).Length > 0;
    }

    public void ImmediatelyForwardBody()
    {
        if (_currentVelocity > 0f) return;
        Vector3 forward = GetForward();
        forward.y = 0f;
        transform.forward = forward;
    }

    public void MovementInput(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            // Define static 으로 만들기
            Vector3 forward = GetForward();
            forward.y = 0f;

            // 이거 이유를 먼저 찾기
            Vector3 right = new Vector3(forward.z, 0f, -forward.x);

            Vector3 targetDir = forward * movementInput.z + right * movementInput.x;

            Vector3 currentDir = _currentDir;
            currentDir.y = 0f;

            if (Vector3.Dot(currentDir, targetDir) < 0)
            {
                _currentVelocity = 0f;
            }
            _currentDir = Vector3.RotateTowards(_currentDir, targetDir, _rotateMoveSpeed * Time.deltaTime, 1000f);
            _currentDir.Normalize();
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0f)
        {
            _currentVelocity += _acceleration * Time.deltaTime;
        }

        else
        {
            _currentVelocity -= _deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0f, _moveMaxSpeed);
    }

    public void StopImmediatelly()
    {
        _isStop = true;
        _currentVelocity = 0f;
        _currentDir = Vector3.zero;
    }

    public void StartMove()
    {
        _isStop = false;
    }
}
