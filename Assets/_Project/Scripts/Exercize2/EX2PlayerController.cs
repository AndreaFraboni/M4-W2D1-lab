using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EX2PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _smooth = 10f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _rotationSpeed = 5f;

    private Rigidbody _rb;
    private float horizontal, vertical;

    private Vector3 _inputMove;
    private Vector3 _move;

    public bool isGrounded = false;

    private bool isJump = false;
    private bool isDoubleJump = false;
    private bool isDoubleJumpUsed = false;

    private bool isRunning = false;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckInput();
        CheckRun();
        CheckJump();

        if (isGrounded)
        {
            isDoubleJump = false;
            isDoubleJumpUsed = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Rotation();
        if (isJump) Jump();
        if (isDoubleJump) Jump();
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        _inputMove = new Vector3(horizontal, 0, vertical);

        if (_inputMove.sqrMagnitude > 1f) _inputMove.Normalize();

        //move = Vector3.Lerp(move, inputMove, _smooth * Time.deltaTime);
    }


    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            if (!isDoubleJumpUsed)
            {
                isDoubleJump = true;
                isDoubleJumpUsed = true;
            }
        }
    }

    private void CheckRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
    }

    private void Move()
    {
        _move = Vector3.Lerp(_move, _inputMove, _smooth * Time.fixedDeltaTime);

        if (_move.sqrMagnitude < 0.0001f)
        {
            _move = Vector3.zero;
        }

        if (_move != Vector3.zero)
        {
            if (isRunning)
            {
                _rb.MovePosition(transform.position + _move * ((_speed * 2) * Time.deltaTime));
            }
            else
            {
                _rb.MovePosition(transform.position + _move * (_speed * Time.deltaTime));
            }
        }
    }

    private void Rotation()
    {
        if (_move != Vector3.zero) //transform.forward = move; 
        {
            Quaternion _rotation = Quaternion.LookRotation(_move, Vector3.up);

            //transform.rotation = _rotation;
            // Smoothly interpolate between current rotation and target rotation
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * _rotationSpeed);
        }
    }

    private void Jump()
    {
        isJump = false;
        isDoubleJump = false;
        if (isRunning) isRunning = false;
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
