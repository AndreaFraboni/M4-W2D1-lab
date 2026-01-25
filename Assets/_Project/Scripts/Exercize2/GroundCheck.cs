using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _probeDistance = 0.1f;
    [SerializeField] private LayerMask _layerGroundMask;
    [SerializeField] private EX2PlayerController _playerController;

    private void Awake()
    {
        if (_playerController == null) _playerController = GetComponentInParent<EX2PlayerController>();

        if (_layerGroundMask == 0) _layerGroundMask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

   private void CheckGround()
    {
        
        bool grounded = Physics.Raycast(transform.position, Vector3.down, _probeDistance, _layerGroundMask);

        _playerController.isGrounded = grounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _probeDistance);
    }
}
