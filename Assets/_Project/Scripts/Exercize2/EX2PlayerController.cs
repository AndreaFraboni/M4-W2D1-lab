using UnityEngine;

public class EX2PlayerController : MonoBehaviour
{

    [SerializeField] float _speed = 5f;
    [SerializeField] float _smooth = 10f;

    private Rigidbody _rb;
    private float horizontal, vertical;
    private Vector3 move;

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

        Vector3 inputMove = new Vector3(horizontal, 0, vertical);

        //transform.position = Vector3.Lerp(start, end, Time.deltaTime * smooth);
        // public static Vector3 Lerp(Vector3 a, Vector3 b, float t);

        move = Vector3.Lerp(move, inputMove, _smooth * Time.deltaTime);
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + move * (_speed * Time.deltaTime));
    }

    private void Rotation()
    {
        if (move != Vector3.zero) transform.forward = move; // Simple Fix For “Look Rotation Viewing Vector Is Zero”
    }


}
