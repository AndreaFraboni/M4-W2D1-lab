using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
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
        horizontal = Input.GetAxisRaw("Horiz" +
            "ontal");
        vertical = Input.GetAxisRaw("Vertical");

        move = new Vector3(horizontal, 0, vertical);
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + move * Time.deltaTime);
    }

    private void Rotation()
    {
        if (move != Vector3.zero) transform.forward = move; // Simple Fix For “Look Rotation Viewing Vector Is Zero”
    }

}
