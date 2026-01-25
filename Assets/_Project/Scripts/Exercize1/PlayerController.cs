
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _smooth = 10f;

    private Rigidbody _rb;
    private float horizontal, vertical;

    private Vector3 _inputMove;
    private Vector3 _move;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        Move();
        Rotation();
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        _inputMove = new Vector3(horizontal, 0, vertical);

        if (_inputMove.sqrMagnitude > 1f) _inputMove.Normalize();

        // transform.position = Vector3.Lerp(start, end, Time.deltaTime * smooth);
        // public static Vector3 Lerp(Vector3 a, Vector3 b, float t);
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
            _rb.MovePosition(transform.position + _move * (_speed * Time.deltaTime));
        }
    }

    private void Rotation()
    {
        if (_move != Vector3.zero) transform.forward = _move; // Simple Fix For “Look Rotation Viewing Vector Is Zero”
    }
}
